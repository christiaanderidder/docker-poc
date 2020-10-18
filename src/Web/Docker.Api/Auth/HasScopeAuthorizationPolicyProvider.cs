using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docker.Core.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Docker.Api.Auth
{

    /// <summary>
    /// Dynamically resolve authorization policies for the requested scope
    /// Based on https://www.jerriepelser.com/blog/creating-dynamic-authorization-policies-aspnet-core/
    /// </summary>
    public class HasScopeAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _options;
        private readonly IOptionsMonitor<IdentityServerConfiguration> _configuration;

        public HasScopeAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options, IOptionsMonitor<IdentityServerConfiguration> configuration) : base(options)
        {
            _options = options.Value;
            _configuration = configuration;
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            // Check static policies first
            var policy = await base.GetPolicyAsync(policyName);

            if (policy == null)
            {
                var identityServerConfig = _configuration.CurrentValue;

                policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new HasScopeRequirement(policyName, identityServerConfig?.Host ?? ""))
                    .Build();

                // Add policy to the AuthorizationOptions, so we don't have to re-create it each time
                _options.AddPolicy(policyName, policy);
            }

            return policy;
        }
    }
}
