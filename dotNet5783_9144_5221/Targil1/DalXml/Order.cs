using DalApi;
using DO;

namespace Dal;

internal class Order : IOrder
{
    public int Add(DO.Order item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Order Get(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order>? Get(Func<DO.Order, bool>? foo = null)
    {
        throw new NotImplementedException();
    }

    public DO.Order GetSingle(Func<DO.Order, bool>? foo)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Order item)
    {
        throw new NotImplementedException();
    }
}