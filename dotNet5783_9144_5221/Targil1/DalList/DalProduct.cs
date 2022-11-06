using System;
using System.Collections.Generic;
using DalFacade;
using DalList;
namespace DalList;

public static class DalProduct
{
    public static int Create(Product obj)
    {
        DataSource.ProductList[DataSource.Config.IndexArrProduct++] = obj;
        return obj.ID;
    }

    public static void Delete(int id)
    {
        for (int i = 0; i < DataSource.Config.IndexArrProduct; i++)
        {
            if (DataSource.ProductList[i].ID== id)
            {
                int index = DataSource.Config.IndexArrProduct;
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
