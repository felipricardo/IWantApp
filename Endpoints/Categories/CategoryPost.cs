using IWantApp.Infra.Data;

namespace IWantApp.Endpoints.Categories;

public class CategoryPost
{
    public string Template => "/categories";
    public string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public Delegate Handle => Action;

    public IResult Action(CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        return Results.Ok("Ok");
    }

}
