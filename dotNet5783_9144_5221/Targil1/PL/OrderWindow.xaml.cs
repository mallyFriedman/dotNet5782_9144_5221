using BlApi;
using BO;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static DO.Enums;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderWindo.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private int id;
        private BO.Cart? cart;
        private BlApi.IBl? Bl;
        private Window lastWindow;

        /// <summary>
        /// constractor of the page
        /// </summary>
        public OrderWindow(BlApi.IBl bl, BO.Cart cart,Window window, int id = 0, bool admin = false)
        {
            try
            {
                this.lastWindow = window;
                this.cart = cart;
                this.id = id;
                this.Bl = bl;
                InitializeComponent();
                BO.Order order;
                if (!admin)
                {
                    UpdateSupply.Visibility = Visibility.Hidden;
                    UpdateShipping.Visibility = Visibility.Hidden;
                }
                if (id != 0)
                {
                    order = Bl.Order.Get(id);
                    int sum = 0;
                    order?.OrderItem?.Select(ord =>
                    {
                        sum = sum + 1;
                        return ord;
                    }).ToList();
                    amount.Content = sum;
                    DataContext = order;
                    OrderItemsListview.ItemsSource = order?.OrderItem;
                }
            }
            catch (BlIdNotValidException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// the function updates the chosen product in the list
        /// </summary>
        private void UpdateSupply1(object sender, RoutedEventArgs e)
        {
            try
            {
                Bl?.Order.UpdateSupply(id);
                MessageBox.Show("updated succesfuly!");
                new ListWindow(Bl, cart,this).Show();
                this.Hide();
            }
            catch (BlCannotChangeTheStatusException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void UpdateShipping1(object sender, RoutedEventArgs e)
        {
            try
            {
                Bl?.Order.UpdateShipping(id);
                MessageBox.Show("updated succesfuly!");
                new ListWindow(Bl, cart,this).Show();
                this.Hide();
            }
            catch (BlCannotChangeTheStatusException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// button that returns to home
        /// </summary>
        private void BackToHome(object sender, RoutedEventArgs e)
        {

           lastWindow.Show();
            this.Hide();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProductName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Price_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AmountProduct_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OrderId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OrdersListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
