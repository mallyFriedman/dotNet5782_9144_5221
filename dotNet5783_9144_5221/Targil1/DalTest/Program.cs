
using DalFacade;
using DalList;
namespace Dal;
public class Program
{
    DataSource ds = new DataSource();
    enum Options { exit, product, order, orderItem };
    enum InnerOptions { add,  readId, read, update, delete };
    //private Product obj=new Product();
    //Order ord=new Order();
    //OrderItem ordrI=new OrderItem();
    public static void Main()
    {
        Console.Write("enter 0 for exit, " +
            "1 for product, " +
            "2 for order, " +
            "3 for orderItem. ");
        Console.Write("Enter your choice:");
        int choice = Convert.ToInt32(Console.ReadLine());
        while (choice != 0)
        {
            try
            {
                switch ((Options)choice)
                {
                    case Options.product:
                        switchProduct();
                        break;
                    case Options.order:
                        switchOrder();
                        break;
                    case Options.orderItem:
                        switchOrderItem();
                        break;
                    default:
                        Console.Write("Worng choice! ");
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.Write("Enter your choice: ");
            choice = Convert.ToInt32(Console.ReadLine());
        }
    }
    public static void switchProduct()
    {
        options();
        int choice = Convert.ToInt32(Console.ReadLine());
        int id;
        Product obj;
        switch ((InnerOptions)choice)
        {
            case InnerOptions.add:
                obj = new Product();
                Console.Write("Enter the product name: ");
                obj.ProductName = Console.ReadLine();
                Console.Write("Enter the product price: ");
                obj.Price = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter the products number in stock: ");
                obj.InStock = Convert.ToInt32(Console.ReadLine());
                obj.ID = DataSource.Config.productId;
                DalProduct.Create(obj);
                break;
            case InnerOptions.readId:
                Console.Write("Enter the id: ");
                id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(DalProduct.Read(id));
                break;
            case InnerOptions.read:
                Product[] orders = DalProduct.Read();
                foreach (Product item in orders) if (item.ID > 0)
                    {
                        Console.WriteLine(item);
                    }
                break;
            case InnerOptions.update:
                Console.Write("Enter id: ");
                id = Convert.ToInt32(Console.ReadLine());
                Product a = DalProduct.Read(id);
                if (a.ProductName == null) throw new Exception("no product found");
                Console.WriteLine(a);
                obj = new Product();
                Console.Write("Enter the product name: ");
                string mishtane = Console.ReadLine();
                if (string.IsNullOrEmpty(mishtane))
                {
                    obj.ProductName = a.ProductName;
                }
                else
                {
                    obj.ProductName = mishtane;
                }
                Console.WriteLine(a);
                
                Console.Write("Enter the product price: ");
                mishtane = Console.ReadLine();
                if (string.IsNullOrEmpty(mishtane))
                {
                    obj.Price = a.Price;
                }
                else
                {
                    obj.Price =Convert.ToInt32(mishtane);
                }
                Console.Write("Enter the products number in stock: ");
                mishtane = Console.ReadLine();
                if (string.IsNullOrEmpty(mishtane))
                {
                    obj.InStock = a.InStock;
                }
                else
                {
                    obj.InStock =   Convert.ToInt32(mishtane);
                }
                obj.ID = id;
                DalProduct.Update(obj);
                break;
            case InnerOptions.delete:
                Console.Write("Enter id: ");
                id = Convert.ToInt32(Console.ReadLine());
                if (DalProduct.Read(id).ProductName == null) throw new Exception("no product found");
                DalProduct.Delete(id);
                break;
        }
    }
    public static void switchOrder()
    {
        options();
        int choice = Convert.ToInt32(Console.ReadLine());
        int id;
        Order ord;
        switch ((InnerOptions)choice)
        {
            case InnerOptions.add:
                ord = new Order();
                Console.Write("Enter the name: ");
                ord.CustomerName = Console.ReadLine();
                Console.Write("Enter the email: ");
                ord.CustomerEmail = Console.ReadLine();
                Console.Write("Enter the adress: ");
                ord.CustomerAdress = Console.ReadLine();
                ord.OrderDate = DateTime.Now;
                ord.ShipDate = DateTime.MinValue;
                ord.DeliveryDate = DateTime.MinValue;
                ord.ID = DataSource.Config.orderId;
                DalOrder.Create(ord);
                break;
            case InnerOptions.readId:
                Console.Write("Enter the id: ");
                id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(DalOrder.Read(id));
                break;
            case InnerOptions.read:
                Order []orders=DalOrder.Read();
                foreach (Order item in orders) if ( item.ID>0)
                {
                    Console.WriteLine(item);
                }
                break;
            case InnerOptions.update:
                Console.Write("Enter id: ");
                id = Convert.ToInt32(Console.ReadLine());
                Order a = DalOrder.Read(id);
                if (a.CustomerEmail == null) throw new Exception("no product found");
                Console.WriteLine(a);
                ord = new Order();
                Console.Write("Enter the your name: ");
                string mishtane = Console.ReadLine();
                if (string.IsNullOrEmpty(mishtane))
                {
                    ord.CustomerName = a.CustomerName;
                }
                else
                {
                    ord.CustomerName = mishtane;
                }
                Console.Write("Enter the your email: ");
                mishtane = Console.ReadLine();
                if (string.IsNullOrEmpty(mishtane))
                {
                    ord.CustomerEmail = a.CustomerEmail;
                }
                else
                {
                    ord.CustomerEmail = mishtane;
                }
                Console.Write("Enter the your adress: ");
                mishtane = Console.ReadLine();
                if (string.IsNullOrEmpty(mishtane))
                {
                    ord.CustomerAdress = a.CustomerAdress ;
                }
                else
                {
                    ord.CustomerAdress = mishtane;
                }
                ord.OrderDate = a.OrderDate;
                ord.ShipDate = a.ShipDate;
                ord.DeliveryDate = a.DeliveryDate;
                ord.ID = id;
                DalOrder.Update(ord);
                break;
            case InnerOptions.delete:
                Console.Write("Enter id: ");
                id = Convert.ToInt32(Console.ReadLine());
               // if (DalOrder.Read(id).CustomerEmail == null) throw new Exception("no product found");
                DalOrder.Delete(id);
                break;
        }
    }

    public static void switchOrderItem()
    {
        options();
        int choice = Convert.ToInt32(Console.ReadLine());
        int id;
        OrderItem ordrI;
        switch ((InnerOptions)choice)
        {
            case InnerOptions.add:
                ordrI = new OrderItem();
                ordrI.ID = DataSource.Config.orderItemId;
                Console.Write("Enter the order id: ");
                ordrI.OrderID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter the product id: ");
                ordrI.ProductID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter the price: ");
                ordrI.Price = Convert.ToInt32(Console.ReadLine());
                DalOrderItem.Create(ordrI);
                break;
            case InnerOptions.readId:
                Console.Write("Enter the id: ");

                id = Convert.ToInt32(Console.ReadLine());
                OrderItem[] items = DalOrderItem.Read(id);
                foreach (OrderItem item in items) if (item.ID > 0)
                    {
                        Console.WriteLine(item);
                    }
                break;
            case InnerOptions.read:
                OrderItem[] orders = DalOrderItem.Read();
                foreach (OrderItem item in orders) if (item.ID > 0)
                    {
                        Console.WriteLine(item);
                    }
                break;
            case InnerOptions.update:
                ordrI = new OrderItem();
                Console.Write("Enter id: ");
                id = Convert.ToInt32(Console.ReadLine());
                OrderItem a = new OrderItem();
                a=DalOrderItem.find(id);
                if (a.ID == 0) throw new Exception("no product found");
                    Console.Write(a);
                Console.Write("Enter order id: ");
                string mishtane= Console.ReadLine();
                if (string.IsNullOrEmpty(mishtane))
                {
                    ordrI.OrderID = a.OrderID;
                }
                else
                {
                    ordrI.OrderID = Convert.ToInt32(mishtane) ;
                }
                Console.Write("Enter the product id: ");
                mishtane = Console.ReadLine();
                if (string.IsNullOrEmpty(mishtane))
                {
                    ordrI.ProductID = a.ProductID;
                }
                else
                {
                    ordrI.ProductID = Convert.ToInt32(mishtane);
                }
                Console.Write("Enter the price: ");
                mishtane = Console.ReadLine();
                if (string.IsNullOrEmpty(mishtane))
                {
                    ordrI.Price = a.Price;
                }
                else
                {
                    ordrI.Price = Convert.ToInt32(mishtane);
                }
                ordrI.ID = a.ID;
                DalOrderItem.Update(ordrI);
                break;
            case InnerOptions.delete:
                Console.Write("Enter order id:");
                id = Convert.ToInt32(Console.ReadLine());
                OrderItem ab = new OrderItem();
                ab = DalOrderItem.find(id);
                if (ab.ID==0) throw new Exception("no product found");
                DalOrderItem.Delete(ab.ID);
                break;
        }
    }

    public static void options()
    {
        Console.Write("enter 0 for add, " +
                          "1 for readId, " +
                          "2 for read, " +
                          "3 for update, " +
                          "4 for delete : ");
    }
}