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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private BlApi.IBl Bl { get; set; }

        
           
            public Window1()
        {
            Bl = new BlImplementation.Bl();
            InitializeComponent();
            CategorySelector.ItemsSource = Bl.Product.GetAllForCustomer();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Product p = new();
                p.ProductName = ProductName.Text;
                p.Price = float.Parse(Price.Text);
                p.InStock = int.Parse(InStock.Text);
                p.Id = 0;
                p.Category = (BO.Category)CategorySelector.SelectedItem;
                Bl.Product.Add(p);
                MessageBox.Show("Add succesfuly!");
            }
            catch
            {
                throw new Exception();//do spesific exption!
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProductName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new Window2().Show();
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Product p = new();
                p.ProductName = ProductName.Text;
                p.Id = int.Parse(Id.Text);
                p.Price = float.Parse(Price.Text);
                p.InStock = int.Parse(InStock.Text);
                p.Category = (BO.Category)CategorySelector.SelectedItem;
                Bl.Product.Update(p);
                MessageBox.Show("updated succesfuly!");
            }
            catch
            {
                throw new Exception();//do spesific exption!
            }
        }
    }
}
