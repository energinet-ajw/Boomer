using System.ComponentModel;
using System.Reflection;
using Boomer.Application.Commands;
using Boomer.Application.Commands.Base;
using Boomer.Application.Queries;
using Boomer.Application.Validators.Base;
using Boomer.Domain.Validators;
using Boomer.WebApi.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
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

            // Validation
            builder.Services.AddValidatorsFromAssembly(typeof(CreateBoomerCommandValidator).Assembly);

            var config = builder.Configuration;

            /*
            builder.Services.AddAuthentication().AddGoogle(options =>
            {
                var section = config.GetSection("Authentication:Google");
                options.ClientId = section["ClientId"]!;
                options.ClientSecret = section["ClientSecret"]!;
            });
            */

            // MediatR is a low-ambition library trying to solve a simple problem — decoupling the in-process sending of messages from handling messages.
            // By Jimmi Bogard, https://github.com/jbogard/MediatR
            builder.Services.AddMediatR(typeof(Program));
            
            // Register
            builder.Services.AddTransient<ExceptionMiddleware>();
            builder.Services.AddScoped<AsyncRequestHandler<OneWayCommand>, OneWayCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<GetHelloQuery, string>, GetHelloQueryHandler>();
            builder.Services.AddScoped<IRequestHandler<CreateBoomerCommand, Unit>, CreateBoomerCommandHandler>();
            
            // Register pipebehavior
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
           
            var app = builder.Build();
            
            app.UseMiddleware<ExceptionMiddleware>();
           

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            await app.RunAsync();
        }
    }
}