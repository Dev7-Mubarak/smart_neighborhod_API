using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260307120000_AddSpGetDashboardStatus")]
    public partial class AddSpGetDashboardStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

    CREATE TABLE #FilteredFamilyMembers
    (
        Id INT NOT NULL PRIMARY KEY,
        PersonId INT NOT NULL
    );

    INSERT INTO #FilteredFamilyMembers (Id, PersonId)
    SELECT fm.Id, fm.PersonId
    FROM dbo.FamilyMembers AS fm
    INNER JOIN #FilteredFamilies AS ff ON ff.Id = fm.FamilyId;

    CREATE TABLE #FilteredPersons
    (
        Id INT NOT NULL PRIMARY KEY
    );

    INSERT INTO #FilteredPersons (Id)
    SELECT DISTINCT ffm.PersonId
    FROM #FilteredFamilyMembers AS ffm;

    CREATE TABLE #FilteredProjects
    (
        Id INT NOT NULL PRIMARY KEY
    );

    INSERT INTO #FilteredProjects (Id)
    SELECT DISTINCT pf.ProjectID
    FROM dbo.ProjectFamilies AS pf
    INNER JOIN #FilteredFamilies AS ff ON ff.Id = pf.FamilyID;

    IF @ResidentialUnitId IS NOT NULL
    BEGIN
        INSERT INTO #FilteredProjects (Id)
        SELECT DISTINCT pb.ProjectId
        FROM dbo.ProjectBlock AS pb
        INNER JOIN dbo.Blocks AS b ON b.Id = pb.BlockId
        WHERE b.ResidentialUnitId = @ResidentialUnitId
          AND NOT EXISTS (SELECT 1 FROM #FilteredProjects AS fp WHERE fp.Id = pb.ProjectId);
    END
    ELSE IF @NeighborhoodId IS NOT NULL
    BEGIN
        INSERT INTO #FilteredProjects (Id)
        SELECT DISTINCT pb.ProjectId
        FROM dbo.ProjectBlock AS pb
        INNER JOIN dbo.Blocks AS b ON b.Id = pb.BlockId
        INNER JOIN dbo.ResidentialUnits AS ru ON ru.Id = b.ResidentialUnitId
        WHERE ru.ResidentialNeighborhoodId = @NeighborhoodId
          AND NOT EXISTS (SELECT 1 FROM #FilteredProjects AS fp WHERE fp.Id = pb.ProjectId);
    END
    ELSE
    BEGIN
        INSERT INTO #FilteredProjects (Id)
        SELECT DISTINCT pb.ProjectId
        FROM dbo.ProjectBlock AS pb
        WHERE NOT EXISTS (SELECT 1 FROM #FilteredProjects AS fp WHERE fp.Id = pb.ProjectId);
    END;

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
        CASE
            WHEN EXISTS (SELECT 1 FROM #FilteredProjects) THEN COALESCE(prj.FilteredProjectsCompleted, 0)
            WHEN @NeighborhoodId IS NULL AND @ResidentialUnitId IS NULL THEN COALESCE(gprj.AllProjectsCompleted, 0)
            ELSE 0
        END AS ProjectsCompleted,
        CASE
            WHEN EXISTS (SELECT 1 FROM #FilteredProjects) THEN COALESCE(prj.FilteredProjectsIncomplete, 0)
            WHEN @NeighborhoodId IS NULL AND @ResidentialUnitId IS NULL THEN COALESCE(gprj.AllProjectsIncomplete, 0)
            ELSE 0
        END AS ProjectsIncomplete,
        COALESCE(ta.TeamsCount, 0) AS TeamsCount,
        COALESCE(pa.ResidentsCount, 0) AS ResidentsCount,
        COALESCE(pa.DisplacedCount, 0) AS DisplacedCount,
        COALESCE(fa.CategoryACount, 0) AS CategoryACount,
        COALESCE(fa.CategoryBCount, 0) AS CategoryBCount,
        COALESCE(fa.CategoryCCount, 0) AS CategoryCCount,
        COALESCE(pa.ChronicDiseasesCount, 0) AS ChronicDiseasesCount,
        COALESCE(fa.HousingRentedCount, 0) AS HousingRentedCount,
        COALESCE(fa.HousingOwnedCount, 0) AS HousingOwnedCount
    FROM
    (
        SELECT
            COUNT(1) AS FamiliesCount,
            SUM(CASE WHEN ff.FamilyCatgoryId = 1 THEN 1 ELSE 0 END) AS CategoryACount,
            SUM(CASE WHEN ff.FamilyCatgoryId = 2 THEN 1 ELSE 0 END) AS CategoryBCount,
            SUM(CASE WHEN ff.FamilyCatgoryId = 3 THEN 1 ELSE 0 END) AS CategoryCCount,
            SUM(CASE WHEN ff.HousingType = 1 THEN 1 ELSE 0 END) AS HousingRentedCount,
            SUM(CASE WHEN ff.HousingType = 2 THEN 1 ELSE 0 END) AS HousingOwnedCount
        FROM #FilteredFamilies AS ff
    ) AS fa
    CROSS JOIN
    (
        SELECT
            COUNT(1) AS IndividualsCount,
            SUM(CASE WHEN p.MaritalStatus = 3 THEN 1 ELSE 0 END) AS DivorcedCount,
            SUM(CASE WHEN p.MaritalStatus = 4 THEN 1 ELSE 0 END) AS WidowsCount,
            SUM(CASE WHEN p.ResidencyStatus = 0 THEN 1 ELSE 0 END) AS ResidentsCount,
            SUM(CASE WHEN p.ResidencyStatus = 1 THEN 1 ELSE 0 END) AS DisplacedCount,
            SUM(CASE WHEN p.HasChronicDiseases = 1 THEN 1 ELSE 0 END) AS ChronicDiseasesCount
        FROM #FilteredPersons AS fp
        INNER JOIN dbo.People AS p ON p.Id = fp.Id
    ) AS pa
    CROSS JOIN
    (
        SELECT
            SUM(CASE WHEN p.ProjectStatus = 2 THEN 1 ELSE 0 END) AS AgreementsCompleted,
            SUM(CASE WHEN p.ProjectStatus <> 2 THEN 1 ELSE 0 END) AS AgreementsNotCompleted
        FROM dbo.ProjectFamilies AS pf
        INNER JOIN #FilteredFamilies AS ff ON ff.Id = pf.FamilyID
        INNER JOIN dbo.Projects AS p ON p.Id = pf.ProjectID
    ) AS aa
    CROSS JOIN
    (
        SELECT
            SUM(CASE WHEN cc.ConflictTypeId = 1 AND cc.IsResolved = 1 THEN 1 ELSE 0 END) AS PeaceCompleted,
            SUM(CASE WHEN cc.ConflictTypeId = 1 AND cc.IsResolved = 0 THEN 1 ELSE 0 END) AS PeaceNotCompleted,
            SUM(CASE WHEN cc.ConflictTypeId = 2 AND cc.IsResolved = 1 THEN 1 ELSE 0 END) AS TreatiesCompleted,
            SUM(CASE WHEN cc.ConflictTypeId = 2 AND cc.IsResolved = 0 THEN 1 ELSE 0 END) AS TreatiesNotCompleted,
            SUM(CASE WHEN cc.ConflictTypeId = 3 AND cc.IsResolved = 1 THEN 1 ELSE 0 END) AS AgreementsTypeCompleted,
            SUM(CASE WHEN cc.ConflictTypeId = 3 AND cc.IsResolved = 0 THEN 1 ELSE 0 END) AS AgreementsTypeNotCompleted
        FROM dbo.ConfilctCases AS cc
        WHERE EXISTS (SELECT 1 FROM #FilteredFamilyMembers AS ffm WHERE ffm.Id = cc.FirstPartyId)
           OR EXISTS (SELECT 1 FROM #FilteredFamilyMembers AS ffm WHERE ffm.Id = cc.SecondPartyId)
    ) AS ca
    CROSS JOIN
    (
        SELECT
            SUM(CASE WHEN p.ProjectStatus = 2 THEN 1 ELSE 0 END) AS FilteredProjectsCompleted,
            SUM(CASE WHEN p.ProjectStatus <> 2 THEN 1 ELSE 0 END) AS FilteredProjectsIncomplete
        FROM dbo.Projects AS p
        INNER JOIN #FilteredProjects AS fp ON fp.Id = p.Id
    ) AS prj
    CROSS JOIN
    (
        SELECT
            SUM(CASE WHEN p.ProjectStatus = 2 THEN 1 ELSE 0 END) AS AllProjectsCompleted,
            SUM(CASE WHEN p.ProjectStatus <> 2 THEN 1 ELSE 0 END) AS AllProjectsIncomplete
        FROM dbo.Projects AS p
    ) AS gprj
    CROSS JOIN
    (
        SELECT COUNT(1) AS TeamsCount
        FROM
        (
            SELECT pt.TeamId
            FROM dbo.ProjectTeams AS pt
            INNER JOIN #FilteredProjects AS fp ON fp.Id = pt.ProjectId

            UNION

            SELECT tm.TeamId
            FROM dbo.TeamMembers AS tm
            INNER JOIN #FilteredPersons AS fp ON fp.Id = tm.PersonId
        ) AS teams
    ) AS ta;
END;
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[dbo].[sp_GetDashboardStatus]', N'P') IS NOT NULL
BEGIN
    DROP PROCEDURE [dbo].[sp_GetDashboardStatus];
END;");
        }
    }
}