using System.Reflection;
using Boomer.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Boomer.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            
            // Swagger configuration
            builder.Services.SwaggerIncludeXmlComments(AppContext.BaseDirectory, "*.xml");
            builder.Services.SwaggerAddSecurity();
            builder.Services.SwaggerResolveConflictingActions();

            // Versioning configuration
            builder.Services.AddApiVersioning("'v'VVV");

            
            // MediatR is a low-ambition library trying to solve a simple problem — decoupling the in-process sending of messages from handling messages.
            // By Jimmi Bogard, https://github.com/jbogard/MediatR
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
            
            builder.Services.AddTransient<ExceptionMiddleware>();

            var app = builder.Build();
            
            app.UseMiddleware<ExceptionMiddleware>();

            var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });
            }

            // Adds middleware for redirecting HTTP requests to HTTPS  
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            await app.RunAsync();
        }
    }
}