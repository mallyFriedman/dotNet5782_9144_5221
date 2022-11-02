
using System;
using System.Collections.Generic;
using DO;
using DalList;
namespace DalList;




public static class DalOrderItem
{
    public static int Create(OrderItem obj)
    {
        DataSource.orderitem[DataSource.Config.IndexArrOrderItem++] = obj;
        return obj.ID;
    }

    public static void Delete(OrderItem obj)
    {
        for (int i = 0; i < DataSource.orderitem.Length; i++)
        {
            if (DataSource.orderitem[i].ID == obj.ID)
            {
                int index = DataSource.orderitem.Length;
                DataSource.orderitem[i] = DataSource.orderitem[index];
                DataSource.orderitem[index].ID = 0;
                DataSource.Config.IndexArrOrderItem = (DataSource.Config.IndexArrOrderItem - 1);
                break;
            }
        }
        return;
    }
    public static OrderItem Read(int id)
    {
        for (int i = 0; i < (DataSource.Config.IndexArrOrderItem); i++)
        {
            if (id == DataSource.orderitem[i].ID)
            {
                return DataSource.orderitem[i];
            }
        }
        throw new Exception("baddddddd");
    }

    public static OrderItem[] Read()
    {
        return DataSource.orderitem;
    }

    public static void Update(OrderItem obj)
    {
        for (int i = 0; i < (DataSource.Config.IndexArrOrderItem); i++)
        {
            if (obj.ID == DataSource.orderitem[i].ID)
            {
                DataSource.orderitem[i] = obj;
            }
        }
    }
}

