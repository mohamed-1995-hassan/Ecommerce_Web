using Microsoft.OpenApi.Models;

namespace Ecommerce_Web.Extentions
{
    public static class SwaggerServiceExtention
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(config =>
            {
                var securitySechema = new OpenApiSecurityScheme
                {
                    Description = "Jwt auth security scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",

                    }
                };
                config.AddSecurityDefinition("Bearer", securitySechema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        securitySechema,
                        new[]
                        {
                            "Bearer"
                        }
                    }
                };
                config.AddSecurityRequirement(securityRequirement);
            });
            return services;
        }
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
