using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace Shows.Api.Api.Shows;

public class Controller(IDocumentSession session) : ControllerBase
{
    [HttpPost("/api/shows")]
    public async Task<ActionResult> AddShowAsync(
        [FromBody] AddShowRequest request,
        [FromServices] IValidator<AddShowRequest> validator)
    {
        var validation = await validator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            return BadRequest();
        }
        var response = new AddShowResponse
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            StreamingService = request.StreamingService,
            CreatedAt = DateTimeOffset.UtcNow
        };
        session.Store(response);
        await session.SaveChangesAsync();
        return Ok(response);
    }

    [HttpGet("/api/shows/{id:guid}")]
    public async Task<ActionResult> GetShowByIdAsync(Guid id, CancellationToken token)
    {
        var show = await session.Query<AddShowResponse>().Where(s => s.Id == id).SingleOrDefaultAsync(token);
        if (show == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(show);
        }
    }

    [HttpGet("/api/shows")]
    public async Task<ActionResult> GetAllShows(CancellationToken token)
    {
        var response = await session
            .Query<AddShowResponse>()
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync(token);
        return Ok(response);
    }
}


public class AddShowRequest
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string StreamingService { get; init; } = string.Empty;
}

public class AddShowRequestValidator : AbstractValidator<AddShowRequest>
{
    public AddShowRequestValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(c => c.Description).NotEmpty().MinimumLength(10).MaximumLength(500);
        RuleFor(c => c.StreamingService).NotEmpty();
    }
}

public record AddShowResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string StreamingService { get; init; } = string.Empty;
    public DateTimeOffset CreatedAt { get; init; }

}