using DalApi;
using DO;
namespace Dal;

public class DalProduct : IProduct
{
    public int Add(Product obj)
    {
        obj.Id = DataSource.Config.ProductId;
        DataSource.ProductList.Add(obj);
        return obj.Id;
    }

    public void Delete(int id)
    {
        DataSource.ProductList.Remove(DataSource.ProductList.Find(o => o.Id == id));
    }
    public Product Get(int id)
    {
        Product p = DataSource.ProductList.Find(o => o.Id == id);
        if (p.Id == 0)/////////////
        {
            throw new EntityNotFoundException("no product whis this id");
        }
        return p;
    }


    public IEnumerable<Product> Get(Func<Product, bool>? foo = null)
    {
        return foo == null ? DataSource.ProductList : DataSource.ProductList.Where(foo).ToList();
    }

    public Product? GetSingle(Func<Product, bool> foo)
    {
        return DataSource.ProductList.Where(foo).ToList()[0];
    }
    public void Update(Product obj)
    {

        int i = DataSource.ProductList.FindIndex(o => o.Id == obj.Id);
        if (i == -1)
            throw new EntityNotFoundException("no items in this order...");
        DataSource.ProductList[i] = obj;
    }

    Product ICrud<Product>.GetSingle(Func<Product, bool>? foo)
    {
        throw new NotImplementedException();
    }
}
