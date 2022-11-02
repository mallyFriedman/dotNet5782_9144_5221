
using System;
using DalFacade;
using DalList;
namespace DalList;

public static class DalOrder
{
    public static int Create(Order obj)
    {
        DataSource.orderArr[DataSource.Config.IndexArrOrder++] = obj;
        return obj.ID;
    }

    public static void Delete(int id)
    {
        for (int i = 0; i < DataSource.orderArr.Length; i++)
        {
            if (DataSource.orderArr[i].ID == id)
            {
                int index = DataSource.orderArr.Length;
                DataSource.orderArr[i] = DataSource.orderArr[index];
                DataSource.orderArr[index].ID = 0;
                DataSource.Config.IndexArrOrder = (DataSource.Config.IndexArrOrder - 1);
                break;
            }
        }
        return;
    }
    public static Order Read(int id)
    {
        for (int i = 0; i < (DataSource.Config.IndexArrOrder); i++)
        {
            if (id == DataSource.orderArr[i].ID)
            {
                return DataSource.orderArr[i];
            }
        }
        throw new Exception("baddddddd");
    }

    public static Order[] Read()
    {
        return DataSource.orderArr;
    }

    public static void Update(Order obj)
    {
        for (int i = 0; i < (DataSource.Config.IndexArrOrder); i++)
        {
            if (obj.ID == DataSource.orderArr[i].ID)
            {
                DataSource.orderArr[i] = obj;
            }
        }
    }
}


