using BO;

namespace BlApi
{
    public interface ICart
    {
        public Cart  Add(Cart cart, int id);
        public Cart Update(Product product,int id,int newAmount);
        public Product Confirm(Cart cart,string CustomerName,string CustomerEmail, string CustomerAdress);
        public IEnumerable<ProductForList> Get();
        public IEnumerable<ProductItem> Read();
        public Product GetManager(int id);
        public void Delete(int id);
       
    }
}
