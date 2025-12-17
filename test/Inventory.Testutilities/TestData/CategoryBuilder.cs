using Bogus;
using Inventory.Domain.Entities;

namespace Inventory.TestUtilities.TestData
{
    public class CategoryBuilder
    {
        private string _name = Faker.Commerce.Categories(1)[0];
        private static readonly Faker Faker = new();

        public CategoryBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public Category Build()
            => new(_name);
    }
}
