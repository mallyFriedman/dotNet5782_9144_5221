using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    internal class OrderForList
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public eOrderStatus OrderStatus { get; set; }///enum
        public int AmountProduct { get; set; }
        public double TotalPrice { get; set; }

    }
}
