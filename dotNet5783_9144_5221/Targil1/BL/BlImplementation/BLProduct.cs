using DalApi;
using BlApi;
using BO;
using DO;
using Dal;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BlImplementation
{
    internal class BLProduct : BlApi.IProduct
    {
        static IDal Dal = DalApi.Factory.Get();
        /// <summary>
        /// returns all the products
        /// in a way that the customer can see
        /// </summary>
         [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.ProductForList> GetAllForCustomer(BO.Category? category = null)
        {
            List<DO.Product> a=new();
            if (category == null)
                a = Dal.Product.Get().ToList();
            else a = Dal.Product.Get(p => (BO.Category)p.Category == category).ToList();
            var ForList = from item in a
                          select new BO.ProductForList
                          {
                              Id = item.Id,
                              ProductName = item.ProductName,
                              Price = item.Price,
                              Category = (Category)item.Category
                          };
            return ForList;
        }
        /// <summary>
        /// returns all the products
        /// in a way that the manager can see
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.ProductItem> GetAllForManager(BO.Category? category = null)
        {
            Random rand = new Random();
            IEnumerable<DO.Product>? a;

            if (category == null)
                a = Dal.Product.Get();
            else a = Dal.Product.Get(p => (BO.Category)p.Category == category);
            var Prod = from item in a
                          select new BO.ProductItem
                          {
                              Id = item.Id,
                              ProductName = item.ProductName,
                              Price = item.Price,
                              Category = (Category)item.Category,
                              InStock = item.InStock >0 ? true : false,
                              AmountInStock= item.InStock
                          };          
            return Prod;
        }
        /// <summary>
        /// the function returns the specific product
        /// that the manager wanted
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Product GetManager(int id)
        {

            BO.Product bProduct = new BO.Product();
            if (id < 100000)
            {
                throw new BlIdNotValidException();
            }
            DO.Product dProduct = Dal.Product.GetSingle(prod => prod.Id == id);
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Product GetCustomer(int id)
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add(BO.Product product)
        {
            if (product.Price < 1 || product.ProductName == null || product.InStock < 0)
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int id)
        {
            if (id < 100000)
            {
                throw new BlIdNotValidException();

            }
            List<DO.OrderItem>? orderItem = Dal?.OrderItem?.Get()?.ToList();
            orderItem?.Find(o => o.Id == id);
            if (orderItem.Equals(default(DO.OrderItem)))
            {
                throw new BlObjectNotFoundException();
            }
            Dal.Product.Delete(id);
        }
        /// <summary>
        /// the function updates the product with the id it got
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(BO.Product product)
        {
            if (product.Id < 100000 || product.Price < 0 || product.ProductName == null || product.InStock < 0)
            {
                throw new BlObjectNotValidException();
            }
            DO.Product dProduct = new DO.Product();
            dProduct.Id = product.Id;
            dProduct.Category = (DO.Enums.Category)product.Category;
            dProduct.InStock = product.InStock;
            dProduct.Price = 5;
            dProduct.InStock = 9;
            dProduct.Price = product.Price;
            dProduct.ProductName = product.ProductName;
            Dal.Product.Update(dProduct);
        }
    }
}



