using BO;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductItem.xaml
    /// </summary>
    public partial class productItem : Window
    {
        private ProductItem? q;
        private BlApi.IBl Bl;
        private BO.Cart cart = new();
        private Window lastWindow;
        Tuple<IEnumerable<BO.ProductItem>, Array> dct;
        public productItem(BlApi.IBl bl, BO.Cart cart, Window lastWindow)
        {
            this.lastWindow= lastWindow;
            this.Bl = bl;
            this.cart = cart;
           
            dct = new Tuple<IEnumerable<BO.ProductItem>, Array>(Bl.Product.GetAllForManager(), Enum.GetValues(typeof(BO.Category)));
            InitializeComponent();
            DataContext = dct;
            this.lastWindow = lastWindow;   
        }
        private void product_Click(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem q = (BO.ProductItem)((ListView)sender).SelectedItem;
            new ProductWindow(Bl, cart,this, null,q).Show();
            this.Hide();
        }
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dct = new Tuple<IEnumerable<BO.ProductItem>, Array>(Bl.Product.GetAllForManager((BO.Category)CategorySelector.SelectedItem), Enum.GetValues(typeof(BO.Category)));
            DataContext = dct;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Cart(Bl, cart,this).Show();
            this.Hide();
        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lastWindow.Show();
            this.Hide();
        }
    }
}
