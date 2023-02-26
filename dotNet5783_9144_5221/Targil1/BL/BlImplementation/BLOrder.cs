using BO;
using DalApi;
using BlApi;
using Dal;
using DO;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Data;

namespace BlImplementation
{
    internal class BLOrder : BlApi.IOrder
    {
        static IDal Dal = DalApi.Factory.Get();
        /// <summary>
        /// the function  returns all orders
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.OrderForList> GetAll()
        {
            IEnumerable<DO.Order> orders = Dal.Order.Get();
            lock (Dal)
            {
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
        }

        /// <summary>
        /// the function returnes the specific order by id
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Order Get(int id = 0)
        {
            if (id < 500000)
            {
                throw new BlIdNotValidException();
            }
            DO.Order dOrder;
            lock (Dal)
            {
                dOrder = Dal.Order.GetSingle(ord => ord.Id == id);
            }
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
            List<BO.OrderItem> orderItem;
            lock (Dal)
            {
                orderItem = new List<BO.OrderItem>(Dal.Product.Get()?.Count() ?? 0);
            }
            IEnumerable<DO.OrderItem> dOrderItem;
            lock (Dal)
            {
                dOrderItem = (IEnumerable<DO.OrderItem>)Dal.OrderItem.Get(ord => ord.OrderID == bOrder.Id);
            }

            double sum = 0;
            bOrder.OrderItem = from item in dOrderItem
                               select new BO.OrderItem
                               {
                                   Amount = item.Amount,
                                   Id = item.Id,
                                   Price = item.Price,
                                   ProductID = item.ProductID,
                                   TotalPrice = ((item.Price) * (item.Amount))
                               };
            bOrder.OrderItem.Sum(item => sum += item.TotalPrice);

            bOrder.TotalPrice = sum;
            return bOrder ?? throw new BlObjectNotFoundException();
        }
        /// <summary>
        /// updates the ship date to DateTime.Now
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Order UpdateSupply(int id)
        {
            DO.Order dOrder;
            lock (Dal)
            {
                dOrder = Dal.Order.Get(id);
            }

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
            lock (Dal)
            {
                Dal.Order.Update(dOrder);
            }
            BO.Order bOrder;
            lock (Dal)
            {
                bOrder = Get(id);
            }
            return bOrder;
        }
        /// <summary>
        /// updates the delivery date to DateTime.Now
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Order UpdateShipping(int id)
        {
            DO.Order dOrder;
            lock (Dal)
            {
                dOrder = Dal.Order.Get(id);
            }
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
            lock (Dal)
            {
                Dal.Order.Update(dOrder);
            }
            BO.Order bOrder;
            lock (Dal)
            {
                bOrder = Get(id);
            }
            //  UpdateTrack(1, id);
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
        /// <summary>
        /// the function returnes the dld Order that orderd
        ///</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public OrderTracking GetOrderTracking(int id)
        {
            BO.Order? order;
            lock (Dal)
            {
                order = Get(id);
            }
            if (order.Id == 0)
            {
                throw new BlObjectNotFoundException();
            }
            OrderTracking orderTracking = new();
            orderTracking.OrderStatus = order.OrderStatus;
            orderTracking.Id = id;
            orderTracking.StatusList = new();
            if (!((DateTime)order?.OrderDate == DateTime.MinValue))
            {
                lock (Dal)
                {
                    orderTracking?.StatusList?.Add(new Tuple<DateTime, BO.eOrderStatus>((DateTime)order.OrderDate, (BO.eOrderStatus)0));
                }
                if (!((DateTime)order.ShipDate == DateTime.MinValue))
                {
                    lock (Dal)
                    {
                        orderTracking?.StatusList?.Add(new Tuple<DateTime, BO.eOrderStatus>((DateTime)order.ShipDate, (BO.eOrderStatus)1));
                    }
                    if (!((DateTime)order.DeliveryDate == DateTime.MinValue))
                    {
                        lock (Dal)
                        {
                            orderTracking?.StatusList?.Add(new Tuple<DateTime, BO.eOrderStatus>((DateTime)order.DeliveryDate, (BO.eOrderStatus)2));
                        }
                    }
                }
            }
            return orderTracking;
        }

        public int? GetOrderToUpdate()
        {
            IEnumerable<DO.Order>? ordered;
            lock (Dal)
            {
                ordered = Dal?.Order.Get(order => order.ShipDate == DateTime.MinValue);
            }
            IEnumerable<DO.Order>? shiped;
            lock (Dal)
            {
                shiped = Dal?.Order.Get(order => order.DeliveryDate == DateTime.MinValue);
            }
            DateTime? minOrdered = ordered?.Min(x => x.OrderDate);
            DateTime? minShiped = shiped?.Min(x => x.ShipDate);
            int idMinOrdered = (from order in ordered
                                where order.OrderDate == minOrdered
                                select order.Id).FirstOrDefault();
            int idMinShiped = (from ship in shiped
                               where ship.ShipDate == minShiped
                               select ship.Id).FirstOrDefault();
            if (minOrdered < minShiped || (minShiped == default && minOrdered != default))
                return idMinOrdered;
            if (minShiped < minOrdered || (minOrdered == default && minShiped != default))
                return idMinShiped;
            else
                return null;
        }
    }
}

