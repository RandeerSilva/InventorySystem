using FluentAssertions;
using Inventory.Application.Abstractions.Persistence;
using Inventory.Application.Features.Products.Commands;
using Inventory.Domain.Entities;
using Inventory.TestUtilities.TestData;
using NSubstitute;

namespace Inventory.UnitTests.Application.Features.Products
{
    public  class CreateProductTests
    {
        [Fact]
        public async Task Should_Create_Product()
        {
            // Arrange
            var categoryRepo = Substitute.For<ICategoryRepository>();
            var productRepo = Substitute.For<IProductRepository>();

            categoryRepo.ExistsAsync(1, Arg.Any<CancellationToken>())
                .Returns(true);

            var handler = new CreateProductCommandHandler(
                categoryRepo, productRepo);

            var defaultProduct = new ProductBuilder().Build();
            var command = new CreateProductCommand(defaultProduct.Name, defaultProduct.Price, defaultProduct.CategoryId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await productRepo.Received(1)
                .AddAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        }

    }
}
