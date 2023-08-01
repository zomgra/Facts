using AutoMapper;
using Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.Language.Flow;
using Storage.API.Data;
using Storage.Core;
using Storage.Core.Exceptions;
using Storage.Core.Mapper;
using Storage.Core.ViewModels;
using Storage.UseCases.Facts.CreateFact;
using System.Xml.Linq;
using Xunit;

namespace Stotage.Tests
{
    public class CreateFactsUseCaseShould
    {
        private readonly FactsDbContext _factDbContext;
        private readonly Mock<IMomentProvider> _momentProvider;
        private readonly ISetup<IGuidFactory, Guid> _getIdSetup;
        private readonly CreateFactsUseCase _sut;

        public CreateFactsUseCaseShould()
        {
            var mapper = new Mapper(new MapperConfiguration(x => x.AddProfile(new ApplicationMapper())));

            var options = new DbContextOptionsBuilder<FactsDbContext>().UseInMemoryDatabase(nameof(CreateFactsUseCaseShould));
            _factDbContext = new FactsDbContext(options.Options);

            _momentProvider = new Mock<IMomentProvider>();

            _momentProvider.Setup(x => x.Now).Returns(new DateTimeOffset(new DateTime(2022, 8, 20, 12, 33, 11)));

             var getIdSetup = new Mock<IGuidFactory>();
             _getIdSetup = getIdSetup.Setup(x => x.CreateGuid());

            _sut = new CreateFactsUseCase(_factDbContext, mapper, getIdSetup.Object, _momentProvider.Object );
        }

        [Fact]
        public async Task ThrowTagNotFoundException_WhenTagNotFound()
        {
            var content = "Hello world";
            var tagIds = new Guid[] {Guid.Parse("9F8989CB-65EF-4555-BEA6-16C9BD22CB19"), };

            await _sut.Invoking(async t => await t.Excecute(content, CancellationToken.None, tagIds))
                .Should().ThrowAsync<FactNotFoundException>();
        }
        [Fact]
        public async Task ReturnNewlyFactAndHaveOnlyOneTag()
        {
            var content = "Hello world";
            var tagIds = new Guid[] { Guid.Parse("9F8989CB-65EF-4555-BEA6-16C9BD22CB18"), };

            _factDbContext.Add(new Tag { CreatedAt = _momentProvider.Object.Now, TagId = tagIds[0], Name = "TestName" });
            _factDbContext.SaveChanges();

            var factId = Guid.Parse("5648b160-4d5a-4816-a994-8f97996ecb4b");
            _getIdSetup.Returns(factId);

            var newFact = await _sut.Excecute(content, CancellationToken.None, tagIds);
            newFact.Should().BeEquivalentTo(new FactViewModel
            {
                Tags = new List<TagViewModel> { new TagViewModel() 
                {
                    CreatedAt = _momentProvider.Object.Now, Id = tagIds[0], Name = "TestName",
                } 
                },
     
                Content = content, 
                Id = factId,
                CreatedAt = new DateTimeOffset(new DateTime(2022, 8, 20, 12, 33, 11)),
            });
            var countTags = _factDbContext.Tags.Count();
            countTags.Should().Be(1);
        }
    }
}
