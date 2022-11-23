using BO;
namespace BlApi
{
    public interface IOrder
    {
        public IEnumerable<OrderForList> Get()
        {
            IEnumerable<Order> a = Dal.DalOrder.Get();
            IEnumerable<OrderForList> ForList;
            foreach (Order item in a)
            {
                OrderForList b = new OrderForList();
                b.Id = item.Id;
                b.CustomerName = item.CustomerName;
                b.OrderStatus = item.OrderStatus;
                b.AmountProduct = item.AmountProduct;///////איך???
                b.TotalPrice = item.TotalPrice;
            }
            return ForList;

        }
        public Order Get(int id)
        {
            Order a = Dal.DalOrder.Get(id);
            if (a.Id>0)
            {
                return a;
            }
            return;
        }
        public Order UpdateSupply(int id);
        public Order UpdateShipping(int id);
        public Order UpdateManager(int id);
    }
}
