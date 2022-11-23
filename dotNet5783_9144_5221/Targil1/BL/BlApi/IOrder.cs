using BO;
namespace BlApi
{
    public interface IOrder
    {
        public IEnumerable<BO.OrderForList> Get();
        public Order Get(int id);
        public Order UpdateSupply(int id);
        public Order UpdateShipping(int id);
        public Order UpdateManager(int id);
    }
}
