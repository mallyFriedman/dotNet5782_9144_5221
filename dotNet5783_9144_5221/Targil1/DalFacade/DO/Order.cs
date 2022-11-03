using DalFacade;

namespace DalFacade;
    public struct Order
    {
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public DateTime ShipDate { get; set; }

    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public override string ToString() => $@"
        Product ID={ID}, 
        Product CustomerName={CustomerName}, 
    	CustomerEmail: {CustomerEmail},
    	CustomerAdress: {CustomerAdress},
        ShipDate: {ShipDate},
        OrderDate: {OrderDate},
        DeliveryDate: {DeliveryDate}";
}
