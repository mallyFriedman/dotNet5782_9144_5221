using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderTracking
    {
        public int Id { get; set; }
        public eOrderStatus OrderStatus { get; set; }
        public List<Tuple<DateTime, eOrderStatus>>? StatusList;
    }
}
