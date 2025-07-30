
using Alba;
using Shows.Api.Api.Shows;
using Shows.Tests.Api.Fixtures;

namespace Shows.Tests.Api.Shows;

[Trait("Category", "SystemTest")]
[Collection("SystemTestFixture")]
public class GettingAllShows(SystemTestFixture fixture)
{
    private readonly IAlbaHost _host = fixture.Host;

    [Fact]
    public async Task Get()
    {
        var showToAdd = new AddShowRequest
        {
            Name = "Foundation",
            Description = "Sci Fi Based on Asimov's Foundation Books",
            StreamingService = "Apple Tv"
        };
        var postResponse = await _host.Scenario(api =>
        {
            api.Post.Json(showToAdd).ToUrl("/api/shows");
        });

        var postResponsebody = await postResponse.ReadAsJsonAsync<AddShowResponse>();
        Assert.NotNull(postResponsebody);
        var response = await _host.Scenario(api =>
        {
           
            api.Get.Url("/api/shows");
            api.StatusCodeShouldBeOk();
        });

        var allShows = await response.ReadAsJsonAsync<IReadOnlyList<AddShowResponse>>();
        Assert.NotNull(allShows);

        // Assert.Contains(allShows, s => s == postResponsebody);

        var firstShow = allShows.Take(1).First();
        Assert.Equal(postResponsebody, firstShow);

    }
}
