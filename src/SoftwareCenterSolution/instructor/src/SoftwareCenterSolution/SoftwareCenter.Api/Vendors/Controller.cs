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

        // validation
        // You can't add a vendor with the same name more than once.
        // field validation - what is required, what is optional, what are the rules for the required things
        // domain validation - we don't already have a vendor with that same name
        // 
        // we have to "save it" somewhere. 
        // Mapping Code (copy from one object to another)
        var response = new CreateVendorResponse(
            Guid.NewGuid(),
            request.Name,
            request.Url,
            request.PointOfContact
            );
        return Ok(response); 
    }


    // GET /vendors/tacos
    [HttpGet("/vendors/{id:guid}")]
    public async Task<ActionResult> GetVendorByIdAsync(Guid id, CancellationToken token)
    {
        return Ok();
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