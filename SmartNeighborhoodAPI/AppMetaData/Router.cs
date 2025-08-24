using Microsoft.Identity.Client;

namespace SmartNeighborhoodAPI.AppMetaData
{
    public static class Router
    {
        public const string Root = "api";
        public const string Rule = Root + "/";

        public static class Blocks
        {
            public const string Prefix = Rule + "blocks";

            public const string GetDetails = Prefix + "/get-details";
            public const string Add = Prefix + "/add";
            public const string ChangeBlockManager = Prefix + "/change-block-manager";
            public const string GetAll = Prefix + "/get-all";
            public const string GetById = Prefix + "/get-by-id/{id:int}";
            public const string Update = Prefix + "/update/{id:int}";
            public const string Delete = Prefix + "/delete/{id:int}";
        }

        public static class ConflictCaseTypes
        {
            public const string Prefix = Rule + "conflict-case-types";

            public const string Add = Prefix + "/add";
            public const string GetAll = Prefix + "/get-all";
            public const string GetById = Prefix + "/get-by-id/{id:int}";
            public const string Update = Prefix + "/update/{id:int}";
            public const string Delete = Prefix + "/delete/{id:int}";
        }

        public static class ConflictCases
        {
            public const string Prefix = Rule + "conflict-cases";

            public const string Add = Prefix + "/add";
            public const string GetAll = Prefix + "/get-all";
            public const string GetById = Prefix + "/get-by-id/{id:int}";
            public const string Update = Prefix + "/update/{id:int}";
            public const string Delete = Prefix + "/delete/{id:int}";
            public const string GetByFamilyMember = Prefix + "/get-by-family-member/{familyMemberId}";
        }

        public static class FamilyCategories
        {
            public const string Prefix = Rule + "family-categories";

            public const string Add = Prefix + "/add";
            public const string GetAll = Prefix + "/get-all";
            public const string GetById = Prefix + "/get-by-id/{id:int}";
            public const string Update = Prefix + "/update/{id:int}";
            public const string Delete = Prefix + "/delete/{id:int}";
        }

        public static class Families
        {
            public const string Prefix = Rule + "families";

            public const string Add = Prefix + "/add";
            public const string GetAll = Prefix + "/get-all";
            public const string GetDetails = Prefix + "/get-details";
            public const string GetById = Prefix + "/get-by-id/{id:int}";
            public const string Update = Prefix + "/update/{id:int}";
            public const string Delete = Prefix + "/delete/{id:int}";
        }

        public static class FamilyMembers
        {
            public const string Prefix = Rule + "family-members";

            public const string Add = Prefix + "/add";
            public const string GetAll = Prefix + "/get-all";
            public const string GetById = Prefix + "/get-by-id/{id:int}";
            public const string Update = Prefix + "/update/{id:int}";
            public const string Delete = Prefix + "/delete/{id:int}";
        }

        public static class FamilyTypes
        {
            public const string Prefix = Rule + "family-types";

            public const string Add = Prefix + "/add";
            public const string GetAll = Prefix + "/get-all";
            public const string GetById = Prefix + "/get-by-id/{id:int}";
            public const string Update = Prefix + "/update/{id:int}";
            public const string Delete = Prefix + "/delete/{id:int}";
        }

        public static class MemberFamilyRoles
        {
            public const string Prefix = Rule + "member-family-roles";

            public const string Add = Prefix + "/add-member-type";
            public const string GetAll = Prefix + "/get-all-member-types";
            public const string GetById = Prefix + "/get-member-type-by-id/{id:int}";
            public const string Update = Prefix + "/update-member-type/{id:int}";
            public const string Delete = Prefix + "/delete-member-type/{id:int}";
        }

        public static class Persons
        {
            public const string Prefix = Rule + "persons";

            public const string GetAll = Prefix + "/get-all";
            public const string GetById = Prefix + "/get-by-id/{id:int}";
            public const string Add = Prefix + "/add";
            public const string Update = Prefix + "/update/{id:int}";
            public const string Delete = Prefix + "/delete/{id:int}";
        }

        public static class ProjectCategories
        {
            public const string Prefix = Rule + "project-categories";

            public const string GetAll = Prefix + "/get-all";
            public const string GetById = Prefix + "/get-by-id/{id:int}";
            public const string Add = Prefix + "/add";
            public const string Update = Prefix + "/update/{id:int}";
            public const string Delete = Prefix + "/delete/{id:int}";
        }

        public static class Projects
        {
            public const string Prefix = Rule + "projects";

            public const string GetAll = Prefix + "/get-all";
            public const string GetById = Prefix + "/get-by-id/{id:int}";
            public const string Add = Prefix + "/add";
            public const string Update = Prefix + "/update/{id:int}";
            public const string Delete = Prefix + "/delete/{id:int}";
            public const string AssignTeam = Prefix + "/assign-team/{projectId:int}";
            public const string RemoveTeam = Prefix + "/remove-team/{projectId:int}";
            public const string AssignFamily = Prefix + "/assign-family/{projectId:int}";
            public const string RemoveFamily = Prefix + "/remove-family/{projectId:int}";
            public const string GetProjectBlocksWithBeneficiaryFamilies =
                Prefix + "/get-project-blocks-with-beneficiary-families/{projectId:int}";
        }

        // -------------------- PROJECT FAMILY --------------------
        public static class ProjectFamilies
        {
            public const string Prefix = Rule + "project-families";

            public const string GetAll = Prefix + "/get-all";
            public const string GetById = Prefix + "/get-by-id/{id:int}";
            public const string Add = Prefix + "/add";
            public const string Update = Prefix + "/update/{id:int}";
            public const string Delete = Prefix + "/delete/{id:int}";
        }

        public static class TeamMembers
        {
            public const string Prefix = Rule + "team-members";

            public const string GetAll = Prefix + "/get-all";
            public const string GetById = Prefix + "/get-by-id/{id:int}";
            public const string Add = Prefix + "/add";
            public const string Update = Prefix + "/update/{id:int}";
            public const string Delete = Prefix + "/delete/{id:int}";
        }

        public static class TeamRoles
        {
            public const string Prefix = Rule + "team-roles";

            public const string GetAll = Prefix + "/get-all";
            public const string GetById = Prefix + "/get-by-id/{id:int}";
            public const string Add = Prefix + "/add";
            public const string Update = Prefix + "/update/{id:int}";
            public const string Delete = Prefix + "/delete/{id:int}";
        }

        // -------------------- TEAMS --------------------
        public static class Teams
        {
            public const string Prefix = Rule + "teams";

            public const string Add = Prefix + "/add";
            public const string GetAll = Prefix + "/get-all";
            public const string GetById = Prefix + "/get-by-id/{id:int}";
            public const string Update = Prefix + "/update/{id:int}";
            public const string Delete = Prefix + "/delete/{id:int}";
            public const string GetTeamProjects = Prefix + "/get-team-projects/{teamId:int}";
        }


        public static class Auth
        {
            public const string Prefix = Rule + "auth";

            public const string Login = Prefix + "/login";
            public const string Register = Prefix + "/register";

            public static class PasswordReset
            {
                public const string Prefix = Auth.Prefix + "/password-reset";

                public const string SendCode = Prefix + "/send-code";
                public const string VerifyCode = Prefix + "/verify-code";
                public const string Confirm = Prefix + "/confirm";
            }

            public const string ConfirmEmailOtp = Prefix + "/confirm-email-otp";
        }
    }

}
