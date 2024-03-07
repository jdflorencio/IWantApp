using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using IWantApp.Infra.Data;

namespace IWantApp.EndPoints.Employees;

public class EmployeesGetAll
{
    private static object? employees;

    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    /*public static IResult Action(int page, int rows, UserManager<IdentityUser>userManager)
    {
        var users = userManager.Users.Skip((page - 1) * rows).Take(rows).ToList();
        var employees = new List<EmployeeResponse>();

        
        foreach(var item in users)
        {
            
            var claims = userManager.GetClaimsAsync(item).Result;
            var claimName = claims.FirstOrDefault(c => c.Type == "Name");
            var userName = claimName != null ? claimName.Value : string.Empty;
            employees.Add(new EmployeeResponse(userName, item.Email));
        }

        return Results.Ok(employees);
    }*/

    public static IResult Action(int? page, int? rows, QueryAllUsersWitchClaimName query)
    {
        return Results.Ok(query.Execute(page, rows));
    }
}
