using Dapper;
using IWantApp.EndPoints.Employees;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data;

public class QueryAllUsersWitchClaimName
{

    private readonly IConfiguration configuration;
    public QueryAllUsersWitchClaimName(IConfiguration configuration) {
        this.configuration = configuration;
    }

    public IEnumerable<EmployeeResponse> Execute(int? page, int? rows)
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
        return db.Query<EmployeeResponse>(query, new { page, rows });
        
    }
}
