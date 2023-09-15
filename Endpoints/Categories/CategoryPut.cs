using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id, CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = new Category
        {
            Name = categoryRequest.Name,
            CreateBy = "Test",
            CreatedOn = DateTime.Now,
            EditeBy = "Test",
            EditeOn = DateTime.Now
        };
        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", category.Id);
    }

}
