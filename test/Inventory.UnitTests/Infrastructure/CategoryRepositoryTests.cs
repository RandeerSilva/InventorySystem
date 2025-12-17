using Inventory.Domain.Entities;
using Inventory.Infrastructure;
using Inventory.Infrastructure.Repositories;
using Inventory.TestUtilities.TestData;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Inventory.UnitTests.Infrastructure
{
    public class CategoryRepositoryTests
    {
        private static InventoryDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<InventoryDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + System.Guid.NewGuid())
                .Options;
            return new InventoryDbContext(options);
        }

        private static Category CreateCategory(int id, string name)
        {
            var category = (Category)Activator.CreateInstance(typeof(Category), true)!;
            typeof(Category).GetProperty(nameof(Category.Id))!
                .SetValue(category, id, null);
            typeof(Category).GetProperty(nameof(Category.Name))!
                .SetValue(category, name, null);
            return category;
        }

        [Fact]
        public async Task ExistsAsync_ReturnsTrue_WhenCategoryExists()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var category = new CategoryBuilder().Build()
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            var repo = new CategoryRepository(context);

            // Act
            var exists = await repo.ExistsAsync(1, CancellationToken.None);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task ExistsAsync_ReturnsFalse_WhenCategoryDoesNotExist()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var repo = new CategoryRepository(context);

            // Act
            var exists = await repo.ExistsAsync(99, CancellationToken.None);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task AddAsync_AddsCategoryToDatabase()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var repo = new CategoryRepository(context);
            var category = CreateCategory(2, "New Category");

            // Act
            await repo.AddAsync(category, CancellationToken.None);

            // Assert
            var exists = await context.Categories.AnyAsync(c => c.Id == 2);
            Assert.True(exists);
        }
    }
}
