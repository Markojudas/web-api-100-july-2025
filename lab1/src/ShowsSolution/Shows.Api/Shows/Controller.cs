using Marten;
using Microsoft.AspNetCore.Mvc;

namespace Shows.Api.Shows
{
    public class Controller(IDocumentSession session): ControllerBase
    {
        [HttpPost("/api/shows")]
        public async Task<ActionResult> AddAShowAsync(
            [FromBody] AddShowRequest request)
        {
            var res = new AddShowResponse
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                StreamingService = request.StreamingService,
                CreatedAt = DateTimeOffset.UtcNow
            };
            session.Store(res);
            await session.SaveChangesAsync();
            return Ok(res);
        }

        [HttpGet("/api/shows/{id:guid}")]
        public async Task<ActionResult> GetShowById(Guid id)
        {
            var response = await session
                .Query<AddShowResponse>()
                .Where(session => session.Id == id)
                .SingleOrDefaultAsync();

            if (response == null) { 
                return NotFound();
            }
            else
            {
                return Ok(response);
            }
        }    
    }

    public record  AddShowRequest
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string StreamingService { get; init; } = string.Empty;
    }

    public record AddShowResponse
    {
        public Guid Id { get; set; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string StreamingService { get; init; } = string.Empty;
        public DateTimeOffset CreatedAt { get; init; }
    }
}
