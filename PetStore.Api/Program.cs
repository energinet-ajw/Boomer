using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;
using Newtonsoft.Json;
using PetStore.Api.Extensions;
using PetStore.Api.Middleware;
using PetStore.Api.Options;
using PetStore.Application.Base;
using PetStore.Domain.CatAggregate;
using PetStore.Domain.MouseAggregate;
using PetStore.Infrastructure.EventDispatching;
using PetStore.Infrastructure.EventDispatching.Domain;
using PetStore.Infrastructure.EventDispatching.Integration;
using PetStore.Infrastructure.Outbox;
using PetStore.Infrastructure.Persistence;
using PetStore.Infrastructure.Persistence.Cat;
using PetStore.Infrastructure.Persistence.Mouse;
using PetStore.Infrastructure.Persistence.Outbox;
using ISystemClock = Microsoft.Extensions.Internal.ISystemClock;

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

        /*builder.Services.AddDbContext<DatabaseContext>(
            options => options.UseSqlite(builder.Configuration.GetConnectionString("PetStore"))
            );
        */
        var inMemorySqlite = new SqliteConnection(builder.Configuration.GetConnectionString("PetStore"));
        inMemorySqlite.Open();
        builder.Services.AddDbContext<DatabaseContext>(options => {
            options.UseSqlite(inMemorySqlite);
        });
      
        // Swagger configuration
        builder.Services.SwaggerIncludeXmlComments(AppContext.BaseDirectory, "*.xml");
        builder.Services.SwaggerAddSecurity();
        builder.Services.SwaggerResolveConflictingActions();

        // Versioning configuration
        // ReSharper disable once StringLiteralTypo
        builder.Services.AddApiVersioning("'v'VVV");

        // Validation
        builder.Services.AddValidatorsFromAssembly(typeof(PetStore.Application.Root).Assembly);
        
        builder.Services.AddMediator();
        
        builder.Services.Configure<MySecrets>(builder.Configuration.GetSection("MySecrets"));
        
        // Register
        builder.Services.AddTransient<ExceptionMiddleware>();
        builder.Services.AddTransient<IDomainEventContainer, DomainEventContainer>();
        builder.Services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
        builder.Services.AddTransient<ICatRepository, CatRepository>();
        builder.Services.AddTransient<IMouseRepository, MouseRepository>();
        builder.Services.AddTransient<IIntegrationEventPublisher, IntegrationEventPublisher>();
        builder.Services.AddTransient<IOutboxMessageRepository, OutboxMessageRepository>();
        builder.Services.AddTransient<IOutboxMessageFactory, OutboxMessageFactory>();
        
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();

        builder.Services.AddSingleton<ISystemClock>(new Microsoft.Extensions.Internal.SystemClock());
        
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