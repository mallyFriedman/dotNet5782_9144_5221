using BO;
using Dal;
using DalApi;

namespace BlImplementation
{
    internal class BLCart : BlApi.ICart
    {
        IDal Dal = new DalList();
        public Cart Add(BO.Cart cart, int id)
        {
            DO.Product product = Dal.Product.Get(id);
            if (product.InStock == 0)
            {
                // throw ();
            }

            DO.OrderItem item = cart.Items.Find(o => o.Id == id);
            if (item.Equals(default(DO.OrderItem)))
            {
                BO.Product bProduct = IProduct.GetCostumer(id);
                cart.Items.Append(bProduct);
            }
            else
            {
                item.Amount += 1;
                item.TotalPrice = item.TotalPrice+item.Price;
            }
            cart.TotalPrice += product.Price;
            

        }
        public Cart Update(Product product, int id, int newAmount);
        public Product Confirm(Cart cart, string CustomerName, string CustomerEmail, string CustomerAdress);
        public IEnumerable<ProductForList> Get();
        public IEnumerable<ProductItem> Read();
        public Product GetManager(int id);
        public void Delete(int id);
    }
}
