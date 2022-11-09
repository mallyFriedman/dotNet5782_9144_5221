using DO;
namespace Dal;




public static class DalOrderItem
{
    public static int Create(OrderItem obj)
    {
        DataSource.OrderItems[DataSource.Config._indexArrOrderItem++] = obj;
        return obj.Id;
    }

    public static void Delete(int id)
    {
        for (int i = 0; i < DataSource.Config._indexArrOrderItem; i++)
        {
            if (DataSource.OrderItems[i].Id == id)
            {
                int index = DataSource.Config._indexArrOrderItem;
                DataSource.OrderItems[i] = DataSource.OrderItems[index];
                DataSource.OrderItems[index].Id= 0;
                DataSource.Config._indexArrOrderItem = (DataSource.Config._indexArrOrderItem - 1);
                break;
            }
        }
        return;
    }
    public static OrderItem ReadOrderItem(int id)
    {
        OrderItem a = new OrderItem();
        for (int i = 0; i < DataSource.Config._indexArrOrderItem; i++)
        {
            if (DataSource.OrderItems[i].Id == id)
            {
                a= DataSource.OrderItems[i];
                break;
            }
        }
        return a;
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

    public static OrderItem[] Read()
    {
        OrderItem[] arr = new OrderItem[DataSource.Config._indexArrOrderItem];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = DataSource.OrderItems[i];
        }
        return arr;
    }

    public static void Update(OrderItem obj)
    {
        for (int i = 0; i < (DataSource.Config._indexArrOrderItem); i++)
        {
            if (obj.Id == DataSource.OrderItems[i].Id)
            {
                DataSource.OrderItems[i] = obj;
            }
        }
    }
}

