using Docker.Core;
using Docker.Data;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Docker.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDockerCore();
            services.AddDockerData();

            services.AddRouting(options =>
            {
                options.LowercaseQueryStrings = true;
                options.LowercaseUrls = true;
            });

            services.AddControllers(options =>
            {
                // require scope1 or scope2
                var policy = ScopePolicy.Create("product.read", "product.write");
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Docker.Api", Version = "v1" });
            });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    // base-address of your identityserver
                    options.Authority = "https://docker-poc-oauth";

                    // name of the API resource
                    options.ApiName = "product";
                    options.ApiSecret = "secret";

                    options.EnableCaching = true;
                    options.CacheDuration = TimeSpan.FromMinutes(10); // that's the default
                });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                // Kubernetes service IP ranges
                options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("10.0.0.0"), 8));
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.ForwardLimit = 2;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Docker.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
