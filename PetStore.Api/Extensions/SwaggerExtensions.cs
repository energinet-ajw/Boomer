using Microsoft.OpenApi.Models;

namespace PetStore.Api.Extensions;

public static class SwaggerExtensions
{
    /// <summary>
    ///     Find all comment xml files and add them to the Swagger engine.
    /// </summary>
    public static void SwaggerIncludeXmlComments(this IServiceCollection serviceCollection, string path,
        string searchPattern)
    {
        var xmlFiles = Directory.GetFiles(path, searchPattern);
        serviceCollection.AddSwaggerGen(options =>
        {
            foreach (var xmlFile in xmlFiles)
                // If creation of XML documentation is enabled, provide the path to them to include tags documentation in Swagger.
                // https://medium.com/@alibenchaabene/include-xml-comments-in-swagger-under-asp-net-core-2-2-webapi-7514e44cc9b1
                options.IncludeXmlComments(xmlFile);
        });
    }

    public static void SwaggerResolveConflictingActions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options =>
        {
            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });
    }

    /// <summary>
    ///     https://www.infoworld.com/article/3650668/implement-authorization-for-swagger-in-aspnet-core-6.html
    /// </summary>
    public static void SwaggerAddSecurity(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
        });
    }
}