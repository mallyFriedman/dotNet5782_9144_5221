using BO;
using Dal;
using DalApi;
using BlApi;
namespace BlImplementation
{
    internal class BLOrder : BlApi.IOrder
    {
        IDal Dal = new DalList();
        /// <summary>
        /// the function  returns all orders
        /// </summary>
        public IEnumerable<BO.OrderForList> GetAll()
        {
            IEnumerable<DO.Order> orders = Dal.Order.Get();
            List<BO.OrderForList> ordersForList = new List<BO.OrderForList>(orders.Count());
            foreach (DO.Order item in orders)
            {
                BO.Order a = Get(item.Id);
                BO.OrderForList b = new BO.OrderForList();
                b.Id = item.Id;
                b.CustomerName = item.CustomerName;
                b.OrderStatus = a.OrderStatus;
                b.AmountProduct = a.OrderItem.Count();
                b.TotalPrice = a.TotalPrice;
                ordersForList.Add(b);
            }
            return ordersForList;
        }

        /// <summary>
        /// the function returnes the specific order by id
        /// </summary>
        public Order Get(int id)
        {
            if (id < 100000)
            {
                throw new BlIdNotValidException();
            }
            DO.Order dOrder = Dal.Order.GetSingle(ord => ord.Id == id);
            if (dOrder.Equals(default(DO.OrderItem)))
            {
                throw new BlObjectNotFoundException();
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
            List<BO.OrderItem> orderItem = new List<BO.OrderItem>(DataSource.ProductList.Count());
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
                orderItem.Add(bOrderItem);
                sum = sum + bOrderItem.TotalPrice;
            }
            bOrder.OrderItem = orderItem;
            bOrder.TotalPrice = sum;
            return bOrder;
        }
        /// <summary>
        /// updates the ship date to DateTime.Now
        /// </summary>
        public Order UpdateSupply(int id)
        {
            DO.Order dOrder = Dal.Order.Get(id);
            if (dOrder.Equals(default(DO.OrderItem)))
            {
                throw new BlObjectNotFoundException();
            }
            eOrderStatus a = status(dOrder.DeliveryDate, DateTime.MinValue, dOrder.ShipDate);
            if (a != eOrderStatus.Ordered)
            {
                throw new BlCannotChangeTheStatusException();
            }
            dOrder.ShipDate = DateTime.Now;
            Dal.Order.Update(dOrder);
            BO.Order bOrder = Get(id);
            return bOrder;
        }
        /// <summary>
        /// updates the delivery date to DateTime.Now
        /// </summary>
        public Order UpdateShipping(int id)
        {
            DO.Order dOrder = Dal.Order.Get(id);
            if (dOrder.Equals(default(DO.OrderItem)))
            {
                throw new BlObjectNotFoundException();
            }
            eOrderStatus a = status(dOrder.DeliveryDate, DateTime.MinValue, dOrder.ShipDate);
            if (!(a == eOrderStatus.Shipped && a != eOrderStatus.Delivered))
            {
                throw new BlCannotChangeTheStatusException();

            }
            dOrder.DeliveryDate = DateTime.Now;
            Dal.Order.Update(dOrder);
            BO.Order bOrder = Get(id);
            return bOrder;
        }

        /// <summary>
        /// the function returnes the status of the order and returns eOrderStatus
        /// </summary>
        private BO.eOrderStatus status(DateTime? DeliveryDate, DateTime? MinValue, DateTime? ShipDate)
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

