using DO;
using DalApi;
using System.Runtime.CompilerServices;
namespace Dal;



public class DalOrderItem : IOrderItem
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(OrderItem obj)
    {
        obj.Id = DataSource.Config.OrderItemId;
        DataSource.OrderItems.Add(obj);
        return obj.Id;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        if (id < 100000)
        {
            throw new IdNotValidException();
        }
        DataSource.OrderItems.Remove(DataSource.OrderItems.Find(o => o.Id == id));
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem Get(int id)
    {
        if (id < 100000)
        {
            throw new IdNotValidException();
        }
        OrderItem p = DataSource.OrderItems.Find(o => o.Id == id);
        if (p.Id == 0)/////////////
        {
            throw new EntityNotFoundException("no object whis this id");
        }
        return p;

    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem> ReadOrderId(int id)
    {
        int num = 0;
        for (int i = 0; i < (DataSource.OrderItems.Count()); i++)
        {
            if (id == DataSource.OrderItems[i].OrderID)
            {

                num++;
            }
        }
        OrderItem[] arr = new OrderItem[num];
        num = 0;
        for (int i = 0; i < (DataSource.OrderItems.Count()); i++)
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


    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem> Get(Func<OrderItem, bool>? foo = null)
    {
        return foo == null ? DataSource.OrderItems : DataSource.OrderItems.Where(foo).ToList();
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem? GetSingle(Func<OrderItem, bool>? foo)
    {
        return DataSource.OrderItems.Where(foo).ToList()[0];
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
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
            throw new EntityNotFoundException("no items in this order");
        DataSource.OrderItems[i] = obj;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    OrderItem ICrud<OrderItem>.GetSingle(Func<OrderItem, bool>? foo)
    {
        return DataSource.OrderItems.Where(foo).FirstOrDefault();
    }
}

