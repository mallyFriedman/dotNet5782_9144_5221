using DalApi;
using DO;
using System.Runtime.CompilerServices;

namespace Dal;

public class DalProduct : IProduct
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Product obj)
    {
        obj.Id = DataSource.Config.ProductId;
        DataSource.ProductList.Add(obj);
        return obj.Id;
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        DataSource.ProductList.Remove(DataSource.ProductList.Find(o => o.Id == id));
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product Get(int id)
    {
        Product p = DataSource.ProductList.Find(o => o.Id == id);
        if (p.Id == 0)
        {
            throw new EntityNotFoundException("no product whis this id");
        }
        return p;
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Product> Get(Func<Product, bool>? foo = null)
    {
        return foo == null ? DataSource.ProductList : DataSource.ProductList.Where(foo).ToList();
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product? GetSingle(Func<Product, bool> foo)
    {
        return DataSource.ProductList.Where(foo).ToList()[0];
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Product obj)
    {

        int i = DataSource.ProductList.FindIndex(o => o.Id == obj.Id);
        if (i == -1)
            throw new EntityNotFoundException("no items in this order...");
        DataSource.ProductList[i] = obj;
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    Product ICrud<Product>.GetSingle(Func<Product, bool>? foo)
    {
        throw new NotImplementedException();
    }
}
