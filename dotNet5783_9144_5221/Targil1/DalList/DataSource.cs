
namespace DO.Dal;


public class DataSource
{
 //static int readonly=1;
 const int NumOfProduct = 50; // אפשר לעשות מחלקה של קבועים
    const int NumOfOrder = 50;
    Product[] ProductList = new Product[NumOfProduct];
    static Order[] order;
 static OrderItem[] orderitem;
Random rand = new Random();
    string[] CustomerName = { "a","b","c","d","e","f","k","l","m","n"
                ,"o","p","q","r","s","t","u","v","x","w"};
    int[] productId = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    string[] productNames = { "NecklacesGold","NecklacesSilver","BraceletsGold","BraceletsSilver",
       "EarringsGold","EarringsSilver","RingsGold","RingsSilver","WatchGold","WatchSilver"};
    TimeSpan t_ShipDate = TimeSpan.FromDays(10);
    TimeSpan t_DeliveryDate = TimeSpan.FromDays(30);
    public void CreateProductList()
{
    for (int i = 0; i < 10; i++)
    {
        ProductList[i] = new Product();
        // הגרלנו מספר שהוא מיקום במערך השמות
        int number =(int) rand.NextInt64(productNames.Length);
        int id =(int) rand.NextInt64(productId.Length);
        int InStock =(int) rand.NextInt64(0,50);
        int price =(int) rand.NextInt64(6000,7000);
        ProductList[i].ProductName = productNames[number];
        ProductList[i].ID = productId[id];
        ProductList[i].InStock = InStock;
        ProductList[i].Price = productId[price];
    }
}
    void CreateOrderList(Order order){
        for (int i = 0; i < 20; i++)
    {
            order[i] = new Order();
        // הגרלנו מספר שהוא מיקום במערך השמות
        int number =(int) rand.NextInt64(CustomerName.Length);
        int id =(int) rand.NextInt64(0,19);
        order[i].CustomerName = CustomerName[number];
        order[i].ID = id;
        order[i].CustomerEmail = CustomerName[number] + "@gmail.com";
        order[i].CustomerAdress = CustomerName[number] + "Street";
        order[i].ShipDate = id;
        order[i].DeliveryDate = id;
        order[i].OrderDate = DateTime.MinValue;
        order[i].ShipDate = (order[i].OrderDate + t_ShipDate);
        order[i].DeliveryDate = (order[i].ShipDate + t_DeliveryDate);
        }

}
    void pushOrderItem(OrderItem orderItem){
        for (int i = 0; i < 20; i++)
        {
            orderItem[i] = new OrderItem();
            int orderId = order[i].ID;
            int min =(int) rand.NextInt64(1,4);
            for (int j = 0; j < min; j++)
            {
                int numberPoduct = (int)rand.NextInt64(0, ProductList.Length);
                int amount = (int)rand.NextInt64(0, ProductList[numberPoduct].InStock);
                orderItem[i].OrderID = orderId;
                orderItem[i].ProductID = ProductList[numberPoduct].ID;
                orderItem[i].Price = ProductList[numberPoduct].Price;
                orderItem[i].Amount = amount;
                ProductList[numberPoduct].InStock = ProductList[numberPoduct].InStock - amount;
            }
        }
    }
    private s_Initialize() { }
}
/*לעשות בדיקה שלא יוצא יותר מבן אדם אחד עם אותו איידי
/* string[] CustomerEmail = { "a@gmail.com","b@gmail.com","c@gmail.com","d@gmail.com","e@gmail.com"
           ,"f@gmail.com","k@gmail.com","l@gmail.com","m@gmail.com","n@gmail.com"
           ,"o@gmail.com","p@gmail.com","q@gmail.com","r@gmail.com","s@gmail.com","t@gmail.com"
           ,"u@gmail.com","v@gmail.com","x@gmail.com","w@gmail.com"};*/

