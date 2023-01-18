using BlApi;
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
    /// Interaction logic for ProductItem.xaml
    /// </summary>
    public partial class productItem : Window
    {
        private ProductItem? q;
        private BlApi.IBl Bl;
        private BO.Cart cart = new();
        private Window lastWindow;
        public productItem(BlApi.IBl bl, BO.Cart cart, Window lastWindow)
        {
            this.lastWindow= lastWindow;
            this.Bl = bl;
            this.cart = cart;
            InitializeComponent();
            ProductsListview.ItemsSource = Bl.Product.GetAllForManager();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
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
            ProductsListview.ItemsSource = Bl.Product.GetAllForManager((BO.Category)CategorySelector.SelectedItem);

            
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
