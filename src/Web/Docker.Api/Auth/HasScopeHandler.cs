using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Docker.Api.Auth
{
    /// <summary>
    /// Authorization handler used for checking scopes
    /// Based on https://www.jerriepelser.com/blog/creating-dynamic-authorization-policies-aspnet-core/
    /// </summary>
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        private const string _scopeClaim = "scope";

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            // If user does not have the scope claim, get out of here
            if (!context.User.HasClaim(c => c.Type == _scopeClaim && c.Issuer == requirement.Issuer)) return Task.CompletedTask;

            // Split the scopes string into an array
            var claim = context.User.FindFirst(c => c.Type == _scopeClaim && c.Issuer == requirement.Issuer);
            if (claim == null) return Task.CompletedTask;

            var scopes = claim.Value.Split(' ');

            // Succeed if the scope array contains the required scope
            if (scopes.Any(s => s == requirement.Scope)) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
