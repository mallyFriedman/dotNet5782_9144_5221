using DO;
namespace Dal;

public static class DalProduct
{
    public static int Create(Product obj)
    {
        DataSource.ProductList[DataSource.Config._indexArrProduct++] = obj;
        return obj.Id;
    }

    public static void Delete(int id)
    {
        for (int i = 0; i < DataSource.Config._indexArrProduct; i++)
        {
            if (DataSource.ProductList[i].Id== id)
            {
                int index = DataSource.Config._indexArrProduct;
                DataSource.ProductList[i] = DataSource.ProductList[index];
                DataSource.ProductList[index].Id = 0;
                DataSource.Config._indexArrProduct = (DataSource.Config._indexArrProduct - 1);
                break;
            }
        }
        return;
    }
    public static Product Read(int id)
    {
        for (int i = 0; i < (DataSource.Config._indexArrProduct); i++)
        {
            if(id == DataSource.ProductList[i].Id)
            {
                return DataSource.ProductList[i];
            }
        }
        throw new Exception("baddddddd");
    }

    public static Product[] Read()
    {
        Product[] arr=new Product[DataSource.Config._indexArrProduct];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = DataSource.ProductList[i];
        }
        return arr;
    }

    public static void Update(Product obj)
    {
        for (int i = 0; i < (DataSource.Config._indexArrProduct); i++)
        {
            if (obj.Id == DataSource.ProductList[i].Id)
            {
                DataSource.ProductList[i] = obj;
            }
        }
    }
}
