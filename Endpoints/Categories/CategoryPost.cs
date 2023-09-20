using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IWantApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(CategoryRequest categoryRequest, HttpContext http, ApplicationDbContext context)
    {
        // Obtém o ID do usuário atual
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        // Cria uma nova categoria
        var category = new Category(categoryRequest.Name, userId, userId);

        // Valida a nova categoria
        if (!category.IsValid)
        {
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
        }

        // Adiciona a categoria ao banco de dados e salva as alterações
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();

        // Retorna uma resposta HTTP 201 Created
        return Results.Created($"/categories/{category.Id}", category.Id);
    }
}
