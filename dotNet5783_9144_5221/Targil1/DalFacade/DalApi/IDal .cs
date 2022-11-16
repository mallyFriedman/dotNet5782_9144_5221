namespace DalApi
{
    internal interface IDal
    {
        public IOrder Order { get; }
        public IProduct Product { get; }
        public IOrderItem OrderItem { get; }
    }
}
