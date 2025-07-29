
using Alba;
using SoftwareCenter.Api.Vendors;

namespace SoftwareCenter.Tests.Vendors;

public  class AddAVendor
{
    [Fact]
    public async Task WeGetASuccessStatusCode()
    {
        var host = await AlbaHost.For<Program>();
        // start the API with our Program.cs, and host it in memory
        var vendorToCreate = new CreateVendorRequest("Microsoft", "https://www.microsoft.com", new CreateVendorPointOfContactRequest("Satya", "800 big-corp", "satya@microsoft.com"));
       var postResponse =  await host.Scenario(api =>
        {
            api.Post.Json(vendorToCreate).ToUrl("/vendors");
            api.StatusCodeShouldBeOk();
        });

        var postBodyResponse = await postResponse.ReadAsJsonAsync<CreateVendorResponse>();

        Assert.NotNull(postBodyResponse);


        var getResponse = await host.Scenario(api =>
        {
            api.Get.Url($"/vendors/{postBodyResponse.Id}");
            api.StatusCodeShouldBeOk();
        });

        var getResponseBody = await getResponse.ReadAsJsonAsync<CreateVendorResponse>();

        Assert.NotNull(getResponseBody);


        Assert.Equal(postBodyResponse, getResponseBody);
     
       
    }
}
