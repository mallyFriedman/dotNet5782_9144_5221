using Dal;
using DalApi;
using BlApi;
using BO;
namespace BlImplementation
{
    internal class BLProduct : BlApi.IProduct
    {
        IDal Dal = new DalList();
        /// <summary>
        /// returns all the products
        /// in a way that the customer can see
        /// </summary>
        public IEnumerable<BO.ProductForList> GetAllForCustomer()
        {
            IEnumerable<DO.Product> a = Dal.Product.Get();
            List<BO.ProductForList> ForList = new List<BO.ProductForList>(a.Count());
            foreach (DO.Product item in a)
            {
                BO.ProductForList b = new BO.ProductForList();
                b.Id = item.Id;
                b.ProductName = item.ProductName;
                b.ProductPrice = item.Price;
                b.Category = (Category)item.Category;
                ForList.Add(b);
            }
            return ForList;
        }
        /// <summary>
        /// returns all the products
        /// in a way that the manager can see
        /// </summary>
        public IEnumerable<BO.ProductItem> GetAllForManager()
        {
            Random rand= new Random();
            IEnumerable<DO.Product> a = Dal.Product.Get();
            List<BO.ProductItem> Prod = new List<BO.ProductItem>(a.Count());
            foreach (DO.Product item in a)
            {
                BO.ProductItem b = new BO.ProductItem();
                b.Id = item.Id;
                b.ProductName = item.ProductName;
                b.Price = item.Price;
                b.Category = (Category)item.Category;
                if (item.InStock>0  ) { b.InStock =true; }
                else  { b.InStock = false;}
                b.AmountInCart = 0;
                Prod.Add(b);
            }
            return Prod;
        }
        /// <summary>
        /// the function returns the specific product
        /// that the manager wanted
        /// </summary>
        public Product GetManager(int id)
        {

            BO.Product bProduct = new BO.Product();
            if (id < 100000)
            {
                throw new BlIdNotValidException();
            }
            DO.Product dProduct = Dal.Product.Get(id);
             if (dProduct.Equals(default(DO.Product)))
             {
                throw new BlObjectNotFoundException();
            }
            bProduct.Category = (Category)dProduct.Category;
            bProduct.Id = dProduct.Id;
            bProduct.InStock = dProduct.InStock;
            bProduct.Price = dProduct.Price;
            bProduct.ProductName = dProduct.ProductName;
            return bProduct;
        }
        /// <summary>
        /// the function returns the specific product
        /// that the customer wanted
        /// </summary>
        public Product GetCustomer(int id)
        {
            BO.Product bProduct = new BO.Product();
            if (id < 100000)
            {
                throw new BlIdNotValidException();
            }
            DO.Product dProduct = Dal.Product.Get(id);
            if (dProduct.Equals(default(DO.Product))) 
            {
                throw new BlObjectNotFoundException();
            }
            bProduct.Category = (Category)dProduct.Category;
            bProduct.Id = dProduct.Id;
            bProduct.InStock = dProduct.InStock;
            bProduct.Price = dProduct.Price;
            bProduct.ProductName = dProduct.ProductName;
            return bProduct;
        }
        /// <summary>
        /// the function adds a product 
        /// to the list of products
        /// </summary>
        public void Add(Product product)
        {
            if ( product.Price < 0 || product.ProductName == null || product.InStock < 0)
            {
                throw new BlObjectNotValidException();
            }
            DO.Product dProduct = new DO.Product();
            dProduct.Id = product.Id;
            dProduct.Category = (DO.Enums.Category)product.Category;
            dProduct.InStock = product.InStock;
            dProduct.Price = product.Price;
            dProduct.ProductName = product.ProductName;
            Dal.Product.Add(dProduct);
        }
        /// <summary>
        /// the function deletes the product with the id it got
        /// </summary>
        public void Delete(int id)
        {
            if (id < 100000)
            {
                throw new BlIdNotValidException();

            }
            DO.OrderItem orderItem = DataSource.OrderItems.Find(o => o.Id == id);
            if (orderItem.Equals(default(DO.OrderItem)))
            {
                throw new BlObjectNotFoundException();
            }
            Dal.Product.Delete(id);
        }
        /// <summary>
        /// the function updates the product with the id it got
        /// </summary>
        public void Update(Product product)
        {
            if (product.Id < 100000 || product.Price < 0 || product.ProductName == null || product.InStock < 0)
            {
                throw new BlObjectNotValidException();
            }
            DO.Product dProduct = new DO.Product();
            dProduct.Id = product.Id;
            dProduct.Category = (DO.Enums.Category)product.Category;
            dProduct.InStock = product.InStock;
            dProduct.Price = product.Price;
            dProduct.ProductName = product.ProductName;
            Dal.Product.Update(dProduct);
        }
    }
}

   

