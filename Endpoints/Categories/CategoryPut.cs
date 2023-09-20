using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IWantApp.Endpoints.Categories;

public class CategoryPut
{
    // Define o template da URL para este endpoint
    public static string Template => "/categories/{id:guid}";

    // Define os métodos HTTP permitidos para este endpoint
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };

    // Define o delegado que manipula a ação deste endpoint
    public static Delegate Handle => Action;

    // Atributo para autorização com base em uma política específica
    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action(
        [FromRoute] Guid id, HttpContext http, CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        // Obtém o ID do usuário atual
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        // Busca a categoria pelo ID fornecido
        var category = context.Categories.Where(c => c.Id == id).FirstOrDefault();

        // Verifica se a categoria existe
        if (category == null)
            return Results.NotFound();

        // Edita as informações da categoria
        category.EditInfo(categoryRequest.Name, categoryRequest.Active, userId);

        // Verifica se a categoria é válida após a edição
        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());

        // Salva as alterações no banco de dados
        context.SaveChanges();

        // Retorna uma resposta HTTP 200 OK
        return Results.Ok();
    }
}
