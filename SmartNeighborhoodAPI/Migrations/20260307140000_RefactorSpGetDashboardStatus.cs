using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260307140000_RefactorSpGetDashboardStatus")]
    public partial class RefactorSpGetDashboardStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE OR ALTER PROCEDURE [dbo].[sp_GetDashboardStatus]
    @NeighborhoodId     INT = NULL,
    @ResidentialUnitId  INT = NULL
WITH RECOMPILE
AS
BEGIN
    SET NOCOUNT ON;

    -- =========================================================
    -- Step 1: Filtered families
    --         Clustered PK on Id = fast lookups from FamilyMembers
    -- =========================================================
    CREATE TABLE #FilteredFamilies
    (
        Id              INT NOT NULL PRIMARY KEY CLUSTERED,
        FamilyCatgoryId INT NOT NULL,
        HousingType     INT NOT NULL
    );

    IF @ResidentialUnitId IS NOT NULL
    BEGIN
        INSERT INTO #FilteredFamilies (Id, FamilyCatgoryId, HousingType)
        SELECT f.Id, f.FamilyCatgoryId, f.HousingType
        FROM   dbo.Families     AS f
        INNER JOIN dbo.Blocks   AS b  ON b.Id = f.BlockId
        WHERE  b.ResidentialUnitId = @ResidentialUnitId;
    END
    ELSE IF @NeighborhoodId IS NOT NULL
    BEGIN
        INSERT INTO #FilteredFamilies (Id, FamilyCatgoryId, HousingType)
        SELECT f.Id, f.FamilyCatgoryId, f.HousingType
        FROM   dbo.Families             AS f
        INNER JOIN dbo.Blocks           AS b  ON b.Id  = f.BlockId
        INNER JOIN dbo.ResidentialUnits AS ru ON ru.Id = b.ResidentialUnitId
        WHERE  ru.ResidentialNeighborhoodId = @NeighborhoodId;
    END
    ELSE
    BEGIN
        INSERT INTO #FilteredFamilies (Id, FamilyCatgoryId, HousingType)
        SELECT f.Id, f.FamilyCatgoryId, f.HousingType
        FROM   dbo.Families AS f;
    END;

    -- =========================================================
    -- Step 2: Family members belonging to filtered families
    --         Extra index on PersonId for the Person join (Step 3)
    -- =========================================================
    CREATE TABLE #FilteredFamilyMembers
    (
        Id       INT NOT NULL PRIMARY KEY CLUSTERED,
        PersonId INT NOT NULL,
        INDEX IX_FFM_PersonId NONCLUSTERED (PersonId)
    );

    INSERT INTO #FilteredFamilyMembers (Id, PersonId)
    SELECT fm.Id, fm.PersonId
    FROM   dbo.FamilyMembers    AS fm
    INNER JOIN #FilteredFamilies AS ff ON ff.Id = fm.FamilyId;

    -- =========================================================
    -- Step 3: Distinct persons in scope
    -- =========================================================
    CREATE TABLE #FilteredPersons
    (
        Id INT NOT NULL PRIMARY KEY CLUSTERED
    );

    INSERT INTO #FilteredPersons (Id)
    SELECT DISTINCT ffm.PersonId
    FROM   #FilteredFamilyMembers AS ffm;

    -- =========================================================
    -- Step 4: Projects in scope
    --         Family-linked projects first, then block-linked
    --         (mirrors original LINQ Union logic exactly)
    -- =========================================================
    CREATE TABLE #FilteredProjects
    (
        Id INT NOT NULL PRIMARY KEY CLUSTERED
    );

    -- 4a. Projects linked to any family in scope via ProjectFamilies
    INSERT INTO #FilteredProjects (Id)
    SELECT DISTINCT pf.ProjectID
    FROM   dbo.ProjectFamilies  AS pf
    INNER JOIN #FilteredFamilies AS ff ON ff.Id = pf.FamilyID;

    -- 4b. Projects linked via blocks (honours the active filter, avoids duplicates)
    IF @ResidentialUnitId IS NOT NULL
    BEGIN
        INSERT INTO #FilteredProjects (Id)
        SELECT DISTINCT pb.ProjectId
        FROM   dbo.ProjectBlock AS pb
        INNER JOIN dbo.Blocks   AS b  ON b.Id = pb.BlockId
        WHERE  b.ResidentialUnitId = @ResidentialUnitId
          AND  NOT EXISTS (SELECT 1 FROM #FilteredProjects fp WHERE fp.Id = pb.ProjectId);
    END
    ELSE IF @NeighborhoodId IS NOT NULL
    BEGIN
        INSERT INTO #FilteredProjects (Id)
        SELECT DISTINCT pb.ProjectId
        FROM   dbo.ProjectBlock         AS pb
        INNER JOIN dbo.Blocks           AS b  ON b.Id  = pb.BlockId
        INNER JOIN dbo.ResidentialUnits AS ru ON ru.Id = b.ResidentialUnitId
        WHERE  ru.ResidentialNeighborhoodId = @NeighborhoodId
          AND  NOT EXISTS (SELECT 1 FROM #FilteredProjects fp WHERE fp.Id = pb.ProjectId);
    END
    ELSE
    BEGIN
        -- No filter: union in all block-linked projects not already in the set
        INSERT INTO #FilteredProjects (Id)
        SELECT DISTINCT pb.ProjectId
        FROM   dbo.ProjectBlock AS pb
        WHERE  NOT EXISTS (SELECT 1 FROM #FilteredProjects fp WHERE fp.Id = pb.ProjectId);
    END;

    -- =========================================================
    -- Step 5: Conflict cases that involve any family member in scope
    --         UNION on FirstPartyId / SecondPartyId avoids the
    --         correlated OR-EXISTS pattern, enabling two efficient
    --         index seeks instead of one row-by-row scan.
    -- =========================================================
    CREATE TABLE #RelevantConflicts
    (
        ConflictTypeId INT NOT NULL,
        IsResolved     BIT NOT NULL,
        INDEX IX_RC_Type NONCLUSTERED (ConflictTypeId, IsResolved)
    );

    INSERT INTO #RelevantConflicts (ConflictTypeId, IsResolved)
    SELECT cc.ConflictTypeId, cc.IsResolved
    FROM   dbo.ConfilctCases       AS cc
    INNER JOIN #FilteredFamilyMembers AS ffm ON ffm.Id = cc.FirstPartyId

    UNION   -- UNION (not UNION ALL) deduplicates cases where both parties are in scope

    SELECT cc.ConflictTypeId, cc.IsResolved
    FROM   dbo.ConfilctCases       AS cc
    INNER JOIN #FilteredFamilyMembers AS ffm ON ffm.Id = cc.SecondPartyId;

    -- =========================================================
    -- Final SELECT: all stats in a single pass per sub-aggregate
    -- CROSS JOIN is safe because each sub-query always returns
    -- exactly one row (aggregates with no GROUP BY).
    -- =========================================================
    SELECT
        -- Social & Family
        COALESCE(fa.FamiliesCount,          0)  AS FamiliesCount,
        COALESCE(pa.IndividualsCount,        0)  AS IndividualsCount,
        COALESCE(pa.DivorcedCount,           0)  AS DivorcedCount,
        COALESCE(pa.WidowsCount,             0)  AS WidowsCount,

        -- Project-family agreements (ProjectFamilies × Project.ProjectStatus)
        COALESCE(aa.AgreementsCompleted,     0)  AS AgreementsCompleted,
        COALESCE(aa.AgreementsNotCompleted,  0)  AS AgreementsNotCompleted,

        -- Conflict cases broken down by ConflictType
        --   1 = صلح   (Peace)
        --   2 = معاهدات (Treaties)
        --   3 = اتفاقيات (Agreements)
        COALESCE(ca.PeaceCompleted,            0) AS PeaceCompleted,
        COALESCE(ca.PeaceNotCompleted,         0) AS PeaceNotCompleted,
        COALESCE(ca.TreatiesCompleted,         0) AS TreatiesCompleted,
        COALESCE(ca.TreatiesNotCompleted,      0) AS TreatiesNotCompleted,
        COALESCE(ca.AgreementsTypeCompleted,   0) AS AgreementsTypeCompleted,
        COALESCE(ca.AgreementsTypeNotCompleted,0) AS AgreementsTypeNotCompleted,

        -- Projects
        --   • If ANY projects are in scope → use the scoped counts.
        --   • If no projects are linked AND no filter is active → global counts.
        --   • If no projects are linked AND a filter is active → 0.
        CASE
            WHEN EXISTS (SELECT 1 FROM #FilteredProjects)
                THEN COALESCE(prj.FilteredProjectsCompleted,   0)
            WHEN @NeighborhoodId IS NULL AND @ResidentialUnitId IS NULL
                THEN COALESCE(gprj.AllProjectsCompleted,       0)
            ELSE 0
        END AS ProjectsCompleted,
        CASE
            WHEN EXISTS (SELECT 1 FROM #FilteredProjects)
                THEN COALESCE(prj.FilteredProjectsIncomplete,  0)
            WHEN @NeighborhoodId IS NULL AND @ResidentialUnitId IS NULL
                THEN COALESCE(gprj.AllProjectsIncomplete,      0)
            ELSE 0
        END AS ProjectsIncomplete,

        -- Teams (distinct teams from scoped projects + scoped persons)
        COALESCE(ta.TeamsCount,              0)  AS TeamsCount,

        -- Population
        COALESCE(pa.ResidentsCount,          0)  AS ResidentsCount,
        COALESCE(pa.DisplacedCount,          0)  AS DisplacedCount,

        -- Income categories (FamilyCatgory seeded: 1=A, 2=B, 3=C)
        COALESCE(fa.CategoryACount,          0)  AS CategoryACount,
        COALESCE(fa.CategoryBCount,          0)  AS CategoryBCount,
        COALESCE(fa.CategoryCCount,          0)  AS CategoryCCount,

        -- Health
        COALESCE(pa.ChronicDiseasesCount,    0)  AS ChronicDiseasesCount,

        -- Housing (HousingType: 1=Rent, 2=Property)
        COALESCE(fa.HousingRentedCount,      0)  AS HousingRentedCount,
        COALESCE(fa.HousingOwnedCount,       0)  AS HousingOwnedCount

    FROM
    (
        -- Family-level aggregates
        SELECT
            COUNT(1)                                                     AS FamiliesCount,
            SUM(CASE WHEN ff.FamilyCatgoryId = 1 THEN 1 ELSE 0 END)    AS CategoryACount,
            SUM(CASE WHEN ff.FamilyCatgoryId = 2 THEN 1 ELSE 0 END)    AS CategoryBCount,
            SUM(CASE WHEN ff.FamilyCatgoryId = 3 THEN 1 ELSE 0 END)    AS CategoryCCount,
            SUM(CASE WHEN ff.HousingType     = 1 THEN 1 ELSE 0 END)    AS HousingRentedCount,
            SUM(CASE WHEN ff.HousingType     = 2 THEN 1 ELSE 0 END)    AS HousingOwnedCount
        FROM #FilteredFamilies AS ff
    ) AS fa

    CROSS JOIN
    (
        -- Person-level aggregates
        -- MaritalStatus  : 1=Single, 2=Married, 3=Divorced, 4=Widowed
        -- ResidencyStatus: 0=Resident, 1=Displaced
        SELECT
            COUNT(1)                                                        AS IndividualsCount,
            SUM(CASE WHEN p.MaritalStatus   = 3  THEN 1 ELSE 0 END)       AS DivorcedCount,
            SUM(CASE WHEN p.MaritalStatus   = 4  THEN 1 ELSE 0 END)       AS WidowsCount,
            SUM(CASE WHEN p.ResidencyStatus = 0  THEN 1 ELSE 0 END)       AS ResidentsCount,
            SUM(CASE WHEN p.ResidencyStatus = 1  THEN 1 ELSE 0 END)       AS DisplacedCount,
            SUM(CASE WHEN p.HasChronicDiseases = 1 THEN 1 ELSE 0 END)     AS ChronicDiseasesCount
        FROM #FilteredPersons AS fp
        INNER JOIN dbo.People AS p ON p.Id = fp.Id
    ) AS pa

    CROSS JOIN
    (
        -- Project-family agreement stats
        -- ProjectStatus: 0=Planned, 1=InProgress, 2=Completed, 3=Cancelled
        SELECT
            SUM(CASE WHEN p.ProjectStatus =  2 THEN 1 ELSE 0 END)         AS AgreementsCompleted,
            SUM(CASE WHEN p.ProjectStatus <> 2 THEN 1 ELSE 0 END)         AS AgreementsNotCompleted
        FROM dbo.ProjectFamilies AS pf
        INNER JOIN #FilteredFamilies AS ff ON ff.Id  = pf.FamilyID
        INNER JOIN dbo.Projects      AS p  ON p.Id   = pf.ProjectID
    ) AS aa

    CROSS JOIN
    (
        -- Conflict case breakdown (from pre-materialised #RelevantConflicts)
        SELECT
            SUM(CASE WHEN m.ConflictTypeId = 1 AND m.IsResolved = 1 THEN 1 ELSE 0 END) AS PeaceCompleted,
            SUM(CASE WHEN m.ConflictTypeId = 1 AND m.IsResolved = 0 THEN 1 ELSE 0 END) AS PeaceNotCompleted,
            SUM(CASE WHEN m.ConflictTypeId = 2 AND m.IsResolved = 1 THEN 1 ELSE 0 END) AS TreatiesCompleted,
            SUM(CASE WHEN m.ConflictTypeId = 2 AND m.IsResolved = 0 THEN 1 ELSE 0 END) AS TreatiesNotCompleted,
            SUM(CASE WHEN m.ConflictTypeId = 3 AND m.IsResolved = 1 THEN 1 ELSE 0 END) AS AgreementsTypeCompleted,
            SUM(CASE WHEN m.ConflictTypeId = 3 AND m.IsResolved = 0 THEN 1 ELSE 0 END) AS AgreementsTypeNotCompleted
        FROM #RelevantConflicts AS m
    ) AS ca

    CROSS JOIN
    (
        -- Scoped project stats (projects in #FilteredProjects)
        SELECT
            SUM(CASE WHEN p.ProjectStatus =  2 THEN 1 ELSE 0 END)         AS FilteredProjectsCompleted,
            SUM(CASE WHEN p.ProjectStatus <> 2 THEN 1 ELSE 0 END)         AS FilteredProjectsIncomplete
        FROM dbo.Projects        AS p
        INNER JOIN #FilteredProjects AS fp ON fp.Id = p.Id
    ) AS prj

    CROSS JOIN
    (
        -- Global project stats (fallback when no filter AND no linked projects)
        SELECT
            SUM(CASE WHEN p.ProjectStatus =  2 THEN 1 ELSE 0 END)         AS AllProjectsCompleted,
            SUM(CASE WHEN p.ProjectStatus <> 2 THEN 1 ELSE 0 END)         AS AllProjectsIncomplete
        FROM dbo.Projects AS p
    ) AS gprj

    CROSS JOIN
    (
        -- Distinct teams reachable from scoped projects OR scoped persons
        SELECT COUNT(1) AS TeamsCount
        FROM
        (
            SELECT pt.TeamId
            FROM   dbo.ProjectTeams AS pt
            INNER JOIN #FilteredProjects AS fp ON fp.Id = pt.ProjectId

            UNION

            SELECT tm.TeamId
            FROM   dbo.TeamMembers AS tm
            INNER JOIN #FilteredPersons AS fp ON fp.Id = tm.PersonId
        ) AS teams
    ) AS ta;
END;
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert to the original (pre-refactor) version of the stored procedure
            migrationBuilder.Sql(@"
CREATE OR ALTER PROCEDURE [dbo].[sp_GetDashboardStatus]
    @NeighborhoodId INT = NULL,
    @ResidentialUnitId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    CREATE TABLE #FilteredFamilies
    (
        Id INT NOT NULL PRIMARY KEY,
        FamilyCatgoryId INT NOT NULL,
        HousingType INT NOT NULL
    );

    IF @ResidentialUnitId IS NOT NULL
    BEGIN
        INSERT INTO #FilteredFamilies (Id, FamilyCatgoryId, HousingType)
        SELECT f.Id, f.FamilyCatgoryId, f.HousingType
        FROM dbo.Families AS f
        INNER JOIN dbo.Blocks AS b ON b.Id = f.BlockId
        WHERE b.ResidentialUnitId = @ResidentialUnitId;
    END
    ELSE IF @NeighborhoodId IS NOT NULL
    BEGIN
        INSERT INTO #FilteredFamilies (Id, FamilyCatgoryId, HousingType)
        SELECT f.Id, f.FamilyCatgoryId, f.HousingType
        FROM dbo.Families AS f
        INNER JOIN dbo.Blocks AS b ON b.Id = f.BlockId
        INNER JOIN dbo.ResidentialUnits AS ru ON ru.Id = b.ResidentialUnitId
        WHERE ru.ResidentialNeighborhoodId = @NeighborhoodId;
    END
    ELSE
    BEGIN
        INSERT INTO #FilteredFamilies (Id, FamilyCatgoryId, HousingType)
        SELECT f.Id, f.FamilyCatgoryId, f.HousingType
        FROM dbo.Families AS f;
    END;

    CREATE TABLE #FilteredFamilyMembers (Id INT NOT NULL PRIMARY KEY, PersonId INT NOT NULL);
    INSERT INTO #FilteredFamilyMembers (Id, PersonId)
    SELECT fm.Id, fm.PersonId FROM dbo.FamilyMembers AS fm INNER JOIN #FilteredFamilies AS ff ON ff.Id = fm.FamilyId;

    CREATE TABLE #FilteredPersons (Id INT NOT NULL PRIMARY KEY);
    INSERT INTO #FilteredPersons (Id) SELECT DISTINCT ffm.PersonId FROM #FilteredFamilyMembers AS ffm;

    CREATE TABLE #FilteredProjects (Id INT NOT NULL PRIMARY KEY);
    INSERT INTO #FilteredProjects (Id)
    SELECT DISTINCT pf.ProjectID FROM dbo.ProjectFamilies AS pf INNER JOIN #FilteredFamilies AS ff ON ff.Id = pf.FamilyID;

    SELECT
        COALESCE(fa.FamiliesCount, 0) AS FamiliesCount,
        COALESCE(pa.IndividualsCount, 0) AS IndividualsCount,
        COALESCE(pa.DivorcedCount, 0) AS DivorcedCount,
        COALESCE(pa.WidowsCount, 0) AS WidowsCount,
        COALESCE(aa.AgreementsCompleted, 0) AS AgreementsCompleted,
        COALESCE(aa.AgreementsNotCompleted, 0) AS AgreementsNotCompleted,
        COALESCE(ca.PeaceCompleted, 0) AS PeaceCompleted,
        COALESCE(ca.PeaceNotCompleted, 0) AS PeaceNotCompleted,
        COALESCE(ca.TreatiesCompleted, 0) AS TreatiesCompleted,
        COALESCE(ca.TreatiesNotCompleted, 0) AS TreatiesNotCompleted,
        COALESCE(ca.AgreementsTypeCompleted, 0) AS AgreementsTypeCompleted,
        COALESCE(ca.AgreementsTypeNotCompleted, 0) AS AgreementsTypeNotCompleted,
        CASE WHEN EXISTS (SELECT 1 FROM #FilteredProjects) THEN COALESCE(prj.FilteredProjectsCompleted, 0)
             WHEN @NeighborhoodId IS NULL AND @ResidentialUnitId IS NULL THEN COALESCE(gprj.AllProjectsCompleted, 0)
             ELSE 0 END AS ProjectsCompleted,
        CASE WHEN EXISTS (SELECT 1 FROM #FilteredProjects) THEN COALESCE(prj.FilteredProjectsIncomplete, 0)
             WHEN @NeighborhoodId IS NULL AND @ResidentialUnitId IS NULL THEN COALESCE(gprj.AllProjectsIncomplete, 0)
             ELSE 0 END AS ProjectsIncomplete,
        COALESCE(ta.TeamsCount, 0) AS TeamsCount,
        COALESCE(pa.ResidentsCount, 0) AS ResidentsCount,
        COALESCE(pa.DisplacedCount, 0) AS DisplacedCount,
        COALESCE(fa.CategoryACount, 0) AS CategoryACount,
        COALESCE(fa.CategoryBCount, 0) AS CategoryBCount,
        COALESCE(fa.CategoryCCount, 0) AS CategoryCCount,
        COALESCE(pa.ChronicDiseasesCount, 0) AS ChronicDiseasesCount,
        COALESCE(fa.HousingRentedCount, 0) AS HousingRentedCount,
        COALESCE(fa.HousingOwnedCount, 0) AS HousingOwnedCount
    FROM (SELECT COUNT(1) AS FamiliesCount, SUM(CASE WHEN ff.FamilyCatgoryId=1 THEN 1 ELSE 0 END) AS CategoryACount, SUM(CASE WHEN ff.FamilyCatgoryId=2 THEN 1 ELSE 0 END) AS CategoryBCount, SUM(CASE WHEN ff.FamilyCatgoryId=3 THEN 1 ELSE 0 END) AS CategoryCCount, SUM(CASE WHEN ff.HousingType=1 THEN 1 ELSE 0 END) AS HousingRentedCount, SUM(CASE WHEN ff.HousingType=2 THEN 1 ELSE 0 END) AS HousingOwnedCount FROM #FilteredFamilies AS ff) AS fa
    CROSS JOIN (SELECT COUNT(1) AS IndividualsCount, SUM(CASE WHEN p.MaritalStatus=3 THEN 1 ELSE 0 END) AS DivorcedCount, SUM(CASE WHEN p.MaritalStatus=4 THEN 1 ELSE 0 END) AS WidowsCount, SUM(CASE WHEN p.ResidencyStatus=0 THEN 1 ELSE 0 END) AS ResidentsCount, SUM(CASE WHEN p.ResidencyStatus=1 THEN 1 ELSE 0 END) AS DisplacedCount, SUM(CASE WHEN p.HasChronicDiseases=1 THEN 1 ELSE 0 END) AS ChronicDiseasesCount FROM #FilteredPersons AS fp INNER JOIN dbo.People AS p ON p.Id = fp.Id) AS pa
    CROSS JOIN (SELECT SUM(CASE WHEN p.ProjectStatus=2 THEN 1 ELSE 0 END) AS AgreementsCompleted, SUM(CASE WHEN p.ProjectStatus<>2 THEN 1 ELSE 0 END) AS AgreementsNotCompleted FROM dbo.ProjectFamilies AS pf INNER JOIN #FilteredFamilies AS ff ON ff.Id=pf.FamilyID INNER JOIN dbo.Projects AS p ON p.Id=pf.ProjectID) AS aa
    CROSS JOIN (SELECT SUM(CASE WHEN cc.ConflictTypeId=1 AND cc.IsResolved=1 THEN 1 ELSE 0 END) AS PeaceCompleted, SUM(CASE WHEN cc.ConflictTypeId=1 AND cc.IsResolved=0 THEN 1 ELSE 0 END) AS PeaceNotCompleted, SUM(CASE WHEN cc.ConflictTypeId=2 AND cc.IsResolved=1 THEN 1 ELSE 0 END) AS TreatiesCompleted, SUM(CASE WHEN cc.ConflictTypeId=2 AND cc.IsResolved=0 THEN 1 ELSE 0 END) AS TreatiesNotCompleted, SUM(CASE WHEN cc.ConflictTypeId=3 AND cc.IsResolved=1 THEN 1 ELSE 0 END) AS AgreementsTypeCompleted, SUM(CASE WHEN cc.ConflictTypeId=3 AND cc.IsResolved=0 THEN 1 ELSE 0 END) AS AgreementsTypeNotCompleted FROM dbo.ConfilctCases AS cc WHERE EXISTS (SELECT 1 FROM #FilteredFamilyMembers AS ffm WHERE ffm.Id=cc.FirstPartyId) OR EXISTS (SELECT 1 FROM #FilteredFamilyMembers AS ffm WHERE ffm.Id=cc.SecondPartyId)) AS ca
    CROSS JOIN (SELECT SUM(CASE WHEN p.ProjectStatus=2 THEN 1 ELSE 0 END) AS FilteredProjectsCompleted, SUM(CASE WHEN p.ProjectStatus<>2 THEN 1 ELSE 0 END) AS FilteredProjectsIncomplete FROM dbo.Projects AS p INNER JOIN #FilteredProjects AS fp ON fp.Id=p.Id) AS prj
    CROSS JOIN (SELECT SUM(CASE WHEN p.ProjectStatus=2 THEN 1 ELSE 0 END) AS AllProjectsCompleted, SUM(CASE WHEN p.ProjectStatus<>2 THEN 1 ELSE 0 END) AS AllProjectsIncomplete FROM dbo.Projects AS p) AS gprj
    CROSS JOIN (SELECT COUNT(1) AS TeamsCount FROM (SELECT pt.TeamId FROM dbo.ProjectTeams AS pt INNER JOIN #FilteredProjects AS fp ON fp.Id=pt.ProjectId UNION SELECT tm.TeamId FROM dbo.TeamMembers AS tm INNER JOIN #FilteredPersons AS fp ON fp.Id=tm.PersonId) AS teams) AS ta;
END;
");
        }
    }
}
