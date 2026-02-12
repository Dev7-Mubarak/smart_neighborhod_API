using Microsoft.Identity.Client;

namespace SmartNeighborhoodAPI.Helpers
{
    public static class Router
    {
        public const string Root = "api";
        public const string Rule = Root + "/";

        public static class ResidentialUnits
        {
            public const string Prefix = Rule + "residential-units";

            public const string Add = Prefix;
            public const string GetAll = Prefix;
            public const string GetById = Prefix + "/{id:int}";
            public const string Update = Prefix + "/{id:int}";
            public const string Delete = Prefix + "/{id:int}";
            public const string ChangeManager = Prefix + "/{id:int}/manager";
            public const string Dashboard = Prefix + "/dashboard";
            public const string Units = Prefix + "/{id:int}/units";

            public const string Blocks = Prefix + "/{unitId:int}/blocks";             
            public const string BlockById = Blocks + "/{blockId:int}";
            
            public const string GetMyUnits = Prefix + "/me";
            public const string GetMyDashboard = Prefix + "/me/dashboard";
            
            public const string GetBlocks = Prefix + "/{id:int}/blocks";
        }

        public static class ResidentialNeighborhoods
        {
            public const string Prefix = Rule + "residential-neighborhoods";

            public const string Add = Prefix;
            public const string GetAll = Prefix;
            public const string GetById = Prefix + "/{id:int}";
            public const string Update = Prefix + "/{id:int}";
            public const string Delete = Prefix + "/{id:int}";
            public const string ChangeManager = Prefix + "/{id:int}/manager";
            public const string Dashboard = Prefix + "/dashboard";
            public const string Units = Prefix + "/{id:int}/units";
            
            public const string GetMyNeighborhoods = Prefix + "/me";
            public const string GetMyDashboard = Prefix + "/me/dashboard";
        }

        public static class Blocks
        {
            public const string Prefix = Rule + "residential-blocks";

            public const string Add = Prefix;
            public const string ChangeManager = Prefix + "/{id:int}/manager";
            public const string GetAll = Prefix;
            public const string GetById = Prefix + "/{id:int}";
            public const string Update = Prefix + "/{id:int}";
            public const string Delete = Prefix + "/{id:int}";
            public const string Dashboard = Prefix + "/dashboard";
            public const string GetFamilies = Prefix + "/{id:int}/families";
            
            public const string GetMyBlocks = Prefix + "/me";
            public const string GetMyDashboard = Prefix + "/me/dashboard";
        }
        public static class GovernmentInstitutions
        {
            public const string Prefix = Rule + "GovernmentInstitutions";
            public const string GetAll = Prefix;
            public const string GetById = Prefix + "/{id:int}";
            public const string Add = Prefix;
            public const string Update = Prefix + "/{id:int}";
            public const string Delete = Prefix + "/{id:int}";
        }
        public static class GovernmentInstitutionContacts
        {
            public const string Prefix = Rule + "GovernmentInstitutionContacts";

            public const string GetAllByAuthority = Prefix + "/authority/{authorityId:int}";

            public const string GetById = Prefix + "/{id:int}";
            public const string AddToAuthority = Prefix + "/authority/{authorityId:int}";
            public const string Update = Prefix + "/{id:int}";
            public const string Delete = Prefix + "/{id:int}";
        }
        public static class ConflictCaseTypes
        {
            public const string Prefix = Rule + "conflict-case-type";

            public const string Add = Prefix;
            public const string GetAll = Prefix;
            public const string GetById = Prefix + "/{id:int}";
            public const string Update = Prefix + "/{id:int}";
            public const string Delete = Prefix + "/{id:int}";
        }

        public static class ConflictCases
        {
            public const string Prefix = Rule + "conflict-cases";

            public const string Add = Prefix;
            public const string GetAll = Prefix;
            public const string GetById = Prefix + "{id:int}";
            public const string Update = Prefix + "/{id:int}";
            public const string Delete = Prefix + "/{id:int}";
            public const string GetByFamilyMember = Prefix + "/{familyMemberId}";
        }

        public static class FamilyCategories
        {
            public const string Prefix = Rule + "family-categories";

            public const string Add = Prefix;
            public const string GetAll = Prefix;
            public const string GetById = Prefix + "/{id:int}";
            public const string Update = Prefix + "/{id:int}";
            public const string Delete = Prefix + "/{id:int}";
        }

        public static class Families
        {
            public const string Prefix = Rule + "families";

            public const string Add = Prefix;
            public const string GetAll = Prefix;
            public const string GetDetails = Prefix + "/details";
            public const string GetById = Prefix + "{id:int}";
            public const string Update = Prefix + "/{id:int}";
            public const string Delete = Prefix + "/{id:int}";
        }

        public static class FamilyMembers
        {
            public const string Prefix = Rule + "family-members";

            public const string Add = Prefix;
            public const string GetAll = Prefix;
            public const string GetById = Prefix + "/{id:int}";
            public const string Update = Prefix + "/{id:int}";
            public const string Delete = Prefix + "/{id:int}";
        }

