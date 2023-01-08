using BO;
using DalApi;
using BlApi;
using System.Text.RegularExpressions;
using DO;
using System.Diagnostics;

namespace BlImplementation
{
    internal class BLCart : BlApi.ICart
    {
        static IDal Dal = DalApi.Factory.Get();
        /// <summary>
        /// the function adds an item to the cart and returens the updated cart
        /// </summary>
        public Cart Add(BO.Cart cart, int id)
        {
            DO.Product product = Dal.Product.Get(id);
            if (product.Equals(default(DO.Product)))
            {
                throw new BlObjectNotFoundException();
            }
            if (product.InStock == 0)
            {
                throw new BlOutOfStockException();
            }
            var item = new BO.OrderItem();///
            if (cart.Items != null)
            {
                item = cart.Items.Find(o => o.ProductID == id);
                if (item == null)
                {
                    item = new BO.OrderItem();
                }
            }
            else
            {
                item = new BO.OrderItem();
                cart.Items = new();
            }

            if (item.ProductID == 0)
            {
                DO.Product dProduct = Dal.Product.Get(id);
                cart.Items.Add(new BO.OrderItem
                {
                    Id = 0,
                    ProductID = id,
                    ProductName = dProduct.ProductName,
                    Price = dProduct.Price,
                    Amount = 1,
                    TotalPrice = dProduct.Price
                });
            }
            else
            {
                item.Amount += 1;
                item.TotalPrice = item.TotalPrice + item.Price;
                cart.Items.Remove(item);
                cart.Items.Add(item);
            }
            cart.TotalPrice += product.Price;
            return cart;
        }
        /// <summary>
        /// the function updates the amount of the specific item and returns the updated cart
        /// </summary>
        public Cart Update(Cart cart, int id, int newAmount)
        {
            if (cart.Items == null)
            {
                throw new BlCartIsEmptyException();
            }
            BO.OrderItem item = cart.Items.Find(o => o.ProductID == id);

            if (item == null)
            {
                throw new BlObjectNotFoundException();
            }
            double lastPrice = item.TotalPrice;

            if (item.Amount < newAmount)
            {
                int amount = newAmount - item.Amount;
                DO.Product product = Dal.Product.Get(id);
                if (product.Equals(default(DO.OrderItem)))
                {
                    throw new BlObjectNotFoundException();
                }
                if ((product.InStock - amount) == 0 || (product.InStock - amount) < 0)
                {
                    throw new BlOutOfStockException();
                }
                item.Amount = newAmount;
                item.TotalPrice = item.Price * newAmount;
                cart.Items.Remove(item);
                cart.Items.Add(item);
                cart.TotalPrice = cart.TotalPrice + amount * item.Price;
            }
            if (item.Amount > newAmount)
            {
                int amount = item.Amount - newAmount;
                item.Amount = newAmount;
                item.TotalPrice = item.Price * newAmount;
                cart.Items.Remove(item);
                cart.Items.Add(item);
                cart.TotalPrice = cart.TotalPrice - amount * item.Price;
            }
            if (item.Amount == 0)
            {
                cart.Items.Remove(item);
                cart.TotalPrice = cart.TotalPrice - item.TotalPrice;
            }
            return cart;

        }
        /// <summary>
        /// the function confirms the order
        /// </summary>
        public void Confirm(Cart cart, string CustomerName, string CustomerEmail, string CustomerAdress)
        {
            var v = Regex.Match(CustomerEmail, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (CustomerName == null || CustomerAdress == null || (CustomerEmail != "" && v.Index == -1))
            {
                throw new BlDetailsNotValidException();
            }
            //////////////////////
            cart.Items?.ForEach(item =>
            {
                DO.Product product = Dal.Product.Get(item.ProductID);
                if (product.Equals(default(DO.Product))) throw new BlObjectNotFoundException();

                if (product.InStock < 0 || product.InStock - item.Amount == 0 || product.InStock - item.Amount < 0)
                {
                    throw new BlOutOfStockException();
                }
            });
            int orderId = Dal.Order.Add(
                new DO.Order()
                {
                    Id = 1,
                    CustomerAdress = CustomerAdress,
                    CustomerEmail = CustomerEmail,
                    CustomerName = CustomerName,
                    OrderDate = DateTime.Now,
                    ShipDate = DateTime.MinValue,
                    DeliveryDate = DateTime.MinValue
                });

            ///////////////////
            cart.Items.ForEach(item =>
            {
                DO.Product product = Dal.Product.Get(item.ProductID);
                product.InStock -= item.Amount;
                Dal.Product.Update(product);
            });


            cart.Items.ForEach(item =>
            {
                DO.Product product = Dal.Product.Get(item.ProductID);
                int orderItemId = Dal.OrderItem.Add(
                    new DO.OrderItem()
                    {
                        Id = 0,
                        ProductID = item.ProductID,
                        OrderID = orderId,
                        Price = product.Price,
                        Amount = item.Amount
                    });
            });
        }
    }
}
