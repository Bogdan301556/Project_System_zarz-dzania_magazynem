public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }

    public Product(int id, string name, int quantity)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
    }
}
