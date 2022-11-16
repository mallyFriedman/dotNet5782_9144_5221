using DalApi;
using DO;
namespace Dal;


public class DalOrder : IOrder
{
    public int Add(Order obj)
    {
        DataSource.OrderArr.Add(obj);
        return obj.Id;
    }

    public void Delete(int id)
    {
        DataSource.OrderArr.Remove(DataSource.OrderArr.Find(o => o.Id == id));
    }
    public Order Get(int id)
    {
        Order p = DataSource.OrderArr.Find(o => o.Id == id);
        if (p.Id == 0)/////////////
        {
            throw new EntryPointNotFoundException("baddddddd");
        }
        return p;
    }

    public IEnumerable<Order> Get()
    {
        return DataSource.OrderArr;
    }

    public void Update(Order obj)
    {
        int i = DataSource.OrderArr.FindIndex(o => o.Id == obj.Id);
        if (i == -1)
            throw new EntryPointNotFoundException("no items in this order...");
        DataSource.OrderArr[i] = obj;
    }
}


