namespace SoftwareCenter.Api.CatalogItems;

public static class Api
{
    public static IEndpointRouteBuilder MapCatalogItems(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("catalog-items");
        group.MapGet("", () =>
        {
            var catalogItems = new List<string>();
            return Results.Ok(catalogItems);
        });

        group.MapPost("", () =>
        {
            return Results.NoContent();
        });
        return app;
    }

    public static IServiceCollection AddCatalogItems(this IServiceCollection services)
    {
        //services.AddScoped
        return services;
    }
}
