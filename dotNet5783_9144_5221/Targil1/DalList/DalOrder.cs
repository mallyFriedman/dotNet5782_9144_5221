using DalApi;
using DO;
namespace Dal;


public class DalOrder : IOrder
{
    public int Add(Order obj)
    {//////////
        obj.Id = DataSource.Config.OrderId;
        DataSource.OrderArr.Add(obj);
        return obj.Id;
    }

    public void Delete(int id)
    {
        if (id < 100000)
        {
            throw new IdNotValidException();
        }
        DataSource.OrderArr.Remove(DataSource.OrderArr.Find(o => o.Id == id));

    }
    public Order Get(int id)
    {
        if (id < 100000)
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

    public IEnumerable<Order>? Get(Func<Order, bool>? foo = null)
    {
        return foo == null ? DataSource.OrderArr : DataSource.OrderArr.Where(foo).ToList();
    }


    public Order? GetSingle(Func<Order, bool>? foo)
    {
        return  DataSource.OrderArr.Where(foo).ToList()[0] ;
    }

    public void Update(Order obj)
    {
        int i = DataSource.OrderArr.FindIndex(o => o.Id == obj.Id);
        if (i == -1)
            throw new EntryPointNotFoundException("no items in this order...");
        DataSource.OrderArr[i] = obj;
    }

    Order ICrud<Order>.GetSingle(Func<Order, bool>? foo)
    {
        return DataSource.OrderArr.Where(foo).FirstOrDefault();
    }
}


