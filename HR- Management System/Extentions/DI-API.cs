using EventHub.API.Middlewares;
using Microsoft.OpenApi.Models;

namespace HR__Management_System.Extentions
{
    public static class DI_API
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration, IHostBuilder host)
        {
            // Serilog Logging
           

            // Http Context Accessor
            services.AddHttpContextAccessor();

            // Exception Handling
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            // 3. Authentication & Authorization
            
            services.AddAuthorization();

            // OpenAPI / Swagger Documentation
            services.AddOpenApiDocumentation();

            services.AddMemoryCache();

            return services;
        }

        

        public static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    document.Components ??= new OpenApiComponents();

                    document.Components.SecuritySchemes.Add("Bearer", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Description = "Insert Token Here"
                    });

                    document.SecurityRequirements.Add(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });

                    return Task.CompletedTask;
                });
            });

            return services;
        }
    }
}
