using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for trackingOrder.xaml
    /// </summary>
    public partial class trackingOrder : Window
    {
        private BlApi.IBl? Bl;
        private BO.Cart? cart = new();
        private int id;
        private Window lastWindow;
        public trackingOrder(BlApi.IBl bl, BO.Cart cart,Window window,int id)
        {
            this.lastWindow = window;
            this.cart = cart;
            this.Bl = bl; 
            this.id = id;
            InitializeComponent();
            BO.OrderTracking? order =Bl.Order.GetOrderTracking(id);
            DataContext = order;
            statusList.DataContext = new { mylist = new ObservableCollection<Tuple<DateTime, BO.eOrderStatus>>(order.StatusList) };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new OrderWindow(Bl, cart, this,id, false).Show();
                this.Hide();
            }
            catch (BlIdNotValidException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           lastWindow.Show();
            this.Hide();
        }
    }
}
