using BO;
using DalApi;
using BlApi;
using Dal;
using DO;
using System.Diagnostics;

namespace BlImplementation
{
    internal class BLOrder : BlApi.IOrder
    {
        static IDal Dal = DalApi.Factory.Get();
        /// <summary>
        /// the function  returns all orders
        /// </summary>
        public IEnumerable<BO.OrderForList> GetAll()
        {
            IEnumerable<DO.Order> orders = Dal.Order.Get();
            
                var ordersForList = from item in orders
                                    let a = Get(item.Id)
                                    select new BO.OrderForList
                                    {
                                        Id = item.Id,
                                        CustomerName = item.CustomerName,
                                        OrderStatus = a.OrderStatus,
                                        AmountProduct = a.OrderItem?.Count() ?? 0,
                                        TotalPrice = a.TotalPrice
                                    };
            return ordersForList;
        }

        /// <summary>
        /// the function returnes the specific order by id
        /// </summary>
        public BO.Order Get(int id = 0)
        {
            if (id < 500000)
            {
                throw new BlIdNotValidException();
            }
            DO.Order dOrder = Dal.Order.GetSingle(ord => ord.Id == id);
            if (dOrder.Equals(default(DO.OrderItem)))
            {
                throw new BlObjectNotFoundException();
            }
            BO.Order bOrder = new BO.Order
            {
                Id = dOrder.Id,
                CustomerAdress = dOrder.CustomerAdress,
                CustomerEmail = dOrder.CustomerEmail,
                CustomerName = dOrder.CustomerName,
                DeliveryDate = dOrder.DeliveryDate,
                OrderDate = dOrder.OrderDate,
                OrderStatus = status(dOrder.DeliveryDate, DateTime.MinValue, dOrder.ShipDate),
                ShipDate = dOrder.ShipDate
            };
            List<BO.OrderItem> orderItem = new List<BO.OrderItem>(Dal.Product.Get()?.Count() ?? 0);
            IEnumerable<DO.OrderItem> dOrderItem = (IEnumerable<DO.OrderItem>)Dal.OrderItem.Get(ord => ord.OrderID == bOrder.Id);
            double sum = 0;
            bOrder.OrderItem = from item in dOrderItem
                               select new BO.OrderItem
                               {
                                   Amount = item.Amount,
                                   Id = item.Id,
                                   Price = item.Price,
                                   ProductID = item.ProductID,
                                   TotalPrice = ((item.Price) * (item.Amount))
                                   //sum = sum + item.TotalPrice
                               };


            // foreach (DO.OrderItem item in dOrderItem)
            // {
            //     BO.OrderItem bOrderItem = new BO.OrderItem();
            //     bOrderItem.Amount = item.Amount;
            //     bOrderItem.Id = item.Id;
            //     bOrderItem.Price = item.Price;
            //     bOrderItem.ProductID = item.ProductID;
            //     bOrderItem.TotalPrice = (item.Price) * (item.Amount);
            //     orderItem.Add(bOrderItem);
            //     sum = sum + bOrderItem.TotalPrice;
            // }
            bOrder.TotalPrice = sum;
            return bOrder;
        }
        /// <summary>
        /// updates the ship date to DateTime.Now
        /// </summary>
        public BO.Order UpdateSupply(int id)
        {
            DO.Order dOrder = Dal.Order.Get(id);
            if (dOrder.Equals(default(DO.OrderItem)))
            {
                throw new BlObjectNotFoundException();
            }
            eOrderStatus a = status(dOrder.DeliveryDate, DateTime.MinValue, dOrder.ShipDate);
            if (a != eOrderStatus.Shipped)
            {
                throw new BlCannotChangeTheStatusException();
            }
            dOrder.DeliveryDate = DateTime.Now;
            Dal.Order.Update(dOrder);
            BO.Order bOrder = Get(id);
            return bOrder;
        }
        /// <summary>
        /// updates the delivery date to DateTime.Now
        /// </summary>
        public BO.Order UpdateShipping(int id)
        {
            DO.Order dOrder = Dal.Order.Get(id);
            if (dOrder.Equals(default(DO.OrderItem)))
            {
                throw new BlObjectNotFoundException();
            }
            eOrderStatus a = status(dOrder.DeliveryDate, DateTime.MinValue, dOrder.ShipDate);
            if (!(a == eOrderStatus.Ordered && a != eOrderStatus.Shipped))
            {
                throw new BlCannotChangeTheStatusException();

            }
            dOrder.ShipDate = DateTime.Now;
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

            //  if (DeliveryDate > DateTime.MinValue)
            //      return (BO.eOrderStatus)3;
            //  else if (ShipDate > DateTime.MinValue)
            //      return (BO.eOrderStatus)2;
            //  else
            //      return (BO.eOrderStatus)1;

        }
    }
}

