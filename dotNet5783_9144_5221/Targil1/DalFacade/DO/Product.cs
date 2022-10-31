

namespace Dal;

    public struct Product
{
    public int ID { get; set; }
    public string ProductName { get; set; }
    public double Price { get; set; }
  //  public Category category { get; set; }
    public int InStock { get; set; }

    public override string ToString() => $@"
        Product ID={ID}, 
        Product ProductName={ProductName}, 
    	Price: {Price}
    	Amount in stock: {InStock}";
    }
/*category - {Category}*/



