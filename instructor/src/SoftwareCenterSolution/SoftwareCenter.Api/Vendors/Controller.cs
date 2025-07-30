using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCenter.Api.Vendors;


[Authorize] // no longer allow anonymous people to do anything here.
public class Controller(IDocumentSession session) : ControllerBase
{

    // this is the method you should call when a POST /vendors is received.
    [HttpPost("/vendors")]
    [Authorize(Policy = "CanAddVendor")]
    public async Task<ActionResult> AddAVendorAsync(
        [FromBody] CreateVendorRequest request,
        [FromServices] IValidator<CreateVendorRequest> validator,
        CancellationToken token)
    {
        //if(!ModelState.IsValid)
        // {
        //     return BadRequest(ModelState);
        // }

        var validationResults = await validator.ValidateAsync(request);
        if(!validationResults.IsValid)
        {
            return BadRequest(validationResults);
        }
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
        session.Store(response);
        await session.SaveChangesAsync();
        return Ok(response); 
    }


    [HttpGet("/vendors/{id:guid}")]
    public async Task<ActionResult> GetVendorByIdAsync(Guid id, CancellationToken token)
    {
        // look that thing up in the database.
        var response = await session
            .Query<CreateVendorResponse>()
            .Where(v => v.Id == id)
            .SingleOrDefaultAsync();

        if (response is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(response);
        }
    }

    [HttpGet("/vendors")]
    public async Task<ActionResult> GetAllVendorsAsync(CancellationToken token)
    {
        var vendors = await session.Query<CreateVendorResponse>().ToListAsync();

        return Ok(vendors);
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


public record CreateVendorRequest
{
   
    public string Name { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
    public CreateVendorPointOfContactRequest PointOfContact { get; init; } = new();
}

public class CreateVendorRequestValidator : AbstractValidator<CreateVendorRequest>
{
    public CreateVendorRequestValidator()
    {
        RuleFor(v => v.Name).NotEmpty().MinimumLength(3).MaximumLength(255);
        RuleFor(v => v.Url).NotEmpty();
        RuleFor(v => v.PointOfContact).NotNull();
    }
}

public record CreateVendorPointOfContactRequest
{
    public string Name { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
};

public class CreateVendorPointOfContactRequestValidator :
    AbstractValidator<CreateVendorPointOfContactRequest>
{
    public CreateVendorPointOfContactRequestValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
 
    }
}

public record CreateVendorResponse(
    Guid Id,
    string Name, string Url, CreateVendorPointOfContactRequest PointOfContact
    );