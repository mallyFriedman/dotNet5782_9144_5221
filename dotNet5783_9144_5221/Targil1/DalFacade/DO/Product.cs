

namespace DO;

    public struct Product
    {
    public int ID { get; set; }
    public string ProductName { get; set; }
    public double Price { get; set; }
  //  public Category category { get; set; }
    public int InStock { get; set; }

    public override string ToString() => $@"
        Product ID={ID}: {Name}, 
        category - {Category}
    	Price: {Price}
    	Amount in stock: {InStock}";
    }


