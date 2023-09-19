using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "Employee909Policy")]
    public static IResult Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        // implementar validação
        return Results.Ok(query.Execute(page.Value, rows.Value));
    }

}
