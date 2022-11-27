using BO;
namespace BlApi
{
    public interface IProduct
    {

        //get arr:
        public IEnumerable<BO.ProductForList> Get();
        public IEnumerable<BO.ProductItem> Read();
        //get one product:
        public Product GetManager(int id);

        public Product GetCustomer(int id);
        public void Add(Product product);
        public void Delete(int id);
        public void Update(Product product);
    }
}
