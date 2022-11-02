
using DalFacade;
using DalList;
namespace Dal;
public class Program
{
    enum Options { exit, product, order ,orderItem};
    enum InnerOptions { add, read, readId, update, delete };
    public void Main()
    {


        Console.Write("Enter your choice:");
        int choice = Console.Read();
        int id;
        try
        {
            switch ((Options)choice)
            {
                case Options.exit:
                    break;
                case Options.product:
                    Product obj;
                    switch ((InnerOptions)choice)
                    {
                        case InnerOptions.add:

                            obj = new Product();
                            Console.Write("Enter the product name:");
                            obj.ProductName = Console.ReadLine();
                            Console.Write("Enter the product price:");
                            obj.Price = Console.Read();
                            Console.Write("Enter the products number in stock:");
                            obj.InStock = Console.Read();
                            obj.ID = DalList.DataSource.Config.productId;
                            DalList.DalProduct.Create(obj);
                            break;
                        case InnerOptions.read:
                            Console.Write("Enter the id");
                            id = Console.Read();
                            DalList.DalProduct.Read(id);
                            break;
                        case InnerOptions.readId:
                            DalList.DalProduct.Read();
                            break;
                        case InnerOptions.update:
                            Console.Write("Enter id:");
                            id = Console.Read();
                            if (DalList.DalProduct.Read(id).ID == null) throw new Exception("no product found");
                            obj = new Product();
                            Console.Write("Enter the product name:");
                            obj.ProductName = Console.ReadLine();
                            Console.Write("Enter the product price:");
                            obj.Price = Console.Read();
                            Console.Write("Enter the products number in stock:");
                            obj.InStock = Console.Read();
                            obj.ID = id;
                            DalList.DalProduct.Update(obj);
                            break;
                        case InnerOptions.delete:
                            Console.Write("Enter id:");
                            id = Console.Read();
                            if (DalList.DalProduct.Read(id).ID == null) throw new Exception("no product found");
                            DalList.DalProduct.Delete(id);
                            break;
                    }
                    break;
                case Options.order:
                    Order ord;
                    switch ((InnerOptions)choice)
                    {
                        case InnerOptions.add:

                            ord = new Order();
                            Console.Write("Enter the your name:");
                            ord.CustomerName = Console.ReadLine();
                            Console.Write("Enter the your email:");
                            ord.CustomerEmail = Console.ReadLine();
                            Console.Write("Enter the your adress");
                            ord.CustomerAdress = Console.ReadLine();
                            Console.Write("Enter the your adress");
                            ord.OrderDate = DateTime.Now;
                            ord.ShipDate = DateTime.MinValue;
                            ord.DeliveryDate = DateTime.MinValue;
                            ord.ID = DalList.DataSource.Config.orderId;
                            DalList.DalOrder.Create(ord);
                            break;
                        case InnerOptions.read:
                            Console.Write("Enter the id");
                            id = Console.Read();
                            DalList.DalOrder.Read(id);
                            break;
                        case InnerOptions.readId:
                            DalList.DalOrder.Read();
                            break;
                        case InnerOptions.update:
                            Console.Write("Enter id:");
                            id = Console.Read();
                            if (DalList.DalOrder.Read(id).ID == null) throw new Exception("no product found");
                            ord = new Order();
                            Console.Write("Enter the your name:");
                            ord.CustomerName = Console.ReadLine();
                            Console.Write("Enter the your email:");
                            ord.CustomerEmail = Console.ReadLine();
                            Console.Write("Enter the your adress");
                            ord.CustomerAdress = Console.ReadLine();
                            Console.Write("Enter the your adress");
                            Order ordd = DalList.DalOrder.Read(id);
                            ord.OrderDate = ordd.OrderDate;
                            ord.ShipDate = ordd.ShipDate;
                            ord.DeliveryDate = ordd.DeliveryDate;
                            ord.ID = id;
                            DalList.DalOrder.Update(ord);
                            break;
                        case InnerOptions.delete:
                            Console.Write("Enter id:");
                            id = Console.Read();
                            if (DalList.DalOrder.Read(id).ID == null) throw new Exception("no product found");
                            DalList.DalOrder.Delete(id);
                            break;
                    }
                    break;
                case Options.orderItem:
                    OrderItem ordrI;
                    switch ((InnerOptions)choice)
                    {
                        case InnerOptions.add:
                            ordrI = new OrderItem();
                            ordrI.ID = DalList.DataSource.Config.orderItemId;
                            Console.Write("Enter the order id");
                            ordrI.OrderID = Console.Read();
                            Console.Write("Enter the product id");
                            ordrI.ProductID = Console.Read();
                            Console.Write("Enter the price");
                            ordrI.Price = Console.Read();
                            DalList.DalOrderItem.Create(ordrI);
                            break;
                        case InnerOptions.read:
                            Console.Write("Enter the id");
                            id = Console.Read();
                            DalList.DalOrderItem.Read(id);
                            break;
                        case InnerOptions.readId:
                            DalList.DalOrderItem.Read();
                            break;
                        case InnerOptions.update:
                            ordrI = new OrderItem();
                            Console.Write("Enter id:");
                            id = Console.Read();
                            if (DalList.DalOrderItem.Read(id).Length == null) throw new Exception("no product found");
                            Console.Write("Enter the order id");
                            ordrI.OrderID = Console.Read();
                            Console.Write("Enter the product id");
                            ordrI.ProductID = Console.Read();
                            Console.Write("Enter the price");
                            ordrI.Price = Console.Read();
                            DalList.DalOrderItem.Update(ordrI);
                            break;
                        case InnerOptions.delete:
                            Console.Write("Enter order id:");
                            id = Console.Read();
                            OrderItem[] arr = DalList.DalOrderItem.Read(id);
                            if (arr.Length != null) throw new Exception("no product found");
                            Console.Write("Enter product id:");
                            int prodId = Console.Read();
                            bool flag = false;
                            for (int i = 0; i < arr.Length; i++)
                            {
                                if (arr[i].ProductID == prodId)
                                {
                                    flag = true;
                                    DalList.DalOrderItem.Delete(arr[i].ID);
                                    break;
                                }
                            }
                            if (!flag) Console.Write("item does not exist in this order");
                            break;
                    }
                    break;
            }
        }