using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    // Define o template da URL para este endpoint
    public static string Template => "/employees";

    // Define os métodos HTTP permitidos para este endpoint
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    // Define o delegado que manipula a ação deste endpoint
    public static Delegate Handle => Action;

    // Atributo para autorização com base em uma política específica
    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        // TODO: implementar validação

        // Executa a consulta para obter todos os usuários com o nome da reivindicação
        var result = await query.Execute(page.Value, rows.Value);

        // Retorna o resultado como uma resposta HTTP 200 OK
        return Results.Ok(result);
    }
}