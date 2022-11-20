using BO;
namespace BlApi
{
    public interface IProduct
    {
        public IEnumerable<ProductForList> Get();
        public IEnumerable<ProductItem> Read();
        public Product GetManager(int id);
        public Product GetCostumer(int id);
        public void Add(Product product);
        public void Delete(int id);
        public void Update(Product product);


    }
}
