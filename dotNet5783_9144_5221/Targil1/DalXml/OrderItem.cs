using DalApi;
using DO;

namespace Dal;

internal class OrderItem : IOrderItem
{
    public int Add(DO.OrderItem item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem Get(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.OrderItem>? Get(Func<DO.OrderItem, bool>? foo = null)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem GetSingle(Func<DO.OrderItem, bool>? foo)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.OrderItem> ReadOrderId(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.OrderItem item)
    {
        throw new NotImplementedException();
    }
}
