using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            [FromServices] ICreateTagUseCase useCase)
        {
            var tag = await useCase.Execute(name, cancellationToken);
            return Ok(tag);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTagAsync(Guid id,
            CancellationToken cancellationToken,
            [FromServices] IDeleteTagUseCase useCase)
        {
            var tag = await useCase.Execute(id, cancellationToken);
            return Ok(tag);
        }
    }
}
