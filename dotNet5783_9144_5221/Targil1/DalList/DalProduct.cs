using System;
using System.Collections.Generic;
using DO;
namespace Dal;

public static class DalProduct
{
    public static int Create(Product obj)
    {
        DataSource.ProductList[DataSource.Config.IndexArrProduct++] = obj;
        return obj.ID;
    }

    public static void Delete(Product obj)
    {
        for (int i = 0; i < DataSource.ProductList.Length; i++)
        {
            if (DataSource.ProductList[i].ID== obj.ID)
            {
                int index = DataSource.ProductList.Length;
                DataSource.ProductList[i] = DataSource.ProductList[index];
                DataSource.ProductList[index].ID = 0;
                DataSource.Config.IndexArrProduct = (DataSource.Config.IndexArrProduct - 1);
                break;
            }
        }
        return;
    }
    public static Product Read(int id)
    {
        for (int i = 0; i < (DataSource.Config.IndexArrProduct); i++)
        {
            if(id == DataSource.ProductList[i].ID)
            {
                return DataSource.ProductList[i];
            }
        }
        throw new Exception("baddddddd");
    }

    public static Product[] Read()
    {
        return DataSource.ProductList;
    }

    public static void Update(Product obj)
    {
        for (int i = 0; i < (DataSource.Config.IndexArrProduct); i++)
        {
            if (obj.ID == DataSource.ProductList[i].ID)
            {
                DataSource.ProductList[i] = obj;
            }
        }
    }
}
