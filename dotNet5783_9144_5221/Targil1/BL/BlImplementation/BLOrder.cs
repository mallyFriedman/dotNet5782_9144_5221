using BO;
using Dal;
using DalApi;
namespace BlImplementation
    internal class BLOrder : BlApi.IOrder
{
    IDal Dal = new DalList();
    public IEnumerable<BO.OrderForList> Get()
    {
        IEnumerable<DO.Order> orders = Dal.Order.Get();
        IEnumerable<BO.OrderForList> ordersForList = new List<BO.OrderForList>(orders.Count());
        foreach (DO.Order item in orders)
        {
            BO.Order a = Get(item.Id);
            BO.OrderForList b = new BO.OrderForList();
            b.Id = item.Id;
            b.CustomerName = item.CustomerName;
            b.OrderStatus = a.OrderStatus;
            b.AmountProduct = a.OrderItem.Count();
            b.TotalPrice = a.TotalPrice;
            ordersForList.Append(b);
        }
        return ordersForList;
    }
    public Order Get(int id)
    {
        DO.Order dOrder = Dal.Order.Get(id);
        if (dOrder.Equals(default(DO.OrderItem)))
        {///
        }
        BO.Order bOrder = new BO.Order();
        bOrder.Id = dOrder.Id;
        bOrder.CustomerAdress = dOrder.CustomerAdress;
        bOrder.CustomerEmail = dOrder.CustomerEmail;
        bOrder.CustomerName = dOrder.CustomerName;
        bOrder.DeliveryDate = dOrder.DeliveryDate;
        bOrder.OrderDate = dOrder.OrderDate;
        bOrder.OrderStatus = status(dOrder.DeliveryDate, DateTime.MinValue, dOrder.ShipDate);
        bOrder.ShipDate = dOrder.ShipDate;
        IEnumerable<BO.OrderItem> orderItem = new List<BO.OrderItem>(DataSource.ProductList.Count());
        double sum = 0;
        IEnumerable<DO.OrderItem> dOrderItem = (IEnumerable<DO.OrderItem>)Dal.OrderItem.ReadOrderId(bOrder.Id);
        foreach (DO.OrderItem item in dOrderItem)
        {
            BO.OrderItem bOrderItem = new BO.OrderItem();
            bOrderItem.Amount = item.Amount;
            bOrderItem.Id = item.Id;
            bOrderItem.Price = item.Price;
            bOrderItem.ProductID = item.ProductID;
            bOrderItem.TotalPrice = (item.Price) * (item.Amount);
            orderItem.Append(bOrderItem);
            sum = sum + bOrderItem.TotalPrice;
        }
        bOrder.OrderItem = orderItem;
        bOrder.TotalPrice = sum;
        return bOrder;
    }
    public Order UpdateSupply(int id)
    {

    }
    public Order UpdateShipping(int id);
    public Order UpdateManager(int id);
    private BO.eOrderStatus status(DateTime DeliveryDate, DateTime MinValue, DateTime ShipDate)
    {
        if (DeliveryDate > MinValue)
            return (BO.eOrderStatus)2;
        else if (ShipDate > MinValue)
            return (BO.eOrderStatus)1;
        else
            return (BO.eOrderStatus)0;
    }
}
}

