using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(UserManager<IdentityUser> userManager)
    {
        var users = userManager.Users.ToList();
        var employees = new List<EmployeeResponse>();
        foreach (var item in users)
        {
            var claims = userManager.GetClaimsAsync(item).Result;
            var userName = claims.First(c => c.Type == "Name");
        }
        var employees = users.Select(u => new EmployeeResponse(u.Email, "Name"));
        return Results.Ok(employees);
    }

}
