
using System;
using System.Collections.Generic;
using DalFacade;
using DalList;
namespace DalList;




public static class DalOrderItem
{
    public static int Create(OrderItem obj)
    {
        DataSource.orderitem[DataSource.Config.IndexArrOrderItem++] = obj;
        return obj.ID;
    }

    public static void Delete(int id)
    {
        for (int i = 0; i < DataSource.orderitem.Length; i++)
        {
            if (DataSource.orderitem[i].ID == id)
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
    public static OrderItem find(int id)
    {
        OrderItem a = new OrderItem();
        for (int i = 0; i < DataSource.orderitem.Length; i++)
        {
            if (DataSource.orderitem[i].ID == id)
            {
                a= DataSource.orderitem[i];
                break;
            }
        }
        return a;
    }
    public static OrderItem[] Read(int id)
    {
        OrderItem[] arr = new OrderItem[4];
        int num = 0;
        for (int i = 0; i < (DataSource.Config.IndexArrOrderItem); i++)
        {
            if (id == DataSource.orderitem[i].OrderID)
            {
                arr[num] = DataSource.orderitem[i];
                num++;
            }
        }
        if (num!=0) return arr;
        throw new Exception("no items in this order...");

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

