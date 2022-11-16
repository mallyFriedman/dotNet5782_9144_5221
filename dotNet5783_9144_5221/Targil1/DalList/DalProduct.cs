using DO;
namespace Dal;

public static class DalProduct
{
    public static int Create(Product obj)
    {
        DataSource.ProductList.Add(obj);
        return obj.Id;
    }

    public static void Delete(int id)
    {
        DataSource.ProductList.Remove(DataSource.ProductList.Find(o => o.Id == id));
    }
    public static Product Read(int id)
    {
        Product p = DataSource.ProductList.Find(o => o.Id == id);
        //if (p.Id == 0)/////////////
        //{
        //    throw new Exception("baddddddd");
        //}
        return p;
    }

    
    public static List<Product> Read()
    {
        return DataSource.ProductList;
    }

    public static void Update(Product obj)
    {

        int i = DataSource.ProductList.FindIndex(o => o.Id == obj.Id);
        if (i == -1)
            throw new Exception("no items in this order...");
        DataSource.ProductList[i] = obj;
    }
}
