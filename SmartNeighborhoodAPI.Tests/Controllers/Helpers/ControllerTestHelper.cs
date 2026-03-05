using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SmartNeighborhoodAPI.Tests.Controllers.Helpers;

/// <summary>Shared utilities for controller unit tests.</summary>
public static class ControllerTestHelper
{
    /// <summary>
    /// Sets a fake authenticated user on the controller so that
    /// <c>User.FindFirstValue(ClaimTypes.NameIdentifier)</c> returns the supplied id.
    /// </summary>
    public static void SetFakeUser(ControllerBase controller, string userId = "test-user-id", string role = "Admin")
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Role, role)
        };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };
    }
}
