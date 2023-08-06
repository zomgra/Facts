using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.API.Publisher;
using Storage.Core.ViewModels;
using Storage.UseCases.Tags.CreateTag;
using Storage.UseCases.Tags.DeleteTag;
using Storage.UseCases.Tags.GetTagsList;

namespace Storage.API.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    public class TagController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(statusCode: 200, type: typeof(List<TagViewModel>))]
        public async Task<IActionResult> GetAllTagsAsync(int page, 
            CancellationToken cancellationToken,
            [FromServices] IGetTagsListUseCase useCase)
        {
            var tags = await useCase.Execute(page, cancellationToken);
            return Ok(tags);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(statusCode: 200, type: typeof(TagViewModel))]
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
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 400)]
        [HttpDelete]
        public async Task<IActionResult> DeleteTagAsync(Guid id,
            CancellationToken cancellationToken,
            [FromServices] IDeleteTagUseCase useCase)
        {
            var ok = await useCase.Execute(id, cancellationToken);
            return ok ? Ok() : BadRequest();
        }
        
    }
}
