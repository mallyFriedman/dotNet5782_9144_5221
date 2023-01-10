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
        private ProductItem q;
        private BlApi.IBl Bl;
        public productItem(BlApi.IBl bl)
        {
            this.Bl = bl;
            InitializeComponent();
            ProductsListview.ItemsSource = Bl.Product.GetAllForManager();
        }
        private void product_Click(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem q = (BO.ProductItem)((ListView)sender).SelectedItem;
            new Window1(Bl,null,q).Show();
            this.Hide();
        }
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductsListview.ItemsSource = Bl.Product.GetAllForManager((BO.Category)CategorySelector.SelectedItem);
            
        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
