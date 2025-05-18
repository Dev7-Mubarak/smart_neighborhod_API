using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Entites.Enums;

namespace SmartNeighborhoodAPI.Controllers
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
        private static IEnumerable<EnumHelper> GetEnumValuesWithDisplayName<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new EnumHelper
                {
                    Name = GetDisplayName(e),
                    Value = Convert.ToInt32(e)
                });
        }

        private static string GetDisplayName<T>(T enumValue)
        {
            var memberInfo = typeof(T).GetMember(enumValue.ToString()).FirstOrDefault();
            var displayAttr = memberInfo?.GetCustomAttributes(typeof(DisplayAttribute), false)
                                        .FirstOrDefault() as DisplayAttribute;

            return displayAttr?.Name ?? enumValue.ToString();
        }
    }
}
