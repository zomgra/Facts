using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.API.Publisher;
using Storage.UseCases.Tags.CreateTag;
using Storage.UseCases.Tags.DeleteTag;

namespace Storage.API.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    public class TagController : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTagAsync(string name,
            CancellationToken cancellationToken,
            [FromServices] INewTagPublisher publisher,
            [FromServices] ICreateTagUseCase useCase)
        {
            var tag = await useCase.Execute(name, cancellationToken);
            if (tag != null)
                await publisher.Publish(tag);
            return Ok(tag);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTagAsync(Guid id,
            CancellationToken cancellationToken,
            [FromServices] IDeleteTagUseCase useCase)
        {
            var ok = await useCase.Execute(id, cancellationToken);
            return Ok();
        }
    }
}
