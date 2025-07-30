
using System.Security.Claims;
using Alba;
using Alba.Security;
using SoftwareCenter.Api.Vendors;

namespace SoftwareCenter.Tests.Vendors;

public  class AddAVendor
{
    [Fact]
    public async Task MustMeetSecurityPolicyToAdd()
    {
        var host = await AlbaHost.For<Program>(
            config => {}, new AuthenticationStub());
        // start the API with our Program.cs, and host it in memory
        var vendorToCreate = new CreateVendorRequest
        {
            Name = "Microsoft",
            Url = "https://www.microsoft.com",
            PointOfContact = new CreateVendorPointOfContactRequest
            {
                Name = "satya",
                Email = "satya@microsoft.com",
                Phone = "888 555-1212"
            }
        };
        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(vendorToCreate).ToUrl("/vendors");
            api.StatusCodeShouldBe(403);
        });
    }

    [Fact]
    public async Task MustHaveProperAuthToAddVender()
    {
        var host = await AlbaHost.For<Program>();
        // start the API with our Program.cs, and host it in memory
        var vendorToCreate = new CreateVendorRequest
        {
            Name = "Microsoft",
            Url = "https://www.microsoft.com",
            PointOfContact = new CreateVendorPointOfContactRequest
            {
                Name = "satya",
                Email = "satya@microsoft.com",
                Phone = "888 555-1212"
            }
        };
        var postResponse = await host.Scenario(api =>
        { 
            api.Post.Json(vendorToCreate).ToUrl("/vendors");
            api.StatusCodeShouldBe(401);
        });
    }
    
    [Fact]
    public async Task WeGetASuccessStatusCode()
    {
        var host = await AlbaHost.For<Program>(
            config =>
            {

            }, new AuthenticationStub()
            );
        // start the API with our Program.cs, and host it in memory
        var vendorToCreate = new CreateVendorRequest
        {
            Name = "Microsoft",
            Url = "https://www.microsoft.com",
            PointOfContact = new CreateVendorPointOfContactRequest
            {
                Name = "satya",
                Email = "satya@microsoft.com",
                Phone = "888 555-1212"
            }
        };
       var postResponse =  await host.Scenario(api =>
        {
            api.WithClaim(new Claim(ClaimTypes.Role, "Manager"));
            api.WithClaim(new Claim(ClaimTypes.Role, "SoftwareCenter"));
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
