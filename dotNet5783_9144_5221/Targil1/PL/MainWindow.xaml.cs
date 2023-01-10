using BlApi;
using BlImplementation;
using BO;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BlApi.IBl Bl { get; set; }
        /// <summary>
        /// constructor of the page
        /// </summary>
        public MainWindow()
        {
            this.Bl = BlApi.Factory.Get();
            InitializeComponent();
        }
        /// <summary>
        /// routs to the home page
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Window2(Bl).Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new productItem(Bl).Show();
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //if (track.Text== )
            //{
            //    throw new System.Exception("Error");
            //}
            try
            {
               // int? i = ;
                Bl.Order.Get(int.Parse(track.Text ==""? "0": track.Text));
                new OrderWindo(Bl, int.Parse(track.Text), false).Show();
                this.Hide();
            }
            catch (BlIdNotValidException ex)
            {
                MessageBox.Show(ex.Message);    
            }
           //catch (BlIdNotValidException ex)
           //{
           //    MessageBox.Show(ex.Message);
           //}
        }

    private void track_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {

    }
}
}
