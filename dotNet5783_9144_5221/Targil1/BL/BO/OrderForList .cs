namespace BO
{
    public class OrderForList
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public eOrderStatus OrderStatus { get; set; }///enum
        public int AmountProduct { get; set; }
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
        ID={Id}, 
        CustomerName={CustomerName}, 
    	OrderStatus: {OrderStatus},
    	AmountProduct: {AmountProduct},
        TotalPrice: {TotalPrice}";
    }
}
