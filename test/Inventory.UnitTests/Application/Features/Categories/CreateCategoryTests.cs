using Inventory.Application.Abstractions.Persistence;
using Inventory.Application.Features.Categories.Commands.CreateCategory;
using Inventory.Domain.Entities;
using Inventory.TestUtilities.TestData;
using NSubstitute;

namespace Inventory.UnitTests.Application.Features.Categories
{
    public class CreateCategoryTests
    {
        [Fact]
        public async Task Should_Create_Category()
        {
            // Arrange
            var repo = Substitute.For<ICategoryRepository>();
            var defaultCategory = new CategoryBuilder().Build();
            var handler = new CreateCategoryCommandHandler(repo);
            var command = new CreateCategoryCommand("Electronics");

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await repo.Received(1)
                .AddAsync(Arg.Any<Category>(), Arg.Any<CancellationToken>());
        }
    }
}
