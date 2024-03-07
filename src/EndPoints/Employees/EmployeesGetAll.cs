using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

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

    public static IResult Action(int? page, int? rows, IConfiguration configuration)
    {
        if (page == null || page < 1) page = 1;
        if (rows == null || rows <= 0) rows = 10;

        var db = new SqlConnection(configuration["ConnectionString:IWantDb"]);
        var query = @"
        SELECT  
            anuc.ClaimValue as Name,
            anu.Email as Email
        FROM AspNetUsers anu  
        INNER JOIN AspNetUserClaims anuc on anu.Id = anuc.UserId and ClaimType = 'Name'
        ORDER BY Name -- Corrigido para 'Name'
        OFFSET (@page -1 ) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
        var employees = db.Query<EmployeeResponse>(query, new {page, rows});
        
        return Results.Ok(employees);
    }
}
