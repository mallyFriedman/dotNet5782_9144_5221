using DO;
namespace Dal;




public static class DalOrderItem
{
    public static int Create(OrderItem obj)
    {
        DataSource.OrderItems.Add(obj);
        DataSource.Config._indexArrOrderItem++;
        return obj.Id;
    }

    public static void Delete(int id)
    {
        DataSource.OrderItems.Remove(DataSource.OrderItems.Find(o => o.Id == id));
    }
    public static OrderItem ReadOrderItem(int id)
    {
        OrderItem p = DataSource.OrderItems.Find(o => o.Id == id);
       //if (p.Id == 0)/////////////
       //{
       //    throw new Exception("baddddddd");
       //}
        return p;

    }
    public static OrderItem[] ReadOrderId(int id)
    {
        int num = 0;
        for (int i = 0; i < (DataSource.Config._indexArrOrderItem); i++)
        {
            if (id == DataSource.OrderItems[i].OrderID)
            {
                num++;
            }
        }
        OrderItem[] arr = new OrderItem[num];
        num = 0;
        for (int i = 0; i < (DataSource.Config._indexArrOrderItem); i++)
        {
            if (id == DataSource.OrderItems[i].OrderID)
            {
                arr[num] = DataSource.OrderItems[i];
                num++;
            }
        }

        if (num!=0) return arr;
        throw new Exception("no items in this order...");

    }

    public static List<OrderItem> Read()
    {
        return DataSource.OrderItems;
    }

    public static void Update(OrderItem obj)
    {
        int w = DataSource.OrderArr.FindIndex(o => o.Id == obj.OrderID);
        int p = DataSource.ProductList.FindIndex(o => o.Id == obj.ProductID);
        if (w == -1 || p == -1)
        {
            throw new Exception("wrong detailes");
        }
        int i = DataSource.OrderItems.FindIndex(o => o.Id == obj.Id);
        if (i == -1)
            throw new Exception("no items in this order...");
        DataSource.OrderItems[i] = obj;
    }
}

