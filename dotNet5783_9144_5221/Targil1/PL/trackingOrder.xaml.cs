﻿using BlApi;
using BO;
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
    /// Interaction logic for trackingOrder.xaml
    /// </summary>
    public partial class trackingOrder : Window
    {
        private BlApi.IBl Bl;
        public trackingOrder(BlApi.IBl bl,int id)
        {
            this.Bl = bl; 
            InitializeComponent();
            BO.Order order =Bl.Order.Get(id);
            DataContext = order;
        }
    }
}
