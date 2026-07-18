using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using DataAccessLayer.ModelContext;
using Microsoft.AspNetCore.Authorization;

namespace PresentationLayer.Authorization
{
    public class MustOwnHandler : AuthorizationHandler<MustOwnRequirement, IOwned>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MustOwnRequirement requirement,
            IOwned resource)
        {
            var userId = context.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (int.TryParse(userId, out var parsedId) && resource.OwnerId == parsedId)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}