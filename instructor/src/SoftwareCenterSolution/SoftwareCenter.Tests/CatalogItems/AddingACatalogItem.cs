
using Alba;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using SoftwareCenter.Api.CatalogItems;

namespace SoftwareCenter.Tests.CatalogItems;

[Trait("Category", "UnitIntegration")]
public  class AddingACatalogItem
{
    [Fact]
    public async Task CanAddACatalogItemWhenTheVendorExists()
    {
        var catalogItem = new CatalogItemCreateRequest
        {
            Description = "Code Editor",
            Name = "Visual Studio Code",
            Version = "1.28.0"
        };
        var fakeVendorId = Guid.NewGuid();
        var host = await AlbaHost.For<Program>(config =>

        {
            config.ConfigureTestServices(services =>
            {
                var fakeVendorLookup = Substitute.For<ILookupVendors>();
                fakeVendorLookup.CheckIfVendorExistsAsync(fakeVendorId).Returns(Task.FromResult(false));
                services.AddScoped<ILookupVendors>( _ => fakeVendorLookup);
            });
        });

        var postReponse = await host.Scenario(api =>
        {
            api.Post.Json(catalogItem).ToUrl($"/vendors/{fakeVendorId}/items");
            api.StatusCodeShouldBeOk();
        });

    }

    [Fact]
    public async Task CannotAddACatalogItemForAVendorThatDoesNotExist()
    {
        var catalogItem = new CatalogItemCreateRequest
        {
            Description = "Code Editor",
            Name = "Visual Studio Code",
            Version = "1.28.0"
        };
        var fakeVendorId = Guid.NewGuid();
        var host = await AlbaHost.For<Program>(config =>
        {
            config.ConfigureTestServices(services =>
            {
                var fakeVendorLookup = Substitute.For<ILookupVendors>();
                fakeVendorLookup.CheckIfVendorExistsAsync(fakeVendorId).Returns(Task.FromResult(true));
                services.AddScoped<ILookupVendors>(_ => fakeVendorLookup);
            });
        });

        var postReponse = await host.Scenario(api =>
        {
            api.Post.Json(catalogItem).ToUrl($"/vendors/{fakeVendorId}/items");
            api.StatusCodeShouldBe(404);
        });

    }
}


//public class StubbedReturnsNoVendorLookup : ILookupVendors
//{
//    public Task<bool> CheckIfVendorExistsAsync(Guid id)
//    {
//        return Task.FromResult( false );
//    }
//}

//public class StubbedReturnsYesVendorLookup : ILookupVendors
//{
//    public Task<bool> CheckIfVendorExistsAsync(Guid id)
//    {
//        return Task.FromResult(true);
//    }
//}