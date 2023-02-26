using BlApi;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PL
{

    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>

    public partial class Cart : Window
    {
        private BlApi.IBl? Bl;
        private BO.Cart? cart = new();
        private Window window;
       // Tuple<BO.Cart?, ObservableCollection<BO.OrderItem>> dct;
        private ObservableCollection<BO.OrderItem> cl { get; set; }
        public Cart(BlApi.IBl bl, BO.Cart? cart, Window window)
        {

            this.window = window;
            this.Bl = bl;
            InitializeComponent();
            this.cart = cart;

           cl = cart?.Items == null ? new() : new(cart.Items);
            //dct = new Tuple<BO.Cart?, ObservableCollection<BO.OrderItem>>(cart, cl);
            //DataContext = dct;
            DataContext = cart;
            ProductsItemListview.DataContext = cl;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CustomerName.Text == "" || CustomerEmail.Text == "" || CustomerAdress.Text == "")
                {
                    throw new NullValueException();
                }
                Bl.Cart.Confirm(cart, CustomerName.Text, CustomerEmail.Text, CustomerAdress.Text);
                MessageBox.Show("confirmed");
                new MainWindow().Show();
                this.Close();
            }
            catch (NullValueException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BlOutOfStockException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            window.Show();
            this.Hide();
        }

        private void deleteProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                DataContext = null;
                BO.OrderItem p = (BO.OrderItem)((Button)sender).DataContext;
                this.cart = Bl.Cart.Update(cart, p.ProductID, 0);
                cl.Remove(p);
                //dct = new Tuple<BO.Cart?, ObservableCollection<BO.OrderItem>>(cart, cl);
                //DataContext = dct;
                DataContext = cart;
            }
            catch (BlOutOfStockException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BlObjectNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BlCartIsEmptyException ex)
            {
                MessageBox.Show(ex.Message);
            }


            //this.Hide();
            //new Cart(Bl, cart).Show();
        }

        private void decreaseProductBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataContext = null;
                BO.OrderItem p = (BO.OrderItem)((Button)sender).DataContext;
                this.cart = Bl.Cart.Update(cart, p.ProductID, p.Amount - 1);
                cl = new();
                cart?.Items?.Select(item =>
                {
                    cl.Add(item ?? throw new BlObjectNotFoundException());
                    return item;
                }).ToList();

                //dct = new Tuple<BO.Cart?, ObservableCollection<BO.OrderItem>>(cart, cl);
                //DataContext = dct;
                 ProductsItemListview.DataContext = cl;
                 DataContext = cart;
            }
            catch (BlOutOfStockException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BlObjectNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BlCartIsEmptyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void addProductAmountBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataContext = null;
                BO.OrderItem p = (BO.OrderItem)((Button)sender).DataContext;
                this.cart = Bl.Cart.Update(cart, p.ProductID, p.Amount + 1);
                cl = new();
                cart?.Items?.Select(item =>
                {
                    cl.Add(item);
                    return item;
                }).ToList();
                ProductsItemListview.DataContext = cl;
                DataContext = cart;
            }
            catch (BlOutOfStockException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BlObjectNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BlCartIsEmptyException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProductsItemListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProductsItemListview_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CustomerName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
    //our problems:
    //when the function goes out of the cart to the main windo- the cart refreshed-! it is a big problem!
    //binding of the pages!
}
