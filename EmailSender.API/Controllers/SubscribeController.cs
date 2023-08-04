using EmailSender.API.Controllers.Base;
using EmailSender.UseCases.Users;
using EmaiSender.Core.Inderfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.API.Controllers
{
    
    public class SubscribeController : BaseApiController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SubscribeToTag(Guid tagId,
            [FromServices] ISubscribeToTagUseCase useCase,
            CancellationToken cancellationToken)
        {
            await useCase.Execute(new()
            {
                Name = User?.Identity?.Name,
                Email = UserEmail,
                Id = UserId
            }, tagId, cancellationToken);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTags(
            [FromServices] IEmailDbContext _context)
        {
            return Ok(_context.Tags.ToArray());
        }
    }
}
