
using Marten;
using SoftwareCenter.Api.Vendors;

namespace SoftwareCenter.Api.CatalogItems;

public class VendorLookup(IDocumentSession session) : ILookupVendors
{
    public async Task<bool> CheckIfVendorExistsAsync(Guid id)
    {
        var item =  await session.Query<CreateVendorResponse>().Where(v => v.Id == id).SingleOrDefaultAsync();

        return item == null;
    }
}
