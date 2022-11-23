using BO;
namespace BlApi
{
    public interface IProduct
    {
        IDal Dal = new DalList();
        public IEnumerable<ProductForList> Get()
        {
            IEnumerable<Product> a =Dal.DalProduct.Get();
            IEnumerable<ProductForList> ForList;
            foreach (Product item in a)
            {
                ProductForList b = new ProductForList();
                b.Id = item.Id;
                b.ProductName = item.ProductName;
                b.ProductPrice = item.ProductPrice;
                b.Category = item.Category;
                ForList.push(b);
            }
            return ForList;
    }
        public IEnumerable<ProductItem> Read()
        {
            IEnumerable<Product> a = Dal.DalProduct.Get();
            IEnumerable<ProductItem> Prod;
            foreach (Order item in a)
            {
                ProductItem b = new ProductItem();
                b.Id = item.Id;
                b.ProductName = item.ProductName;
                b.ProductPrice = item.ProductPrice;
                b.Category = item.Category;
                b.InStock = item.InStock;
                b.AmountInCart = item.AmountInCart;//////////איך משיגים????????
                Prod.push(b);
            }
            return Prod;
        }
        public Product GetManager(int id)
        {
            if (id > 0)
            {
                Product p = Dal.DalProduct.Get(id);
            }
            else return
        }
        public Product GetCostumer(int id)
        {
            if (id > 0)
            {
                Product p = Dal.DalProduct.Get(id);
                return p;
            }
            else return
        }
        public void Add(Product product)
        {
            if(product.Id>0&& product.ProductPrice>0&& product.ProductName != null&&product.InStock>0)
            {
                Dal.DalOrder.Add(product);
            }
        }
        public void Delete(int id)
        {
            Dal.DalOrder.Delete(product);
        }
        public void Update(Product product)
        {
            if (product.Id > 0 && product.ProductPrice > 0 && product.ProductName != null && product.InStock > 0)
            {
                Dal.DalOrder.Update(product);
            }
        }
    }
}
