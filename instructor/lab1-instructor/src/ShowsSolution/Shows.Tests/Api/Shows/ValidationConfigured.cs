
using Alba;
using Shows.Api.Api.Shows;
using Shows.Tests.Api.Fixtures;

namespace Shows.Tests.Api.Shows;

[Trait("Category", "SystemTest")]
[Collection("SystemTestFixture")]
public  class ValidationConfigured(SystemTestFixture fixture)
{
    private readonly IAlbaHost _host = fixture.Host;

    [Fact]
    public async Task ValidationIsDone()
    {
        await _host.Scenario(api =>
        {
            api.Post.Json(new AddShowRequest()).ToUrl("/api/shows");
            api.StatusCodeShouldBe(400);
        });
    }
}
