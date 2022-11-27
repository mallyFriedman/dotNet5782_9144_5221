using BO;
//using Dal;
using DalApi;
using System.Text.RegularExpressions;

namespace BlImplementation
{
    internal class BLCart : BlApi.ICart
    {
        IDal Dal = new Dal.DalList();
        /// <summary>
        /// the function adds an item to the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="id"></param>
        /// <returns>the updated cart</returns>
        public Cart Add(BO.Cart cart, int id)
        {
            DO.Product product = Dal.Product.Get(id);
            if (product.Equals(default(DO.OrderItem)))
            {
                // throw ();
            }
            if (product.InStock == 0)
            {
                // throw ();
            }
            BO.OrderItem item = cart.Items.Find(o => o.ProductID == id);
            if (item == null)
            {
                DO.Product dProduct = Dal.Product.Get(id);
                BO.OrderItem dItem = new();
                dItem.Id = 0;
                dItem.ProductID = id;
                dItem.ProductName = dProduct.ProductName;
                dItem.Price = dProduct.Price;
                dItem.Amount = 1;
                dItem.TotalPrice = dProduct.Price;
                cart.Items.Append(dItem);
            }
            else
            {
                item.Amount += 1;
                item.TotalPrice = item.TotalPrice + item.Price;
                cart.Items.Remove(item);
                cart.Items.Append(item);
            }
            cart.TotalPrice += product.Price;
            return cart;
        }
        /// <summary>
        /// the function updates the amount of the specific item
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="id"></param>
        /// <param name="newAmount"></param>
        /// <returns>the updated cart</returns>
        public Cart Update(Cart cart, int id, int newAmount)
        {

            BO.OrderItem item = cart.Items.Find(o => o.ProductID == id);

            if (item == null)
            {
                //throw()
            }
            double lastPrice = item.TotalPrice;

            if (item.Amount < newAmount)
            {
                int amount = newAmount - item.Amount;
                DO.Product product = Dal.Product.Get(id);
                if (product.Equals(default(DO.OrderItem)))
                {
                    // throw ();
                }
                if ((product.InStock - amount) == 0 || (product.InStock - amount) < 0)
                {
                    // throw ();
                }
                item.Amount += newAmount;
                item.TotalPrice = item.Price * newAmount;
                cart.Items.Remove(item);
                cart.Items.Append(item);
                cart.TotalPrice = cart.TotalPrice + amount * item.Price;
            }
            if (item.Amount > newAmount)
            {
                int amount = item.Amount - newAmount;
                item.Amount = newAmount;
                item.TotalPrice = item.Price * newAmount;
                cart.Items.Remove(item);
                cart.Items.Append(item);
                cart.TotalPrice = cart.TotalPrice - amount * item.Price;
            }
            if (item.Amount == 0)
            {
                cart.Items.Remove(item);
                cart.TotalPrice = cart.TotalPrice - item.TotalPrice;
            }
            return cart;

        }
        public void Confirm(Cart cart, string CustomerName, string CustomerEmail, string CustomerAdress)
        {
            var v = Regex.Match(CustomerEmail, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (CustomerName == null || CustomerAdress == null || (CustomerEmail != "" && v.Index == -1))
            {
                //throw();
            }
            cart.Items.ForEach(item =>
            {
                DO.Product product = Dal.Product.Get(item.Id);
                if (product.Equals(default(DO.OrderItem)))
                {
                    // throw ();
                }
                if (product.InStock < 0)
                {
                    // throw ();
                }
                if (product.InStock - item.Amount == 0 || product.InStock - item.Amount < 0)
                {
                    // throw ();
                }
            });
            DO.Order order = new DO.Order();
            order.Id = 1;
            order.CustomerAdress = CustomerAdress;
            order.CustomerEmail = CustomerEmail;
            order.CustomerName = CustomerName;
            order.OrderDate = DateTime.Now;
            order.ShipDate = DateTime.MinValue;
            order.DeliveryDate = DateTime.MinValue;
            int orderId= Dal.Order.Add(order);
            order.Id = orderId;
            cart.Items.ForEach(item =>
            {
                DO.Product product = Dal.Product.Get(item.Id);
                product.InStock-=item.Amount;
                Dal.Product.Update(product);
            });

            cart.Items.ForEach(item =>
            {
                DO.Product product = Dal.Product.Get(item.ProductID);
                DO.OrderItem orderItem = new();
                orderItem.Id = 0;
                orderItem.ProductID= item.ProductID;
                orderItem.OrderID=order.Id;
                orderItem.Price = product.Price;
                orderItem.Amount = item.Amount;
                int orderItemId=Dal.OrderItem.Add(orderItem);
                orderItem.Id = orderItemId;
            });

            // public IEnumerable<OrderItem> OrderItem { get; set; }
            //  public double TotalPrice { get; set; }
        }


        public double Price { get; set; }
        //  public IEnumerable<ProductForList> Get();
        //  public IEnumerable<ProductItem> Read();
        //  public Product GetManager(int id);
        //  public void Delete(int id);
    }
}
