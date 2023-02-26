using DO;
namespace Dal;


public static class DataSource ///internal ???????????
{
    static DataSource()
    {
        s_Initialize();
    }
    public static class Config  
    {
        internal static int _indexArrProduct = 0;
        internal static int _indexArrOrder = 0;
        internal static int _indexArrOrderItem = 0;

        private static int _productId = 100000;
        private static int _orderId = 500000;        
        private static int _orderItemId = 100000;

        public static int ProductId { get { return ++_productId; } }
        public static int OrderId { get { return ++_orderId; } }
        public static int OrderItemId { get { return ++_orderItemId; } }

        public static int IndexArrProduct { get { return ++_indexArrProduct; } set { _indexArrProduct = ++_indexArrProduct; } }
        public static int IndexArrOrder { get { return ++_indexArrOrder; } set { _indexArrOrder = ++_indexArrOrder; } }
        public static int IndexArrOrderItem { get { return ++_indexArrOrderItem; } set { _indexArrOrderItem = ++_indexArrOrderItem; } }
    }
    const int NumOfProduct = 50; // אפשר לעשות מחלקה של קבועים
    const int NumOfOrder = 100;
    const int NumOfOrderItem = 200;

    public static List<Product> ProductList = new List<Product>();
    public static List<Order> OrderArr = new List<Order>();
    public static List<OrderItem> OrderItems = new List<OrderItem>();
    static readonly Random rand = new Random();             
    static string[] customerName = { "a","b","c","d","e","f","k","l","m","n"
                ,"o","p","q","r","s","t","u","v","x","w"};
    static string[] productNames = { "NecklacesGold","NecklacesSilver","BraceletsGold","BraceletsSilver",
       "EarringsGold","EarringsSilver","RingsGold","RingsSilver","WatchGold","WatchSilver"};

    static public void s_Initialize()
    {
        CreateProductList();
        CreateOrderList();
        PushOrderItem();
    }
    static public void CreateProductList()
    {
        for (int i = 0; i < 10; i++)
        {
            Product product = new Product();
            int number = (int)rand.NextInt64(productNames.Length);
            int category = (int)rand.NextInt64(0, 4);
            int id = Config.ProductId;
            int inStock = (int)rand.NextInt64(0, 50);
            int price = (int)rand.NextInt64(1000, 9000);
            product.ProductName = productNames[number];
            product.Id = id;
            product.Category = (Enums.Category)category;
            product.InStock = inStock;
            product.Price = price;
            ProductList.Add(product);
        }
    }
    static public void CreateOrderList()
    {
        TimeSpan t_ShipDate = TimeSpan.FromDays((int)rand.NextInt64(0, 10));
        TimeSpan t_DeliveryDate = TimeSpan.FromDays((int)rand.NextInt64(10, 25));
        for (int i = 0; i < 20; i++)
        {
            Order orderi = new Order();
            // הגרלנו מספר שהוא מיקום במערך השמות
            int number = (int)rand.NextInt64(customerName.Length);
            int id = DataSource.Config.OrderId;
            orderi.CustomerName = customerName[number];
            orderi.Id = id;
            orderi.CustomerEmail = customerName[number] + "@gmail.com";
            orderi.CustomerAdress = customerName[number] + "Street";

            orderi.OrderDate = DateTime.Now;
            if (i % 10 < 8)  // 80% have ship date
                orderi.ShipDate = orderi.OrderDate + t_ShipDate;
            else
                orderi.ShipDate = DateTime.MinValue;
            if (i % 10 < 6)
            { // 60% from them have delivery date
                if (orderi.ShipDate == DateTime.MinValue)
                    orderi.ShipDate = orderi.OrderDate + t_ShipDate;
                orderi.DeliveryDate = orderi.ShipDate + t_DeliveryDate;
            }   
            else
                orderi.DeliveryDate = DateTime.MinValue;

            OrderArr.Add(orderi);
        }
    }



   


    static public void PushOrderItem()
    {
        for (int i = 0; i < 20; i++)
        {
            OrderItem orderItemi = new OrderItem();
            int orderId = OrderArr[i].Id;
            int min = (int)rand.NextInt64(1, 4);

            for (int j = 0; j < min; j++)
            {
                int id = Config.OrderItemId;
                int numberPoduct = (int)rand.NextInt64(0, ProductList.Count());
                int amount = (int)rand.NextInt64(0, ProductList[numberPoduct].InStock);
                orderItemi.Id = id;
                orderItemi.OrderID = orderId;
                orderItemi.ProductID = ProductList[numberPoduct].Id;
                orderItemi.Price = ProductList[numberPoduct].Price;
                orderItemi.Amount = amount;
                Product p = ProductList[numberPoduct];
                p.InStock = ProductList[numberPoduct].InStock - amount;
                OrderItems.Add(orderItemi);
            }

        }
    }




}
/*לעשות בדיקה שלא יוצא יותר מבן אדם אחד עם אותו איידי
/* string[] CustomerEmail = { "a@gmail.com","b@gmail.com","c@gmail.com","d@gmail.com","e@gmail.com"
           ,"f@gmail.com","k@gmail.com","l@gmail.com","m@gmail.com","n@gmail.com"
           ,"o@gmail.com","p@gmail.com","q@gmail.com","r@gmail.com","s@gmail.com","t@gmail.com"
           ,"u@gmail.com","v@gmail.com","x@gmail.com","w@gmail.com"};*/

