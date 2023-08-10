using EmailSender.API.Controllers.Base;
using EmailSender.UseCases.Tags;
using EmailSender.UseCases.Users;
using EmaiSender.Core.Inderfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmailSender.API.Controllers
{
    public class SubscribeController : BaseApiController
    {
        string UserEmail => User?.FindFirstValue(ClaimTypes.Email);

        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SubscribeToTag([FromBody]Guid tagId,
            [FromServices] ISubscribeToTagUseCase useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.Execute(new()
            {
                Name = FullNameUser,
                Email = UserEmail,
                Id = UserId
            }, tagId, cancellationToken);
            return result ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTags(
            [FromServices] IEmailDbContext _context)
        {
            return Ok(_context.Tags.ToArray());
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSubscribedUserToTag(Guid id,
            [FromServices] IGetSubscribedUsersByTagIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var users = await useCase.Execute(id, cancellationToken);
            return Ok(users);
        }
        [HttpGet]
        public async Task<IActionResult> GetSubscribedUserByNameTagUseCase(string tagName,
            [FromServices] IGetSubscribedUserByNameTagUseCase useCase,
            CancellationToken cancellationToken)
        {
            var users = await useCase.Execute(tagName, cancellationToken);
            return Ok(users);
        }
        [HttpPut]
        public async Task<IActionResult> UnSubscribe(string userId, Guid tagId)
        {
            return Ok(userId + tagId);
        }
    }
}
