using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alba;
using Microsoft.Extensions.Hosting;
using SoftwareCenter.Api.Vendors;

namespace SoftwareCenter.Tests.Vendors
{
    public class AddAVenodor
    {
        [Fact]
        public async Task WeGetASuccessStatusCode()
        {
            var host = await AlbaHost.For<Program>();
            var vendorToCreate = new CreateVendorRequest("Microsoft", "https://www.microsoft.com", new CreateVendorPointOfContactRequest("Satya", "800 big-corp", "satya@microsoft.com"));
            await host.Scenario(api =>
            {
                api.Post.Json(vendorToCreate).ToUrl("/vendors");
                api.StatusCodeShouldBeOk();
            });
        }
    }
}
