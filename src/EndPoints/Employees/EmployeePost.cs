using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.EndPoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser { UserName = employeeRequest.Name, Email = employeeRequest.Email };
        var result = userManager.CreateAsync(user, employeeRequest.Password).Result;//para salvar esse usuario

        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());
        
        var userClaims = new List<Claim>
        {
           new Claim("EmployeeCode", employeeRequest.EmployeeCode),
           new Claim("Name", employeeRequest.EmployeeCode)
        };

        var claimResult = 
            userManager.AddClaimsAsync(user, userClaims).Result;      

        if (!claimResult.Succeeded)
            return Results.BadRequest(claimResult.Errors.First());

        return Results.Created($"/employee/{user.Id}", user.Id);
    }
}
