using DO;
using DalApi;
namespace Dal;



public class DalOrderItem : IOrderItem
{
    public int Add(OrderItem obj)
    {
        obj.Id = DataSource.Config.OrderItemId;
        DataSource.OrderItems.Add(obj);
        return obj.Id;
    }
    public void Delete(int id)
    {
        DataSource.OrderItems.Remove(DataSource.OrderItems.Find(o => o.Id == id));
    }
  
    public OrderItem Get(int id)
    {
        OrderItem p = DataSource.OrderItems.Find(o => o.Id == id);
        if (p.Id == 0)/////////////
        {
            throw new Exception("baddddddd");
        }
        return p;

    }
    public IEnumerable<OrderItem> ReadOrderId(int id)
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

        if (num != 0) return arr;
        throw new EntityNotFoundException("no items in this order...");

    }

    public IEnumerable<OrderItem> Get()
    {
        return DataSource.OrderItems;
    }

    public void Update(OrderItem obj)
    {
        int w = DataSource.OrderArr.FindIndex(o => o.Id == obj.OrderID);
        int p = DataSource.ProductList.FindIndex(o => o.Id == obj.ProductID);
        if (w == -1 || p == -1)
        {
            throw new EntityNotFoundException("wrong detailes");
        }
        int i = DataSource.OrderItems.FindIndex(o => o.Id == obj.Id);
        if (i == -1)
            throw new EntityNotFoundException("no items in this order...");
        DataSource.OrderItems[i] = obj;
    }
}

