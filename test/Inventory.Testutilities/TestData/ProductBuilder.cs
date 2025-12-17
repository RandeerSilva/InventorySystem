using System.Threading.Tasks;
using Inventory.Domain.Entities;
using Inventory.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Inventory.TestUtilities.TestData
{
    public class ProductBuilder
    {
        private string _name = Faker.Commerce.ProductName();
        private decimal _price = Faker.Random.Decimal(1000, 500_000);
        private int _categoryId = 1;

        private static readonly Bogus.Faker Faker = new();

        public ProductBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ProductBuilder WithPrice(decimal price)
        {
            _price = price;
            return this;
        }

        public ProductBuilder WithCategory(int categoryId)
        {
            _categoryId = categoryId;
            return this;
        }

        public Product Build()
            => new(_name, _price, _categoryId);

        public async Task<Product> CreateAsync(InventoryDbContext dbContext)
        {
            var product = Build();
            dbContext.Set<Product>().Add(product);
            await dbContext.SaveChangesAsync();
            return product;
        }
    }
}
