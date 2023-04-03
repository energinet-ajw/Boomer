using Boomer.Application.Validators;
using Boomer.WebApi.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json;
using PetStore.Api.Extensions;
using PetStore.Application.Base;
using PetStore.Application.Mouse;
using PetStore.Application.Mouse.Listeners;
using PetStore.Domain.MouseAggregate;
using PetStore.Infrastructure.EventDispatching;
using PetStore.Infrastructure.Persistence;
using PetStore.Infrastructure.Persistence.Mouse;

namespace PetStore.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling =
                ReferenceLoopHandling.Ignore);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        // Swagger configuration
        builder.Services.SwaggerIncludeXmlComments(AppContext.BaseDirectory, "*.xml");
        builder.Services.SwaggerAddSecurity();
        builder.Services.SwaggerResolveConflictingActions();

        // Versioning configuration
        // ReSharper disable once StringLiteralTypo
        builder.Services.AddApiVersioning("'v'VVV");

        // Validation
        builder.Services.AddValidatorsFromAssembly(typeof(CreateBoomerCommandValidator).Assembly);

        // MediatR is a low-ambition library trying to solve a simple problem — decoupling the in-process sending of messages from handling messages.
        // By Jimmi Bogard, https://github.com/jbogard/MediatR
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        // Register
        builder.Services.AddTransient<ExceptionMiddleware>();
        builder.Services.AddTransient<IRequestHandler<PublishEventCommand>, PublishEventHandler>();
        builder.Services.AddTransient<IRequestHandler<GetMouseQuery, MouseDto>, GetMouseQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<CreateMouseCommand, Guid>, CreateMouseCommandHandler>();
        builder.Services.AddTransient<IDomainEventContainer, DomainEventContainer>();
        builder.Services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
        builder.Services.AddTransient<IMouseRepository, MouseRepository>();
        builder.Services.AddTransient<INotificationHandler<MouseCreatedDomainEvent>, MouseCreatedEventListener>();

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
        
        builder.Services.AddPipelineBehaviours();

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
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
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