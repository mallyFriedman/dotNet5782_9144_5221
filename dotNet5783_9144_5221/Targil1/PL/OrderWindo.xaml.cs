using BlApi;
using BO;
//using Microsoft.VisualBasic;
using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderWindo.xaml
    /// </summary>
    public partial class OrderWindo : Window
    {
        private OrderForList p;

        private BlApi.IBl Bl { get; set; }


        /// <summary>
        /// constractor of the page
        /// </summary>
        public OrderWindo(OrderForList p = null)
        {
            this.p = p;
            Bl = BlApi.Factory.Get();
            InitializeComponent();
            if (p != null)
            {
                BO.Order a = Bl.Order.Get(p.Id);
                OrderId.Text = (p.Id).ToString();
                //CostumerName.Text = p.CostumerName;
              //  OrderDate.Text = p.OrderDate;//.ToString();
                //OrderStatus.Text = p.OrderStatus;//.ToString();
                TotalPrice.Text = p.TotalPrice.ToString();
                //ShipDate.Text = a.ShipDate;
                //DeliveryDate.Text = a.DeliveryDate;
                //CustomerAdress.Text = (a.CustomerAdress).ToString();
            }
        }



        /// <summary>
        /// the function updates the chosen product in the list
        /// </summary>
        private void UpdateSupply1(object sender, RoutedEventArgs e)
        {
           // try
            //{
                BO.OrderForList p = new();
                Bl.Order.UpdateSupply(p.Id);
                MessageBox.Show("updated succesfuly!");
                new Window2().Show();
                this.Hide();
            //}
           //catch (BlObjectNotValidException ex)
           //{
           //    MessageBox.Show(ex.Message);
           //}
           //catch (BlPriceMustBeANumber ex)
           //{
           //    MessageBox.Show(ex.Message);
           //}
           //catch (BlInStockMustBeANumber ex)
           //{
           //    MessageBox.Show(ex.Message);
           //}
        }
        private void UpdateShipping1(object sender, RoutedEventArgs e)
        {
            //status(DateTime ? DeliveryDate, DateTime ? MinValue, DateTime ? ShipDate)
            //UpdateShipping(int id)
            //UpdateSupply
            //try
            //{
                BO.OrderForList p = new();
                Bl.Order.UpdateSupply(p.Id);
                MessageBox.Show("updated succesfuly!");
                new Window2().Show();
                this.Hide();
            //}
            //catch (BlObjectNotValidException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //catch (BlPriceMustBeANumber ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //catch (BlInStockMustBeANumber ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        private void status1(object sender, RoutedEventArgs e)
        {
            //status(DateTime ? DeliveryDate, DateTime ? MinValue, DateTime ? ShipDate)
            //try
            //{
                BO.OrderForList p = new();
              //  Bl.Order.UpdateSupply(p.DeliveryDate, p.MinValue,p.ShipDate);
                MessageBox.Show("updated succesfuly!");
                new Window2().Show();
                this.Hide();
            //}
            //catch (BlObjectNotValidException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //catch (BlPriceMustBeANumber ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //catch (BlInStockMustBeANumber ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }


        /// <summary>
        /// button that returns to home
        /// </summary>
        private void BackToHome(object sender, RoutedEventArgs e)
        {

            new Window2().Show();
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
    }
}
