using Alba;
using Shows.Api.Api.Shows;
using Shows.Tests.Api.Fixtures;

namespace Shows.Tests.Api.Shows;

[Collection("SystemTestFixture")]
[Trait("Category", "SystemTest")]

public class AddingAShow(SystemTestFixture fixture)
{
    private readonly IAlbaHost _host = fixture.Host;

    [Fact]
    public async Task AddShow()
    {
        var showToAdd = new AddShowRequest
        {
            Name = "Twin Peaks, the Return",
            Description = "25 Years Later. David Lynch at his Lynchiest",
            StreamingService = "HBO Max"
        };
        var postResponse = await _host.Scenario(_ =>
        {
            _.Post.Json(showToAdd).ToUrl("/api/shows");
            _.StatusCodeShouldBeOk();
        });

        var postResponeBody = await postResponse.ReadAsJsonAsync<AddShowResponse>();
        Assert.NotNull(postResponeBody);

        var getResponse = await _host.Scenario(api =>
        {
            api.Get.Url($"/api/shows/{postResponeBody.Id}");
            api.StatusCodeShouldBeOk();
        });

        var getResponseBody = await getResponse.ReadAsJsonAsync<AddShowResponse>();

        Assert.NotNull(getResponseBody);

        Assert.Equal(postResponeBody, getResponseBody);

    }
    
}