using Scalar.AspNetCore;
using System.Security.Claims;

namespace HR__Management_System.Extentions
{
    public static class ApiMiddlewares
    {
        public static WebApplication UseApiMiddelwares(this WebApplication app)
        {

            app.UseExceptionHandler();

            // HTTPS Redirection
            app.UseHttpsRedirection();

            app.UseCors("AllowAngularApp");

            // Routing 
            app.UseRouting();

            // Developer Tools / Scalar OpenAPI
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options.Title = "HRMS";
                    options.Theme = ScalarTheme.Purple;
                });
            }
           
            // Authentication & Authorization 
            app.UseAuthentication();
            app.UseAuthorization();


            return app;
        }
    }
}
