
using IWantApp.EndPoints.Employees;
using Microsoft.AspNetCore.Identity;


namespace IWant.Endpoints.Employees;

public class EmployeeGetAll
{
	public static string Template => "/employees";

	public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
	public static Delegate Handle => Action;

	public static IResult Action(UserManager<IdentityUser> userManager) 
	{
		
		var users = userManager.Users.ToList();
		var employees = users.Select(u => new EmployeeResponse(u.Email, "Name"));
		return Results.Ok(employees);
	}
}