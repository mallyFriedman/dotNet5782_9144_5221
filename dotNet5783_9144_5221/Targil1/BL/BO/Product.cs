namespace BO
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public Enums.Category Category { get; set; }
        public int InStock { get; set; }

        public override string ToString() => $@"
        Product ID={Id}, 
        Product ProductName={ProductName}, 
    	Price: {Price}
    	Amount in stock: {InStock}
        category - {Category}";
    }
}
