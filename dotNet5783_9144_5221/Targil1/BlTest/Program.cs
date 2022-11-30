using BlImplementation;
using BlApi;
//namespace BlTest;


public static class Program1
{
    static IBl iBl = new Bl();
    static BO.Cart cart=new BO.Cart();
    enum Options { exit, product, order, cart };
    enum ProductOptions { EXIT, Add, GetAllForCustomer, GetAllForManager, GetCustomer, GetManager, update, Delete };
    enum OrderOptions { EXIT, GetAll, Get, UpdateSupply, UpdateShipping };
    enum CartOptions { EXIT, Add, Update, Confirm };

    public static void Main()
    {
        Console.Write("enter 0 for exit, " +
            "1 for product, " +
            "2 for order, " +
            "3 for cart. ");
        Console.Write("Enter your choice: ");
        int choice = Convert.ToInt32(Console.ReadLine());
        while (choice != 0)
        {
            try
            {
                switch ((Options)choice)
                {
                    case Options.product:
                        SwitchProduct();
                        break;
                    case Options.order:
                        SwitchOrder();
                        break;
                    case Options.cart:
                        SwitchCart();
                        break;
                    default:
                        Console.Write("Worng choice! ");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.Write("Enter your choice: ");
            choice = Convert.ToInt32(Console.ReadLine());
        }
    }
    public static void SwitchProduct()
    {
        productOptions();
        int choice = Convert.ToInt32(Console.ReadLine());
        int id;
        BO.Product obj;
        while (choice != 0)
        {
            try
            {
                switch ((ProductOptions)choice)
                {
                    case ProductOptions.Add:
                        obj = new BO.Product();
                        Console.Write("Enter the product name: ");
                        obj.ProductName = Console.ReadLine();
                        Console.Write("Enter the product price: ");
                        obj.Price = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter the products number in stock: ");
                        obj.InStock = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter the products category:" +
                            "1 for  Necklaces" +
                            "2 for Bracelets" +
                            "3 for Earrings" +
                            "4 for Rings: ");
                        obj.Category = (BO.Category)Convert.ToInt32(Console.ReadLine());
                        obj.Id = 0;
                        iBl.Product.Add(obj);
                        break;
                    case ProductOptions.GetAllForCustomer:
                        IEnumerable<BO.ProductForList> products = iBl.Product.GetAllForCustomer();
                        foreach (BO.ProductForList item in products)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case ProductOptions.GetAllForManager:
                        IEnumerable<BO.ProductItem> items = iBl.Product.GetAllForManager();
                        foreach (BO.ProductItem item in items)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case ProductOptions.GetCustomer:
                        Console.Write("Enter the id: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(iBl.Product.GetCustomer(id));
                        break;
                    case ProductOptions.GetManager:
                        Console.Write("Enter the id: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(iBl.Product.GetManager(id));
                        break;
                    case ProductOptions.update:
                        Console.Write("Enter id: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        BO.Product a = iBl.Product.GetManager(id);
                        if (a.ProductName == null) throw new Exception("no product found");
                        Console.WriteLine(a);
                        obj = new BO.Product();
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
                        Console.Write("Enter the product price: ");
                        mishtane = Console.ReadLine();
                        if (string.IsNullOrEmpty(mishtane))
                        {
                            obj.Price = a.Price;
                        }
                        else
                        {
                            obj.Price = Convert.ToInt32(mishtane);
                        }
                        Console.Write("Enter the products number in stock: ");
                        mishtane = Console.ReadLine();
                        if (string.IsNullOrEmpty(mishtane))
                        {
                            obj.InStock = a.InStock;
                        }
                        else
                        {
                            obj.InStock = Convert.ToInt32(mishtane);
                        }
                        obj.Id = id;
                        iBl.Product.Update(obj);
                        break;
                    case ProductOptions.Delete:
                        Console.Write("Enter id: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        if (iBl.Product.GetManager(id).ProductName == null) throw new Exception("no product found");
                        iBl.Product.Delete(id);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            productOptions();
            Console.Write("Enter your product choice: ");
            choice = Convert.ToInt32(Console.ReadLine());
           
        }
    }
    public static void SwitchOrder()
    {
        orderOptions();
        int choice = Convert.ToInt32(Console.ReadLine());
        int id;
        BO.Order ord;
        while (choice != 0)
        {
            try
            {
                switch ((OrderOptions)choice)
                {
                    case OrderOptions.GetAll:
                        IEnumerable<BO.OrderForList> orders = iBl.Order.GetAll();
                        foreach (BO.OrderForList item in orders)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case OrderOptions.Get:
                        Console.Write("Enter the id: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(iBl.Order.Get(id));
                        break;

                    case OrderOptions.UpdateSupply:
                        Console.Write("Enter id: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        iBl.Order.UpdateSupply(id);
                        break;
                    case OrderOptions.UpdateShipping:
                        Console.Write("Enter id: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        iBl.Order.UpdateShipping(id);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            orderOptions();
            Console.Write("Enter your choice: ");
            choice = Convert.ToInt32(Console.ReadLine());
        }
    }

    public static void SwitchCart()
    {
        cartOptions();
        int choice = Convert.ToInt32(Console.ReadLine());
        int id;
        while (choice != 0)
        {
            try
            {
                switch ((CartOptions)choice)
                {
                    case CartOptions.Add:
                        Console.Write("Enter the product id: ");
                        int idProduct = Convert.ToInt32(Console.ReadLine());
                        iBl.Cart.Add(cart, idProduct);

                        Console.WriteLine(cart);
                        break;
                    case CartOptions.Update:
                        Console.Write("Enter the product id to update: ");
                        int idProd = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter the new amount for update: ");
                        int amount = Convert.ToInt32(Console.ReadLine());
                        iBl.Cart.Update(cart, idProd, amount);

                        Console.WriteLine(cart);
                        break;
                    case CartOptions.Confirm:
                        Console.Write("Enter the CustomerName: ");
                        string CustomerName = Console.ReadLine();
                        Console.Write("Enter the CustomerEmail: ");
                        string CustomerEmail = Console.ReadLine();
                        Console.Write("Enter the CustomerAdress: ");
                        string CustomerAdress = Console.ReadLine();

                        iBl.Cart.Confirm(cart, CustomerName, CustomerEmail, CustomerAdress);
                        Console.WriteLine(cart);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            cartOptions();
            Console.Write("Enter your choice: ");
            choice = Convert.ToInt32(Console.ReadLine());
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
    public static void productOptions()
    {
        Console.Write("enter 0 for EXIT " +
                          "1 for Add , "+
                          "2 for GetAllForCustomer, " +
                          "3 for GetAllForManager, " +
                          "4 for GetCustomer, " +
                          "5 for GetManager, " +
                          "6 for update, " +
                          "7 for Delete: " 
                          );
    }

    public static void orderOptions()
    {
        Console.Write("enter 0 for EXIT " +
                          "1 for GetAll, " +
                          "2 for Get, " +
                          "3 for UpdateSupply, " +
                          "4 for UpdateShipping : "
                          );
    }
    public static void cartOptions()
    {
        Console.Write("enter 0 for EXIT " +
                          "1 for Add, " +
                          "2 for Update, " +
                          "3 for Confirm: "
                          );
    }

   // public static void AddProduct()
   // {
   // }
   // public static void AddProduct()
   // {
   // }
   // public static void AddProduct()
   // {
   // }
   // public static void AddProduct()
   // {
   // }

}