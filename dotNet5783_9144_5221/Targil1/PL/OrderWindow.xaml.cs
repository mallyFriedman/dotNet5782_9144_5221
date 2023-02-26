using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
        private bool adminFlag { get; set; }
        /// <summary>
        /// constractor of the page
        /// </summary>
        public OrderWindow(BlApi.IBl bl, BO.Cart cart, Window window, int id = 0, bool admin = false)
        {
            try
            {
                this.lastWindow = window;
                this.cart = cart;
                this.id = id;
                this.Bl = bl;
                int sum = 0;
                adminFlag = true;
                Tuple<BO.Order, bool, int, IEnumerable<OrderItem>> dct;
                InitializeComponent();
                BO.Order order = new Order();
                if (!admin)
                    adminFlag = false;
                if (id != 0)
                {
                    order = Bl.Order.Get(id);
                    order?.OrderItem?.Select(ord =>
                    {
                        sum = sum + 1;
                        return ord;
                    }).ToList();
                    dct = new Tuple<BO.Order, bool, int, IEnumerable<OrderItem>>(order, adminFlag, sum, order?.OrderItem);
                    DataContext = dct;
                }
            }
            catch (BlIdNotValidException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BlObjectNotFoundException ex) 
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex) 
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
                new ListWindow(Bl, cart, this).Show();
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
                new ListWindow(Bl, cart, this).Show();
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
