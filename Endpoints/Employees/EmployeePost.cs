using System.Security.Claims;

namespace IWantApp.Endpoints.Employees;

public class EmployeePost
{
    // Define o template da URL para este endpoint
    public static string Template => "/employees";

    // Define os métodos HTTP permitidos para este endpoint
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    // Define o delegado que manipula a ação deste endpoint
    public static Delegate Handle => Action;

    // Atributo para autorização com base em uma política específica
    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(EmployeeRequest employeeRequest, HttpContext http, UserManager<IdentityUser> userManager)
    {
        // Obtém o ID do usuário atual
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        // Cria um novo usuário Identity
        var newUser = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email };

        // Tenta criar o novo usuário
        var result = await userManager.CreateAsync(newUser, employeeRequest.Password);

        // Verifica se a criação foi bem-sucedida
        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

        // Cria uma lista de reivindicações para o novo usuário
        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name),
            new Claim("CreatedBy", userId)
        };

        // Tenta adicionar as reivindicações ao novo usuário
        var claimResult = await userManager.AddClaimsAsync(newUser, userClaims);

        // Verifica se a adição de reivindicações foi bem-sucedida
        if (!claimResult.Succeeded)
            return Results.BadRequest(claimResult.Errors.First());

        // Retorna o ID do novo usuário como uma resposta HTTP 201 Created
        return Results.Created($"/employees/{newUser.Id}", newUser.Id);
    }
}
