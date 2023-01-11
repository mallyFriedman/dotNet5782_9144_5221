using BlApi;
using BlImplementation;
using BO;
using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//using static System.Net.Mime.MediaTypeNames;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private ProductForList p;
        private ProductItem q;
        private BO.Cart cart = new();
        private BlApi.IBl Bl;


        /// <summary>
        /// constractor of the page
        /// </summary>
        public Window1(BlApi.IBl bl, BO.Cart cart, ProductForList p = null,ProductItem q=null)
        {
            this.Bl = bl;    
            this.p = p;
            this.q = q;
            this.cart = cart;
            InitializeComponent();
            CategorySelector.ItemsSource = Bl.Product.GetAllForCustomer();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            if (p != null)
            {
                //DataContext = p;
                BO.Product a = Bl.Product.GetManager(p.Id);
                DataContext = a;
                //Id.Text = (p.Id).ToString();              //binding
                //ProductName.Text = p.ProductName;         //binding
                //Price.Text = (p.ProductPrice).ToString(); //binding
                //InStock.Text = (a.InStock).ToString();    //binding
                CategorySelector.SelectedItem = p.Category;
                add.Visibility = Visibility.Hidden;
                addToCart.Visibility = Visibility.Hidden;
                category.Visibility = Visibility.Hidden;

            }
            else if(q != null)  
            {
                DataContext = q;
                add.Visibility = Visibility.Hidden;
                update.Visibility = Visibility.Hidden;
                delete.Visibility = Visibility.Hidden;
                CategorySelector.Visibility = Visibility.Hidden;
                category.Visibility = Visibility.Visible;
            }
            else
            {
                update.Visibility = Visibility.Hidden;
                delete.Visibility = Visibility.Hidden;
                addToCart.Visibility = Visibility.Hidden;
                category.Visibility = Visibility.Hidden;
            }
        }



        /// <summary>
        /// the function updates the chosen product in the list
        /// </summary>
        private void Update(object sender, RoutedEventArgs e)
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
                new Window2(Bl, cart).Show();
                this.Hide();
            }
            catch (BlObjectNotValidException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BlPriceMustBeANumber ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BlInStockMustBeANumber ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// the function deletes the chosen product from the list
        /// </summary>
        private void Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                Bl.Product.Delete(p.Id);
                MessageBox.Show("deleted succesfuly!");
                new Window2(Bl, cart).Show();
                this.Hide();
            }
            
            catch (BlIdNotValidException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// the function adds a product to the list
        /// </summary>
        private void Add(object sender, RoutedEventArgs e)
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
                new Window2(Bl, cart).Show();
                this.Hide();
                Id.Text = null;
                ProductName.Text = null;
                Price.Text = null;
                InStock.Text = null;
                CategorySelector.SelectedItem = null;

            }
            catch (BlObjectNotValidException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BlPriceMustBeANumber ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BlInStockMustBeANumber ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// button that returns to home
        /// </summary>
        private void BackToHome(object sender, RoutedEventArgs e)
        {

            new Window2(Bl, cart).Show();
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

        private void Id_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void addToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               this.cart= Bl.Cart.Add(cart, q.Id);
                MessageBox.Show("add successfully");
                new productItem(Bl, cart).Show();
                this.Hide();
            }
            catch (BlOutOfStockException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BlObjectNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void category_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Id_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
