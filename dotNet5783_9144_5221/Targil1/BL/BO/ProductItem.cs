
namespace BO
{
    public class ProductItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public bool InStock { get; set; }
        public int AmountInCart { get; set; }

        public override string ToString() => $@"
        Product ID={Id}, 
        Product ProductName={ProductName}, 
    	Price: {Price}
        Category - {Category}
        In stock? {InStock}
        Amount in cart= {AmountInCart}";
        
    }
}

