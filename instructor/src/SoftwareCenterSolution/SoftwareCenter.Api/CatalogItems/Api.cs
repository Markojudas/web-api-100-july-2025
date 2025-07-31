using Marten;

namespace SoftwareCenter.Api.CatalogItems;

public static class Api
{
    public static IEndpointRouteBuilder MapCatalogItems(this IEndpointRouteBuilder app)
    {

        app.MapPost("/vendors/{id:guid}/items", async (
            Guid id, 
            CatalogItemCreateRequest request, 
            IDocumentSession session,
            ILookupVendors vendorLookups) =>
        {
            // validate the stuff - see the issue.
            // create the entity
            bool noSuchVendor = await vendorLookups.CheckIfVendorExistsAsync(id);
            if(noSuchVendor)
            {
                return Results.NotFound();
            }
            // from CatalogItemCreateRequest -> CatalogItemEntity
            var entity = request.MapToEntity(id);
            // save it to the database (side effect)
            session.Store(entity);
            await session.SaveChangesAsync();
            // create a response 
            var response = entity.MapToResponse();

            return Results.Ok(response);
        });
        return app;
    }

    public static IServiceCollection AddCatalogItems(this IServiceCollection services)
    {
       
        //services.AddScoped
        return services;
    }
}

public record CatalogItemCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;

    public  CatalogItemEntity MapToEntity(Guid vendorId)
    {
        return new CatalogItemEntity
        {

            Id = Guid.NewGuid(),
            VendorId = vendorId,
            Created = DateTimeOffset.UtcNow,
            Description = Description,
            Name = Name,
            Version = Version,
        };
    }
}

public record CatalogItemDetails
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;

  
}

public class CatalogItemEntity
{
    public Guid Id { get; set; }
    public Guid VendorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;

    public DateTimeOffset Created { get; set; }
    public  CatalogItemDetails MapToResponse()
    {
        return new CatalogItemDetails
        {
            Id = Id,
            Description =Description,
            Name = Name,
            Version = Version,
        };
    }
}

public interface ILookupVendors
{
    Task<bool> CheckIfVendorExistsAsync(Guid id);
}