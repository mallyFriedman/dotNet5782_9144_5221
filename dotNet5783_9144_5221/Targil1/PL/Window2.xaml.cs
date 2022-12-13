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
        private BlApi.IBl Bl { get; set; }

        public Window2()
        {
            Bl = new BlImplementation.Bl();
            InitializeComponent();
            ProductsListview.ItemsSource = Bl.Product.GetAllForCustomer();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductsListview.ItemsSource = Bl.Product.GetAllForCustomer((BO.Category)CategorySelector.SelectedItem);
            CategorySelector.SelectedItem = "jjj";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Window1().Show();
            this.Hide();
        }
           
        

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void product_Click(object sender, MouseButtonEventArgs e)
        {
             BO.ProductForList p = (BO.ProductForList)((ListView)sender).SelectedItem;
            new Window1(p).Show();
            this.Hide();

        }
    }
}
