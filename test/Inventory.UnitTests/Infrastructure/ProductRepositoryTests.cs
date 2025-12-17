using Inventory.Domain.Entities;
using Inventory.Infrastructure;
using Inventory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inventory.UnitTests.Infrastructure
{
    public class ProductRepositoryTests
    {
        private static InventoryDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<InventoryDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
                .Options;
            return new InventoryDbContext(options);
        }

        [Fact]
        public async Task AddAsync_AddsProductToDatabase()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var repository = new ProductRepository(context);
            var product = new Product { Id = 1, Name = "Test Product" };
            var ct = CancellationToken.None;

            // Act
            await repository.AddAsync(product, ct);

            // Assert
            var savedProduct = await context.Products.FindAsync(new object[] { 1 }, ct);
            Assert.NotNull(savedProduct);
            Assert.Equal("Test Product", savedProduct.Name);
        }
    }
}
