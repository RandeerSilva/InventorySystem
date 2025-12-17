using Inventory.Domain.Entities;

namespace Inventory.TestUtilities.TestData
{
    public class ProductTestContext
    {
        public static Product CreateDefaultProduct()
        {
            return new Product
            {
                Id = 1,
                Name = "TestProduct-" + Guid.NewGuid(),
                Price = 250000,
                CategoryId = 1
            };
        }
    }

}
