using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RamandTestSolution.Application;
using RamandTestSolution.Persistence;
using RamandTestSolution.WebAPI.Filters.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace RamandTestSolution.WebAPI
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
            #region Api Versioning
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });


            #endregion

            #region JWT Configuration
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = Configuration["Jwt:Issuer"],
                      ValidAudience = Configuration["Jwt:Issuer"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            #endregion

            #region Swagger Configuration
            services.AddSwaggerGen(configureSwaggerGen);


            #endregion

            #region Persistence Dependency
            services.AddPersistence();
            #endregion

            #region Appilcation Dependency
            services.AddApplication(typeof(Startup));
            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region SwaggerConfiguration
            app.UseSwagger(c => { c.RouteTemplate = "dev/swagger/{documentName}/swagger.json"; });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/dev/swagger/v1/swagger.json", "API v1");
                options.SwaggerEndpoint("/dev/swagger/v2/swagger.json", "API v2");
                options.RoutePrefix = "dev/swagger";
            });
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private static void configureSwaggerGen(SwaggerGenOptions options)
        {
            addSwaggerDocs(options);

            options.OperationFilter<RemoveVersionFromParameter>();
            options.DocumentFilter<ReplaceVersionWithExactValueInPath>();

            options.DocInclusionPredicate((version, desc) =>
            {
                if (!desc.TryGetMethodInfo(out var methodInfo))
                    return false;

                var versions = methodInfo
                   .DeclaringType?
               .GetCustomAttributes(true)
               .OfType<ApiVersionAttribute>()
               .SelectMany(attr => attr.Versions);

                var maps = methodInfo
                   .GetCustomAttributes(true)
               .OfType<MapToApiVersionAttribute>()
               .SelectMany(attr => attr.Versions)
               .ToList();

                return versions?.Any(v => $"v{v}" == version) == true
                         && (!maps.Any() || maps.Any(v => $"v{v}" == version));
            });
        }

        private static void addSwaggerDocs(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Ramand Test Api v1",
                Description = "API for test",
            });

            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2",
                Title = "Ramand Test Api v2",
                Description = "API for test",
            });
        }

    }
}
