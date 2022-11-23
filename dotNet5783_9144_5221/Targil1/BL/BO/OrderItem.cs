namespace BO
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
        order item ID={Id}, 
        product ID={ProductID},
        product name={ProductName},
    	price: {Price},
        amount: {Amount},
        total price: {TotalPrice}";
    }
}
