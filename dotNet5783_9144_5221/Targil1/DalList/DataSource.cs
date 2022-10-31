
namespace DO.Dal;


public class DataSource
{
    static DataSource()
    {
       // s_Initialize();
    }

    internal static class Config
    {

        internal static int IndexArrProduct = 0;
        internal static int IndexArrOrder = 0;
        internal static int IndexArrOrderItem = 0;

        private static int ProductId = 0;
        private static int OrderId = 0;
        private static int OrderItemId = 0;

        public static int productId { get => ProductId++; }
        public static int orderId { get => OrderId++; }
        public static int orderItemId { get => OrderItemId++; }
        public static int indexArrProduct { get => IndexArrProduct++; }
        public static int indexArrOrder { get => IndexArrOrder++; }
        public static int indexArrOrderItem { get => IndexArrOrderItem++; }
    }
    //static int readonly=1;
    const int NumOfProduct = 50; // אפשר לעשות מחלקה של קבועים
    const int NumOfOrder = 50;
    const int NumOfOrderItem = 50;
    public static Product[] ProductList = new Product[NumOfProduct];
    public static Order[] orderArr = new Order[NumOfOrder];
    public static OrderItem[] orderitem = new OrderItem[NumOfOrderItem];
    Random rand = new Random();
    string[] CustomerName = { "a","b","c","d","e","f","k","l","m","n"
                ,"o","p","q","r","s","t","u","v","x","w"};
    string[] productNames = { "NecklacesGold","NecklacesSilver","BraceletsGold","BraceletsSilver",
       "EarringsGold","EarringsSilver","RingsGold","RingsSilver","WatchGold","WatchSilver"};
    int[] productId = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    TimeSpan t_ShipDate = TimeSpan.FromDays(10);
    TimeSpan t_DeliveryDate = TimeSpan.FromDays(30);
    public void s_Initialize()
    {
        CreateProductList();
        CreateOrderList();
        pushOrderItem();
    }
    public void CreateProductList()
    {
        for (int i = 0; i < 10; i++)
        {
            Product product = new Product();
            // הגרלנו מספר שהוא מיקום במערך השמות
            int number = (int)rand.NextInt64(productNames.Length);
            int id = (int)rand.NextInt64(productId.Length);
            int InStock = (int)rand.NextInt64(0, 50);
            int price = (int)rand.NextInt64(6000, 7000);
            product.ProductName = productNames[number];
            product.ID = productId[id];
            product.InStock = InStock;
            product.Price = productId[price];
            ProductList[Config.indexArrProduct] = product;
        }
    }
    public void CreateOrderList()
    {
        for (int i = 0; i < 20; i++)
        {
            Order orderi = new Order();
            // הגרלנו מספר שהוא מיקום במערך השמות
            int number = (int)rand.NextInt64(CustomerName.Length);
            int id = (int)rand.NextInt64(0, 19);
            orderi.CustomerName = CustomerName[number];
            orderi.ID = id;
            orderi.CustomerEmail = CustomerName[number] + "@gmail.com";
            orderi.CustomerAdress = CustomerName[number] + "Street";
            orderi.OrderDate = DateTime.MinValue;
            orderi.ShipDate = (orderArr[i].OrderDate + t_ShipDate);
            orderi.DeliveryDate = (orderArr[i].ShipDate + t_DeliveryDate);
            orderArr[Config.indexArrOrder] = orderi;
        }

    }
    public void pushOrderItem()
    {
        for (int i = 0; i < 20; i++)
        {
            OrderItem orderItemi = new OrderItem();
            int orderId = orderArr[i].ID;
            int min = (int)rand.NextInt64(1, 4);
            for (int j = 0; j < min; j++)
            {
                int numberPoduct = (int)rand.NextInt64(0, ProductList.Length);
                int amount = (int)rand.NextInt64(0, ProductList[numberPoduct].InStock);
                orderItemi.OrderID = orderId;
                orderItemi.ProductID = ProductList[numberPoduct].ID;
                orderItemi.Price = ProductList[numberPoduct].Price;
                orderItemi.Amount = amount;
                ProductList[numberPoduct].InStock = ProductList[numberPoduct].InStock - amount;
            }
        }
    }
  



}
/*לעשות בדיקה שלא יוצא יותר מבן אדם אחד עם אותו איידי
/* string[] CustomerEmail = { "a@gmail.com","b@gmail.com","c@gmail.com","d@gmail.com","e@gmail.com"
           ,"f@gmail.com","k@gmail.com","l@gmail.com","m@gmail.com","n@gmail.com"
           ,"o@gmail.com","p@gmail.com","q@gmail.com","r@gmail.com","s@gmail.com","t@gmail.com"
           ,"u@gmail.com","v@gmail.com","x@gmail.com","w@gmail.com"};*/

