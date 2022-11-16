using DO;
namespace Dal;

public static class DalOrder
{
    public static int Create(Order obj)
    {
        DataSource.OrderArr.Add(obj);
        DataSource.Config._indexArrOrder++;
        return obj.Id;
    }

    public static void Delete(int id)
    {
        DataSource.OrderArr.Remove(DataSource.OrderArr.Find(o => o.Id == id));
    }
    public static Order Read(int id)
    {
        Order p = DataSource.OrderArr.Find(o => o.Id == id);
       //if (p.Id == 0)/////////////
       //{
       //    throw new Exception("baddddddd");
       //}
        return p;
    }

    public static List<Order> Read()
    {
        return DataSource.OrderArr;
    }

    public static void Update(Order obj)
    {
        int i = DataSource.OrderArr.FindIndex(o => o.Id == obj.Id);
        if (i == -1)
            throw new Exception("no items in this order...");
        DataSource.OrderArr[i] = obj;
    }
}


