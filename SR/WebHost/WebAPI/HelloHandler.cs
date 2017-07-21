
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace WebHost.WebAPI
{
	public class HelloHandler : AuthorizationHandler<HelloRequirement, Hello>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HelloRequirement requirement, Hello resource)
		{
			if (resource.Name == context.User.FindFirst(ClaimTypes.Name).Value)
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}