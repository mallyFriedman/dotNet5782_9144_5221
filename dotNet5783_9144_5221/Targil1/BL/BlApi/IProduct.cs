using BO;
namespace BlApi
{
    public interface IProduct
    {

        //get arr:
        public IEnumerable<BO.ProductForList> GetAllForCustomer(BO.Category? category = null);
        public IEnumerable<BO.ProductItem> GetAllForManager(BO.Category? category = null);
        //get one product:
        public Product GetManager(int id);
        public Product GetCustomer(int id);
        public void Add(Product product);
        public void Delete(int id);
        public void Update(Product product);
    }
}
