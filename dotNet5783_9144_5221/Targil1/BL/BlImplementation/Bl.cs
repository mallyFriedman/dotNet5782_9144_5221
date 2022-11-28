using BlApi;
namespace BlImplementation
    
{
    public class Bl:IBl
    {
        public IOrder Order => new BLOrder();
        public IProduct Product => new BLProduct();
        public ICart Cart => new BLCart();

    }
}
