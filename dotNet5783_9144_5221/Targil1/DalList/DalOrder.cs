using DO;
namespace Dal;

public static class DalOrder
{
    public static int Create(Order obj)
    {
        DataSource.OrderArr[DataSource.Config._indexArrOrder++] = obj;
        return obj.Id;
    }

    public static void Delete(int id)
    {
        for (int i = 0; i < DataSource.OrderArr.Length; i++)
        {
            if (DataSource.OrderArr[i].Id == id)
            {
                int index = DataSource.Config._indexArrOrder;
                DataSource.OrderArr[i] = DataSource.OrderArr[index];
                DataSource.OrderArr[index].Id = 0;
                DataSource.Config._indexArrOrder = (DataSource.Config._indexArrOrder - 1);
                break;
            }
        }
        return;
    }
    public static Order Read(int id)
    {
        for (int i = 0; i < ((DataSource.Config._indexArrOrder)); i++)
        {
            if (id == DataSource.OrderArr[i].Id)
            {
                return DataSource.OrderArr[i];
            }
        }
        throw new Exception("baddddddd");
    }

    public static Order[] Read()
    {
        Order[] arr = new Order[DataSource.Config._indexArrOrder];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = DataSource.OrderArr[i];
        }
        return arr;
    }

    public static void Update(Order obj)
    {
        for (int i = 0; i < (DataSource.Config._indexArrOrder); i++)
        {
            if (obj.Id == DataSource.OrderArr[i].Id)
            {
                DataSource.OrderArr[i] = obj;
            }
        }
    }
}


