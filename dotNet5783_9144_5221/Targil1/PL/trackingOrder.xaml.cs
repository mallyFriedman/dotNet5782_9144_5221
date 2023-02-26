using BlApi;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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
