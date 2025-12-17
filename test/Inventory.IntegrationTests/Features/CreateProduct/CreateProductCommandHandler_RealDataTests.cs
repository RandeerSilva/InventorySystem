using DotNet.Testcontainers.Builders;
using Inventory.Application.Features.Products.Commands;
using Inventory.Domain.Entities;
using Inventory.Infrastructure;
using Inventory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inventory.IntegrationTests.Features.CreateProduct
{
    public class CreateProductCommandHandlerRealDataTests : IAsyncLifetime,IDisposable
    {
        private InventoryDbContext? _dbContext;
        private readonly DotNet.Testcontainers.Containers.IContainer _pgContainer = new ContainerBuilder()
            .WithImage("postgres:15-alpine")
            .WithEnvironment("POSTGRES_DB", "testdb")
            .WithEnvironment("POSTGRES_USER", "testuser")
            .WithEnvironment("POSTGRES_PASSWORD", "testpass")
            .WithPortBinding(5432, true)
            .Build();

        public async Task InitializeAsync()
        {
            await _pgContainer.StartAsync().ConfigureAwait(false);

            var hostPort = _pgContainer.GetMappedPublicPort(5432);
            var connectionString = $"Host=localhost;Port={hostPort};Database=testdb;Username=testuser;Password=testpass;";
            var options = new DbContextOptionsBuilder<InventoryDbContext>()
                .UseNpgsql(connectionString)
                .Options;

            _dbContext = new InventoryDbContext(options);
            await _dbContext.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            await _pgContainer.DisposeAsync();
        }

        [Fact]
        public async Task Handle_CreatesProduct_WhenCategoryExists()
        {
            // Arrange
            if (_dbContext == null) throw new InvalidOperationException("DbContext is not initialized.");
            var category = new Category("Electronics");
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            var categoryRepo = new CategoryRepository(_dbContext);
            var productRepo = new ProductRepository(_dbContext);

            var command = new CreateProductCommand(
                "Smartphone",
                299.99m,
                category.Id
            );

            var handler = new CreateProductCommandHandler(categoryRepo, productRepo);

            // Act
            var productId = await handler.Handle(command, CancellationToken.None);

            // Assert
            var product = await _dbContext.Products.FindAsync(productId);
            Assert.NotNull(product);
            Assert.Equal("Smartphone", product.Name);
            Assert.Equal(299.99m, product.Price);
            Assert.Equal(category.Id, product.CategoryId);
        }

        [Fact]
        public async Task Handle_ThrowsException_WhenCategoryDoesNotExist()
        {
            // Arrange
            if (_dbContext == null) throw new InvalidOperationException("DbContext is not initialized.");
            var categoryRepo = new CategoryRepository(_dbContext);
            var productRepo = new ProductRepository(_dbContext);

            var command = new CreateProductCommand(
                "Tablet",
                199.99m,
                999 // Non-existent
            );

            var handler = new CreateProductCommandHandler(categoryRepo, productRepo);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
