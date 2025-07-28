using Microsoft.AspNetCore.Mvc;

namespace SoftwareCenter.Api.Vendors;

public class Controller : ControllerBase
{
    // this is the method you should call when a POST /vendors is received.
    [HttpPost("/vendors")]
    public async Task<ActionResult> AddAVendorAsync(
        [FromBody] CreateVendorRequest request,
        CancellationToken token)
    {
        // Mapping Code (copy from one object to another)
        var response = new CreateVendorResponse(
            Guid.NewGuid(),
            request.Name,
            request.Url,
            request.PointOfContact
            );
        return Ok(response); 
    }

}

/*{
    "name": "Microsoft",
    "url": "https://wwww.microsoft.com",
    "pointOfContact": {
        "name": "Satya",
        "phone": "800 big-boss",
        "email": "satya@microsoft.com"
    }
}*/


public record CreateVendorRequest(
    string Name, string Url, CreateVendorPointOfContactRequest PointOfContact);

public record CreateVendorPointOfContactRequest(string Name, string Phone, string Email);


public record CreateVendorResponse(
    Guid Id,
    string Name, string Url, CreateVendorPointOfContactRequest PointOfContact
    );