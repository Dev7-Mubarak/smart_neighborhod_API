using SmartNeighborhoodAPI.Entites.Enums;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    public class EnumsController : AppControllerBase
    {
        [HttpGet("marital-statuses")]
        public IActionResult GetMaritalStatuses()
        {
            var values = GetEnumValuesWithDisplayName<MaritalStatus>();
            return Response(ApiResponse<IEnumerable<EnumHelper>>.Success(values));
        }

        [HttpGet("blood-types")]
        public IActionResult GetBloodTypes()
        {
            var values = GetEnumValuesWithDisplayName<BloodType>();
            return Response(ApiResponse<IEnumerable<EnumHelper>>.Success(values));
        }

        [HttpGet("identity-types")]
        public IActionResult GetIdentityTypes()
        {
            var values = GetEnumValuesWithDisplayName<IdentityType>();
            return Response(ApiResponse<IEnumerable<EnumHelper>>.Success(values));
        }

        [HttpGet("occupation-statuses")]
        public IActionResult GetOccupationStatuses()
        {
            var values = GetEnumValuesWithDisplayName<OccupationStatus>();
            return Response(ApiResponse<IEnumerable<EnumHelper>>.Success(values));
        }

        [HttpGet("Gender")]
        public IActionResult GetGender()
        {
            var values = GetEnumValuesWithDisplayName<Gender>();
            return Response(ApiResponse<IEnumerable<EnumHelper>>.Success(values));
        }


        [HttpGet("get-project-status")]
        public IActionResult GetProjectStatus()
        {
            var values = GetEnumValuesWithDisplayName<ProjectStatus>();
            return Response(ApiResponse<IEnumerable<EnumHelper>>.Success(values));
        }

        [HttpGet("get-project-priority")]
        public IActionResult GetProjectPriority()
        {
            var values = GetEnumValuesWithDisplayName<ProjectPriority>();
            return Response(ApiResponse<IEnumerable<EnumHelper>>.Success(values));
        }
        private static IEnumerable<EnumHelper> GetEnumValuesWithDisplayName<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new EnumHelper
                {
                    Name = e.ToString(),
                    Value = Convert.ToInt32(e)
                });
        }
    }
}
