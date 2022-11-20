using BO;
namespace BlApi
{
    public interface IOrder
    {
        public IEnumerable<OrderForList> Get();
        public Order Get(int id);
        public Order UpdateSupply(int id);
        public Order UpdateShipping(int id);
        public Order UpdateManager(int id);
    }
}
