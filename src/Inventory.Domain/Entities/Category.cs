namespace Inventory.Domain.Entities
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = default!;

        public Category() { }

        public Category(string name)
        {
            Name = name;
        }
    }
}
