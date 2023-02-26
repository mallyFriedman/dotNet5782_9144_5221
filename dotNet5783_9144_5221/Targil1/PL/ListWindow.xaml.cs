using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class ListWindow : Window
    {
        private BlApi.IBl? Bl;
        private BO.Cart? cart = new();
        private Window lastWindow;
        Tuple<IEnumerable<BO.ProductForList>, IEnumerable<BO.OrderForList>, Array> dct;
        /// <summary>
        /// constructor of the page
        /// </summary>
        public ListWindow(BlApi.IBl bl, BO.Cart cart, Window lastWindow)
        {
            try
            {
                this.Bl = bl;
                this.cart = cart;
                InitializeComponent();
               
                dct = new Tuple< IEnumerable<BO.ProductForList>, IEnumerable<BO.OrderForList>,Array>
                    (Bl.Product.GetAllForCustomer(), Bl.Order.GetAll(), Enum.GetValues(typeof(BO.Category)));// CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
                DataContext = dct;
            }
            catch (BlIdNotValidException ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.lastWindow = lastWindow;   
        }
        /// <summary>
        /// Setting the selection menu
        /// </summary>
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Array a = Enum.GetValues(typeof(BO.Category));
            /////////////////////////////////////////////
            dct = new Tuple<IEnumerable<BO.ProductForList>, IEnumerable<BO.OrderForList>, Array>
                   (Bl.Product.GetAllForCustomer((BO.Category)CategorySelector.SelectedItem), Bl.Order.GetAll(), a);// CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            DataContext = dct;
        }

        /// <summary>
        /// the function routs to the add page
        /// </summary>
        private void GoToAdd(object sender, RoutedEventArgs e)
        {
            new ProductWindow(Bl,cart,this).Show();
            this.Hide();
        }
        /// <summary>
        /// the function routs to the update/delete page
        /// </summary>
        private void product_Click(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList p = (BO.ProductForList)((ListView)sender).SelectedItem;
            new ProductWindow(Bl, cart, this,p).Show();
            this.Hide();

        }

        private void order_Click(object sender, MouseButtonEventArgs e)
        {
            order_Click(sender, e, cart);
        }

        private void order_Click(object sender, MouseButtonEventArgs e, BO.Cart? cart)
        {
            BO.OrderForList p = (BO.OrderForList)((ListView)sender).SelectedItem;
            new OrderWindow(Bl, cart, this, p.Id, true).Show();     /////////
            this.Hide();

        }



        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OrdersListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lastWindow.Show();
            this.Hide();
        }
    }
}
