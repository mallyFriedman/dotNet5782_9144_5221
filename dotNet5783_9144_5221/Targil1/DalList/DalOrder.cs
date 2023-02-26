using DalApi;
using DO;
using System.Runtime.CompilerServices;
namespace Dal;


public class DalOrder : IOrder
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Order obj)
    {//////////
        obj.Id = DataSource.Config.OrderId;
        DataSource.OrderArr.Add(obj);
        return obj.Id;
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        if (id < 500000)
        {
            throw new IdNotValidException();
        }
        DataSource.OrderArr.Remove(DataSource.OrderArr.Find(o => o.Id == id));

    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order Get(int id)
    {
        if (id < 500000)
        {
            throw new IdNotValidException();
        }
        Order p = DataSource.OrderArr.Find(o => o.Id == id);
        if (p.Id == 0)
        {
            throw new EntityNotFoundException("order does not exist");
        }
        return p;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order>? Get(Func<Order, bool>? foo = null)
    {
        return foo == null ? DataSource.OrderArr : DataSource.OrderArr.Where(foo).ToList();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order? GetSingle(Func<Order, bool>? foo)
    {
        return  DataSource.OrderArr.Where(foo).ToList()[0] ;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order obj)
    {
        int i = DataSource.OrderArr.FindIndex(o => o.Id == obj.Id);
        if (i == -1)
            throw new EntryPointNotFoundException("no items in this order...");
        DataSource.OrderArr[i] = obj;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    Order ICrud<Order>.GetSingle(Func<Order, bool>? foo)
    {
        return DataSource.OrderArr.Where(foo).FirstOrDefault();
    }
}


