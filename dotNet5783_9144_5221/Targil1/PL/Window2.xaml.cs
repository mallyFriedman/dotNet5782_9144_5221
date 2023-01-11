﻿using BlApi;
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
    public partial class Window2 : Window
    {
        private BlApi.IBl Bl;
        private BO.Cart cart = new();
        /// <summary>
        /// constructor of the page
        /// </summary>
        public Window2(BlApi.IBl bl, BO.Cart cart)
        {
            try
            {
                this.Bl = bl;
                this.cart = cart;
                InitializeComponent();
                ProductsListview.ItemsSource = Bl.Product.GetAllForCustomer();
                CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
                OrdersListview.ItemsSource = Bl.Order.GetAll();
            }
            catch (BlIdNotValidException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Setting the selection menu
        /// </summary>
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductsListview.ItemsSource = Bl.Product.GetAllForCustomer((BO.Category)CategorySelector.SelectedItem);
        }

        /// <summary>
        /// the function routs to the add page
        /// </summary>
        private void GoToAdd(object sender, RoutedEventArgs e)
        {
            new Window1(Bl, cart).Show();
            this.Hide();
        }
        /// <summary>
        /// the function routs to the update/delete page
        /// </summary>
        private void product_Click(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList p = (BO.ProductForList)((ListView)sender).SelectedItem;
            new Window1(Bl, cart, p).Show();
            this.Hide();

        }

        private void order_Click(object sender, MouseButtonEventArgs e)
        {
            BO.OrderForList p = (BO.OrderForList)((ListView)sender).SelectedItem;
            new OrderWindo(Bl, cart, p.Id, true).Show();     /////////
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
            new MainWindow().Show();
            this.Hide();
        }
    }
}
