using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;
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
    public partial class ProductWindow : Window
    {
        private ProductForList? p;
        private ProductItem? q;
        private BO.Cart cart = new();
        private BlApi.IBl Bl;
        Window lastWindow;
        private bool libby { get; set; }
        private bool flagProductForList { get; set; }
        private bool flagAdd { get; set; }
        Tuple<BO.Product, bool,bool,bool> dct;
        Tuple<BO.ProductItem, bool,bool,bool> dct1;

        /// <summary>
        /// constractor of the page
        /// </summary>
        public ProductWindow(BlApi.IBl bl, BO.Cart cart,Window lWindow, ProductForList? p = null, ProductItem? q = null)
        {
            lastWindow=lWindow;
            libby = false;
            flagProductForList = false;
            flagAdd = false;
            this.Bl = bl;
            this.p = p;
            this.q = q;
            this.cart = cart;
            InitializeComponent();
            CategorySelector.ItemsSource = Bl.Product.GetAllForCustomer();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));

            //
            if (p != null)
            {
                BO.Product a = Bl.Product.GetManager(p.Id);
                this.flagProductForList = true;
                flagAdd = false;
                libby = false;
        dct = new Tuple<Product, bool,bool,bool>(a, flagProductForList, flagAdd,libby);
                DataContext = dct;
                CategorySelector.SelectedItem = p.Category;
            }
            else if (q != null)
            {

                flagProductForList = false;
                flagAdd = false;
                libby = true;
                dct1 = new Tuple<ProductItem, bool, bool, bool>(q, flagProductForList, flagAdd, libby);
                DataContext = dct1;
            }
            else
            {
                this.flagProductForList = false;
                flagAdd = true;
                libby = false;
                dct1 = new Tuple<ProductItem, bool, bool, bool>(q, flagProductForList, flagAdd, libby);
                DataContext = dct1;
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
                new ListWindow(Bl, cart, this   ).Show();
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
                new ListWindow(Bl, cart, this).Show();
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
                if(Price.Text==""|| InStock.Text=="")
                {
                    throw new NullValueException();
                }
                else
                {
                    p.Price = float.Parse(Price.Text  );
                    p.InStock = int.Parse(InStock.Text );
                }
                p.Id = 0;
                p.Category = (BO.Category)CategorySelector.SelectedItem;
                Bl.Product.Add(p);
                MessageBox.Show("Add succesfuly!");
                new ListWindow(Bl, cart, this).Show();
                this.Hide();

            }
            catch (NullValueException ex)
            {
                MessageBox.Show(ex.Message);
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

            lastWindow.Show();
            this.Close();
        }


        private void addToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.cart = Bl.Cart.Add(cart, q.Id);
                MessageBox.Show("add successfully");
                new productItem(Bl, cart,this).Show();
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
