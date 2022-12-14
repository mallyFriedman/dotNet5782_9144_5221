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
            InitializeComponent();
        }
        /// <summary>
        /// routs to the home page
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Window2().Show();
            this.Hide();
        }
    }
}
