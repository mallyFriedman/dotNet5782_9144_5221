using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Window
    {

        private BlApi.IBl Bl;
        private BO.Cart cart = new();
        public Cart(BlApi.IBl bl, BO.Cart cart)
        {
            this.Bl = bl;
            InitializeComponent();
            this.cart = cart;
            DataContext = this.cart;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Bl.Cart.Confirm(cart,CustomerName.Text,CustomerEmail.Text,CustomerAdress.Text);
            MessageBox.Show("confirmed");
            new MainWindow().Show();
            this.Close();
        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new productItem(Bl,cart).Show();
            this.Hide();
        }

        private void ProductsItemListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProductsItemListview_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void deleteProduct(object sender, RoutedEventArgs e)
        {
            BO.OrderItem p = (BO.OrderItem)((Button)sender).DataContext;
            Bl.Cart.Update(cart, p.ProductID,0);
            this.Hide();
            new Cart(Bl, cart).Show();
        }

        private void decreaseProductBtn_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem p = (BO.OrderItem)((Button)sender).DataContext;
                Bl.Cart.Update(cart, p.ProductID, p.Amount - 1);
                this.Hide();
                new Cart(Bl, cart).Show();
            
        }
        private void addProductAmountBtn_Click(object sender, RoutedEventArgs e)
        {
           BO.OrderItem p = (BO.OrderItem)((Button)sender).DataContext;
            Bl.Cart.Update(cart, p.ProductID, p.Amount+1);
            this.Hide();
            new Cart(Bl, cart).Show();
        }
    }
    //our problems:
    //when the function goes out of the cart to the main windo- the cart refreshed-! it is a big problem!
    //binding of the pages!
}
