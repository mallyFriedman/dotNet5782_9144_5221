using BlImplementation;
using BlApi;
//namespace BlTest;


public static class Program1
{
    static IBl iBl = new Bl();
    static BO.Cart cart = new BO.Cart();
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
                throw new Exception(ex.Message); ;
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
                        ProductAdd();
                        break;
                    case ProductOptions.GetAllForCustomer:
                        ProductGetAllForCustomer();
                        break;
                    case ProductOptions.GetAllForManager:
                        ProductGetAllForManager();
                        break;
                    case ProductOptions.GetCustomer:
                        ProductGetCustomer();
                        break;
                    case ProductOptions.GetManager:
                        ProductGetManager();
                        break;
                    case ProductOptions.update:
                        ProductUpdate();
                        break;
                    case ProductOptions.Delete:
                        ProductDelete();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                        OrderGetAll();
                        break;
                    case OrderOptions.Get:
                        OrderGet();
                        break;
                    case OrderOptions.UpdateSupply:
                        OrderUpdateSupply();
                        break;
                    case OrderOptions.UpdateShipping:
                        OrderUpdateShipping();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                        CartAdd();
                        break;
                    case CartOptions.Update:
                        CartUpdate();
                        break;
                    case CartOptions.Confirm:
                        CartConfirm();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); ;
            }
            cartOptions();
            Console.Write("Enter your choice: ");
            choice = Convert.ToInt32(Console.ReadLine());
        }
    }


    //=======================
    //       FUNCTIONS
    //=======================

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
                          "1 for Add , " +
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


    //====================
    // product functions
    //====================

    public static void ProductAdd()
    {
        try
        {
            BO.Product obj;
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
        }
        catch (BlObjectNotValidException ex)
        {
            throw new Exception(ex.Message); ;
        }
    }
    public static void ProductGetAllForCustomer()
    {
        IEnumerable<BO.ProductForList> products = iBl.Product.GetAllForCustomer();
        foreach (BO.ProductForList item in products)
        {
            Console.WriteLine(item);
        }
    }
    public static void ProductGetAllForManager()
    {
        IEnumerable<BO.ProductItem> items = iBl.Product.GetAllForManager();
        foreach (BO.ProductItem item in items)
        {
            Console.WriteLine(item);
        }
    }
    public static void ProductGetCustomer()
    {
        try
        {
            int id;
            Console.Write("Enter the id: ");
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(iBl.Product.GetCustomer(id));
        }
        catch (BlIdNotValidException ex)
        {
            throw new Exception(ex.Message); ;
        }
        catch (BlObjectNotFoundException ex)
        {
            throw new Exception(ex.Message); ;
        }
    }
    public static void ProductGetManager()
    {
        try
        {
            int id;
            Console.Write("Enter the id: ");
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(iBl.Product.GetManager(id));
        }
        catch (BlIdNotValidException ex)
        {
            throw new Exception(ex.Message); ;
        }
        catch (BlObjectNotFoundException ex)
        {
            throw new Exception(ex.Message); ;
        }
    }
    public static void ProductUpdate()
    {
        try
        {
            int id;
            BO.Product obj;
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
        }
        catch (BlObjectNotValidException ex)
        {
            throw new Exception(ex.Message); ;
        }
    }

    public static void ProductDelete()
    {
        try
        {
            int id;
            Console.Write("Enter id: ");
            id = Convert.ToInt32(Console.ReadLine());
            if (iBl.Product.GetManager(id).ProductName == null) throw new Exception("no product found");
            iBl.Product.Delete(id);
        }
        catch (BlIdNotValidException ex)
        {
            throw new Exception(ex.Message); ;
        }
        catch (BlObjectNotFoundException ex)
        {
            throw new Exception(ex.Message); ;
        }
    }



    //====================
    // order functions
    //====================
    public static void OrderGetAll()
    {
        IEnumerable<BO.OrderForList> orders = iBl.Order.GetAll();
        foreach (BO.OrderForList item in orders)
        {
            Console.WriteLine(item);
        }
    }

    public static void OrderGet()
    {
        try
        {
            int id;
            Console.Write("Enter the id: ");
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(iBl.Order.Get(id));
        }
        catch (BlIdNotValidException ex)
        {
            throw new Exception(ex.Message); ;
        }
        catch (BlObjectNotFoundException ex)
        {
            throw new Exception(ex.Message); ;
        }

    }
    public static void OrderUpdateSupply()
    {
        try
        {
            int id;
            Console.Write("Enter id: ");
            id = Convert.ToInt32(Console.ReadLine());
            iBl.Order.UpdateSupply(id);
        }
        catch (BlCannotChangeTheStatusException ex)
        {
            throw new Exception(ex.Message); ;
        }
        catch (BlObjectNotFoundException ex)
        {
            throw new Exception(ex.Message); ;
        }
    }

    public static void OrderUpdateShipping()
    {
        try
        {
            int id;
            Console.Write("Enter id: ");
            id = Convert.ToInt32(Console.ReadLine());
            iBl.Order.UpdateShipping(id);
        }
        catch (BlCannotChangeTheStatusException ex)
        {
            throw new Exception(ex.Message); ;
        }
        catch (BlObjectNotFoundException ex)
        {
            throw new Exception(ex.Message); ;
        }
    }



    //====================
    // order functions
    //====================

    public static void CartAdd()
    {
        try
        {
            Console.Write("Enter the product id: ");
            int idProduct = Convert.ToInt32(Console.ReadLine());
            iBl.Cart.Add(cart, idProduct);
            Console.WriteLine(cart);
        }
        catch (BlObjectNotFoundException ex)
        {
            throw new Exception(ex.Message); ;
        }
        catch (BlOutOfStockException ex)
        {
            throw new Exception(ex.Message); ;
        }

    }

    public static void CartUpdate()
    {
        try
        {
            Console.Write("Enter the product id to update: ");
            int idProd = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the new amount for update: ");
            int amount = Convert.ToInt32(Console.ReadLine());
            iBl.Cart.Update(cart, idProd, amount);
            Console.WriteLine(cart);
        }
        catch (BlObjectNotFoundException ex)
        {
            throw new Exception(ex.Message); ;
        }
        catch (BlOutOfStockException ex)
        {
            throw new Exception(ex.Message); ;
        }
        catch (BlCartIsEmptyException ex)
        {
            throw new Exception(ex.Message); ;
        }
    }

    public static void CartConfirm()
    {
        try
        {
            Console.Write("Enter the CustomerName: ");
            string CustomerName = Console.ReadLine();
            Console.Write("Enter the CustomerEmail: ");
            string CustomerEmail = Console.ReadLine();
            Console.Write("Enter the CustomerAdress: ");
            string CustomerAdress = Console.ReadLine();
            iBl.Cart.Confirm(cart, CustomerName, CustomerEmail, CustomerAdress);
            Console.WriteLine(cart);
        }
        catch (BlObjectNotFoundException ex)
        {
            throw new Exception(ex.Message); ;
        }
        catch (BlOutOfStockException ex)
        {
            throw new Exception(ex.Message); ;
        }
        catch (BlDetailsNotValidException ex)
        {
            throw new Exception(ex.Message); ;
        }
    }

}