using BlApi;
using BlImplementation;
using BO;
using System.Collections.ObjectModel;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 



    public partial class MainWindow : Window
    {
        private BO.Cart cart = new();
        private BlApi.IBl Bl { get; set; }
      
        /// <summary>
        /// constructor of the page
        /// </summary>
        public MainWindow()
        {
            this.Bl = BlApi.Factory.Get();
            InitializeComponent();
            //this.cart =cart??null;
        }
        /// <summary>
        /// routs to the home page
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ListWindow(Bl, cart,this).Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new productItem(Bl, cart,this).Show();
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                Bl.Order.Get(int.Parse(track.Text ==""? "0": track.Text));
                new trackingOrder(Bl,cart,this, int.Parse(track.Text)).Show();
                this.Hide();
            }
            catch (BlIdNotValidException ex)
            {
                MessageBox.Show(ex.Message);    
            }
            catch (BlObjectNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    private void track_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {

    }
}
}
