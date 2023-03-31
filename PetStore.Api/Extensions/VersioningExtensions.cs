using Boomer.WebApi.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace PetStore.Api.Extensions;

public static class VersioningExtensions
{
    public static void AddApiVersioning(this IServiceCollection serviceCollection, string groupNameFormat)
    {
        serviceCollection.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new MediaTypeApiVersionReader("x-api-version"));
        });

        // Add ApiExplorer to discover versions
        serviceCollection.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = groupNameFormat;
            setup.SubstituteApiVersionInUrl = true;
        });

        serviceCollection.ConfigureOptions<ConfigureSwaggerOptions>();
    }
}