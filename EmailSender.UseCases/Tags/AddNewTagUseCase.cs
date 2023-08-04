using EmaiSender.Core.Exceptions;
using EmaiSender.Core.Inderfaces;
using EmaiSender.Core.Models;
using EmaiSender.Core.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.UseCases.Tags
{
    public class AddNewTagUseCase : IAddNewTagUseCase
    {
        private readonly IEmailDbContext _context;

        public AddNewTagUseCase(IEmailDbContext context)
        {
            _context = context;
        }

        public async Task Execute(TagViewModel tagViewModel, CancellationToken cancellationToken)
        {
            var existsTag = await _context.Tags.AnyAsync(x => x.Name == tagViewModel.Name || x.TagId == tagViewModel.Id);
            if (existsTag)
            {
                throw new TagAlreadyExistsException($"Tag with name: {tagViewModel.Name} already exists");
            };
            try
            {
                var newTag = new Tag()
                {
                    Name = tagViewModel.Name.Trim().ToUpper(),
                    TagId = tagViewModel.Id,
                    CreatedAt = tagViewModel.CreatedAt,
                };
                await _context.Tags.AddAsync(newTag);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            { 

                throw;
            }
        }
    }
}
