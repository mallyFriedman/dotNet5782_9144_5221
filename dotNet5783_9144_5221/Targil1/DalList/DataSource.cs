using DalFacade;
namespace DalList;


public  class DataSource
{

    static DataSource()
    {
        s_Initialize();
    }

    public static class Config
    {

        internal static int IndexArrProduct = 0;
        internal static int IndexArrOrder = 0;
        internal static int IndexArrOrderItem = 0;

        private static int ProductId = 0;
        private static int OrderId = 0;
        private static int OrderItemId = 0;

        public static int productId { get { return ++ProductId;  } set { ProductId = ++ProductId; } }
        public static int orderId { get { return ++OrderId; } set { OrderId = ++OrderId; } }
        public static int orderItemId { get { return ++OrderItemId; } set { OrderItemId = ++OrderItemId; } }
        public static int indexArrProduct { get { return ++IndexArrProduct; } set { IndexArrProduct = ++IndexArrProduct; } }
        public static int indexArrOrder { get { return ++IndexArrOrder; } set { IndexArrOrder= ++IndexArrOrder; } }
        public static int indexArrOrderItem { get { return ++IndexArrOrderItem; } set { IndexArrOrderItem = ++IndexArrOrderItem; } }
    }
    const int NumOfProduct = 50; // אפשר לעשות מחלקה של קבועים
    const int NumOfOrder = 50;
    const int NumOfOrderItem = 50;
    public static Product[] ProductList = new Product[NumOfProduct];
    public static Order[] orderArr = new Order[NumOfOrder];
    public static OrderItem[] orderitem = new OrderItem[NumOfOrderItem];
    static readonly Random rand = new Random();
    static string[] CustomerName = { "a","b","c","d","e","f","k","l","m","n"
                ,"o","p","q","r","s","t","u","v","x","w"};
    static string[] productNames = { "NecklacesGold","NecklacesSilver","BraceletsGold","BraceletsSilver",
       "EarringsGold","EarringsSilver","RingsGold","RingsSilver","WatchGold","WatchSilver"};

    static public void s_Initialize()
    {
        CreateProductList();
        CreateOrderList();
        pushOrderItem();
    }
    static public void CreateProductList()
    {
        for (int i = 0; i < 10; i++)
        {
            Product product = new Product();
            // הגרלנו מספר שהוא מיקום במערך השמות
            int number = (int)rand.NextInt64(productNames.Length);
            int id = Config.productId;
            //int id = 2;
            int InStock = (int)rand.NextInt64(0, 50);
            int price = (int)rand.NextInt64(6000, 7000);
            product.ProductName = productNames[number];
            product.ID = id;
            product.InStock = InStock;
            product.Price = price;
            ProductList[Config.indexArrProduct] = product;
        }
    }
    static public void CreateOrderList()
    {
        TimeSpan t_ShipDate = TimeSpan.FromDays(10);
        TimeSpan t_DeliveryDate = TimeSpan.FromDays(30);
        for (int i = 0; i < 20; i++)
        {
            Order orderi = new Order();
            // הגרלנו מספר שהוא מיקום במערך השמות
            int number = (int)rand.NextInt64(CustomerName.Length);
            int id = Config.orderId;
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
    static public void pushOrderItem()
    {
        for (int i = 0; i < 20; i++)
        {
            OrderItem orderItemi = new OrderItem();
            int orderId = orderArr[i].ID;
            int min = (int)rand.NextInt64(1, 4);
           
            for (int j = 0; j < min; j++)
            {
                int id = Config.orderItemId;
                int numberPoduct = (int)rand.NextInt64(0, Config.IndexArrProduct);
                int amount = (int)rand.NextInt64(0,ProductList[numberPoduct].InStock);
                orderItemi.ID = id;
                orderItemi.OrderID = orderId;
                orderItemi.ProductID = ProductList[numberPoduct].ID;
                orderItemi.Price = ProductList[numberPoduct].Price;
                orderItemi.Amount = amount+1;
                ProductList[numberPoduct].InStock = ProductList[numberPoduct].InStock - amount;
                orderitem[Config.indexArrOrderItem] = orderItemi;
                ///////////////update the source ????????????
            }

        }
    }
  



}
/*לעשות בדיקה שלא יוצא יותר מבן אדם אחד עם אותו איידי
/* string[] CustomerEmail = { "a@gmail.com","b@gmail.com","c@gmail.com","d@gmail.com","e@gmail.com"
           ,"f@gmail.com","k@gmail.com","l@gmail.com","m@gmail.com","n@gmail.com"
           ,"o@gmail.com","p@gmail.com","q@gmail.com","r@gmail.com","s@gmail.com","t@gmail.com"
           ,"u@gmail.com","v@gmail.com","x@gmail.com","w@gmail.com"};*/