        public static class MemberFamilyRoles
        {
            public const string Prefix = Rule + "member-family-roles";

            public const string Add = Prefix;
            public const string GetAll = Prefix;
            public const string GetById = Prefix + "/{id:int}";
            public const string Update = Prefix + "/{id:int}";
            public const string Delete = Prefix + "/{id:int}";
        }

        public static class Persons
        {
            public const string Prefix = Rule + "person";

            public const string GetAll = Prefix;
            public const string GetById = Prefix + "/{id:int}";
            public const string Add = Prefix;
            public const string Update = Prefix + "/{id:int}";
            public const string Delete = Prefix + "/{id:int}";
        }

        public static class ProjectCategories
        {
            public const string Prefix = Rule + "project-categories";

            public const string GetAll = Prefix;
            public const string GetById = Prefix + "/{id:int}";
            public const string Add = Prefix;
            public const string Update = Prefix + "/{id:int}";
            public const string Delete = Prefix + "/{id:int}";
        }

        public static class Projects
        {
            public const string Prefix = Rule + "projects";

            public const string GetAll = Prefix;
            public const string GetById = Prefix + "/{id:int}";
            public const string Add = Prefix;
            public const string Update = Prefix + "/{id:int}";
            public const string ChangeStatus = Prefix + "/{id:int}/status";
            public const string Delete = Prefix + "/{id:int}";

            // Teams
            public const string GetTeams = Prefix + "/{projectId:int}/teams";
            public const string AssignTeam = Prefix + "/{projectId:int}/teams/{teamId:int}";         
            public const string RemoveTeam = Prefix + "/{projectId:int}/teams/{teamId:int}";

            // Families
            public const string GetFamilies = Prefix + "/{projectId:int}/families";
            public const string AssignFamily = Prefix + "/{projectId:int}/families/{familyId:int}";               
            public const string RemoveFamily = Prefix + "/{projectId:int}/families/{familyId:int}"; 

            // Project Blocks with Beneficiary Families
            public const string GetProjectBlocksWithBeneficiaryFamilies =
                Prefix + "/{projectId:int}/blocks-with-families"; 
        }



        // -------------------- PROJECT FAMILY --------------------
        public static class ProjectFamilies
        {
            public const string Prefix = Rule + "project-families";

            public const string GetAll = Prefix;
            public const string GetById = Prefix + "/{id:int}";
            public const string Add = Prefix;
            public const string Update = Prefix + "/{id:int}";
            public const string Delete = Prefix + "/{id:int}";
        }

        public static class TeamMembers
        {
            public const string Prefix = Rule + "team-members";

            public const string GetAll = Prefix;
            public const string GetById = Prefix + "/{id:int}";
            public const string Add = Prefix;
            public const string Update = Prefix + "/{id:int}";
            public const string Delete = Prefix + "/{id:int}";
        }

        public static class TeamRoles
        {
            public const string Prefix = Rule + "team-roles";

            public const string GetAll = Prefix;
            public const string GetById = Prefix + "/{id:int}";
            public const string Add = Prefix;
            public const string Update = Prefix + "/{id:int}";
            public const string Delete = Prefix + "/{id:int}";
        }

        // -------------------- TEAMS --------------------
        public static class Teams
        {
            public const string Prefix = Rule + "teams";

            public const string Add = Prefix;
            public const string GetAll = Prefix;
            public const string GetById = Prefix + "/{id:int}";
            public const string Update = Prefix + "/{id:int}";
            public const string Delete = Prefix + "/{id:int}";
            public const string GetTeamProjects = Prefix + "/{teamId:int}/projects";
        }

        public static class Enums
        {
            public const string Prefix = Rule;

            public const string MaritalStatuses = Prefix + "marital-statuses";
            public const string BloodTypes = Prefix + "blood-types";
            public const string IdentityTypes = Prefix + "identity-types";
            public const string OccupationStatuses = Prefix + "occupation-statuses";
            public const string Gender = Prefix + "gender";
            public const string ProjectStatus = Prefix + "project-status";
            public const string ProjectPriority = Prefix + "project-priority";
        }
        public static class Auth
        {
            public const string Prefix = Rule + "auth";

            public const string Login = Prefix + "/login";
            public const string Register = Prefix + "/register";

            public static class PasswordReset
            {
                public const string Prefix = Auth.Prefix + "/reset-password";

                public const string SendCode = Prefix + "/send-code";
                public const string VerifyCode = Prefix + "/verify-code";
                public const string Confirm = Prefix + "/confirm";
            }

            public const string ConfirmEmailOtp = Prefix + "/confirm-email-otp";
        }

        public static class Issues
        {
            public const string Prefix = Rule + "issues";

            public const string GetAll = Prefix;
            public const string GetById = Prefix + "/{id:guid}";
            public const string Add = Prefix;
            public const string Update = Prefix + "/{id:guid}";
            public const string Delete = Prefix + "/{id:guid}";
        }

        public static class Sync
        {
            public const string Prefix = Rule + "sync";

            public const string Pull = Prefix + "/pull";
            public const string Push = Prefix + "/push";
            public const string Status = Prefix + "/status";
        }
    }
}

