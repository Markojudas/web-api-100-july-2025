using Alba;
using Shows.Api.Shows;
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
        var requestBody = new AddShowRequest
        {
            Name = "My Name Is Earl",
            Description = "Lowlife turns life around thanks to Carson Daily",
            StreamingService = "PlexStation"
        };
        var response = await _host.Scenario(_ =>
        {
            _.Post.Json(requestBody).ToUrl("/api/shows");
        });

        var postResponseBody = await response.ReadAsJsonAsync<AddShowResponse>();

        Assert.NotNull(postResponseBody);

        var getResponse = await _host.Scenario(_ =>
        {
            _.Get.Url($"/api/shows/{postResponseBody.Id}");
            _.StatusCodeShouldBeOk();
        });

        var getResponseBody = await getResponse.ReadAsJsonAsync<AddShowResponse>();

        Assert.NotNull(getResponseBody);

        Assert.Equal(postResponseBody, getResponseBody);
    }
}