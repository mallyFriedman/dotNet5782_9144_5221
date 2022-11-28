using Dal;
using DalApi;
using BO;
namespace BlImplementation
{
    internal class BLProduct : BlApi.IProduct
    {
        IDal Dal = new DalList();
        //get arr:
        public IEnumerable<BO.ProductForList> GetAllForCustomer()
        {
            IEnumerable<DO.Product> a = Dal.Product.Get();

            IEnumerable<BO.ProductForList> ForList = new List<BO.ProductForList>(a.Count());
            foreach (DO.Product item in a)
            {
                BO.ProductForList b = new BO.ProductForList();
                b.Id = item.Id;
                b.ProductName = item.ProductName;
                //b.ProductPrice = item.ProductPrice;
                b.Category = (Category)item.Category;
                ForList.Append(b);
            }
            return ForList;
        }
        public IEnumerable<BO.ProductItem> GetAllForManager()
        {
            IEnumerable<DO.Product> a = Dal.Product.Get();
            IEnumerable<BO.ProductItem> Prod = new List<BO.ProductItem>(a.Count());
            foreach (DO.Product item in a)
            {
                BO.ProductItem b = new BO.ProductItem();
                b.Id = item.Id;
                b.ProductName = item.ProductName;
                b.Price = item.Price;
                b.Category = (Category)item.Category;
                if (item.InStock>0 || item.InStock == 0)
                {
                    b.InStock =false;
                }
                b.InStock = true;

                /// b.AmountInCart = item.AmountInCart;//////////איך משיגים????????
                Prod.Append(b);
            }
            return Prod;
        }
        //get one product:
        public Product GetManager(int id)
        {
            BO.Product bProduct = new BO.Product();
            if (id < 0)
            {
                //  throw Exception("dd");
            }
            DO.Product dProduct = Dal.Product.Get(id);
            /////////// if (dProduct.Id<0 )
            /////////// {    //  throw Exception("dd");
            /////////// }
            bProduct.Category = (Category)dProduct.Category;
            bProduct.Id = dProduct.Id;
            bProduct.InStock = dProduct.InStock;
            bProduct.Price = dProduct.Price;
            bProduct.ProductName = dProduct.ProductName;
            return bProduct;
        }

        public Product GetCostumer(int id)
        {
            BO.Product bProduct = new BO.Product();
            if (id < 0)
            {
                //  throw Exception("dd");
            }
            DO.Product dProduct = Dal.Product.Get(id);
            /////////// if (dProduct.Id<0 )
            /////////// {    //  throw Exception("dd");
            /////////// }
            bProduct.Category = (Category)dProduct.Category;
            bProduct.Id = dProduct.Id;
            bProduct.InStock = dProduct.InStock;
            bProduct.Price = dProduct.Price;
            bProduct.ProductName = dProduct.ProductName;
            return bProduct;
        }
        public void Add(Product product)
        {
            if (product.Id < 0 || product.Price < 0 || product.ProductName == null || product.InStock < 0)
            {
             //   throw Exception("bad")
            }
            DO.Product dProduct = new DO.Product();
            dProduct.Id = product.Id;
            dProduct.Category = (Category)product.Category;
            dProduct.InStock = product.InStock;
            dProduct.Price = product.Price;
            dProduct.ProductName = product.ProductName;
            Dal.Product.Add(dProduct);
        }
        public void Delete(int id)
        {
            DO.OrderItem orderItem = DataSource.OrderItems.Find(o => o.Id == id);
            if (orderItem.Equals(default(DO.OrderItem)))//////////
            {
                /////
                /////
            }
            Dal.Product.Delete(id);
        }
        public void Update(Product product)
        {
            if (product.Id < 0 || product.Price < 0 || product.ProductName == null || product.InStock < 0)
            {
                //   throw Exception("bad")
            }
            DO.Product dProduct = new DO.Product();
            dProduct.Id = product.Id;
            dProduct.Category = (Category)product.Category;
            dProduct.InStock = product.InStock;
            dProduct.Price = product.Price;
            dProduct.ProductName = product.ProductName;
            Dal.Product.Update(dProduct);
        }
    }
}

   

