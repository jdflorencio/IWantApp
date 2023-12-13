using IWantApp.Domain.Products;
using IWantApp.Infra.Data;

namespace IWantApp.EndPoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        /*if (string.IsNullOrEmpty(categoryRequest.Name))
        {
            return Results.BadRequest("name is invalid");
        }*/

        var category = new Category(categoryRequest.Name, "Teste", "Test");

        if (!category.IsValid)
            return Results.BadRequest(category.Notifications);

        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/categoreis/{category.Id}", category.Id);
    }
}
