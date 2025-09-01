using SmartNeighborhoodAPI.Entites.Enums;

namespace SmartNeighborhoodAPI.Controllers.V1
{
    public class EnumsController : AppControllerBase
    {
        [HttpGet(Router.Enums.MaritalStatuses)]
        public IActionResult GetMaritalStatuses()
        {
            var values = GetEnumValuesWithDisplayName<MaritalStatus>();
            return Response(ApiResponse<IEnumerable<EnumHelper>>.Success(values));
        }

        [HttpGet(Router.Enums.BloodTypes)]
        public IActionResult GetBloodTypes()
        {
            var values = GetEnumValuesWithDisplayName<BloodType>();
            return Response(ApiResponse<IEnumerable<EnumHelper>>.Success(values));
        }

        [HttpGet(Router.Enums.IdentityTypes)]
        public IActionResult GetIdentityTypes()
        {
            var values = GetEnumValuesWithDisplayName<IdentityType>();
            return Response(ApiResponse<IEnumerable<EnumHelper>>.Success(values));
        }

        [HttpGet(Router.Enums.OccupationStatuses)]
        public IActionResult GetOccupationStatuses()
        {
            var values = GetEnumValuesWithDisplayName<OccupationStatus>();
            return Response(ApiResponse<IEnumerable<EnumHelper>>.Success(values));
        }

        [HttpGet(Router.Enums.Gender)]
        public IActionResult GetGender()
        {
            var values = GetEnumValuesWithDisplayName<Gender>();
            return Response(ApiResponse<IEnumerable<EnumHelper>>.Success(values));
        }


        [HttpGet(Router.Enums.ProjectStatus)]
        public IActionResult GetProjectStatus()
        {
            var values = GetEnumValuesWithDisplayName<ProjectStatus>();
            return Response(ApiResponse<IEnumerable<EnumHelper>>.Success(values));
        }

        [HttpGet(Router.Enums.ProjectPriority)]
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
