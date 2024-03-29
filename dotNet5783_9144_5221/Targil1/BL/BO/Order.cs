﻿namespace BO
{
    public class Order
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerAdress { get; set; }
        public eOrderStatus OrderStatus { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public IEnumerable<OrderItem?>? OrderItem { get; set; }
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
        Order ID={Id}, 
        Order CustomerName={CustomerName}, 
    	CustomerEmail: {CustomerEmail},
    	CustomerAdress: {CustomerAdress},
        OrderDate: {OrderDate},
        ShipDate: {ShipDate},
        DeliveryDate: {DeliveryDate}";
    }
}
