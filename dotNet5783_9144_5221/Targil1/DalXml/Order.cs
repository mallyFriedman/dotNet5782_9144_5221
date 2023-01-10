using DalApi;
using DO;
using System.Linq;
using System.Xml.Linq;

namespace Dal;

internal class Order : IOrder
{
    public int Add(DO.Order item)
    {
        XElement? root = XDocument.Load("OrderXml.xml").Root;
        XElement el2 = new("Order",
             new XAttribute("Id", item.Id),
             new XAttribute("CustomerName", item.CustomerName),
             new XAttribute("CustomerEmail", item.CustomerEmail),
             new XAttribute("CustomerAdress", item.CustomerAdress),
             new XAttribute("ShipDate", item.ShipDate),
             new XAttribute("OrderDate", item.OrderDate),
             new XAttribute("DeliveryDate", item.DeliveryDate));
        root?.Element("ArrayOfOrder")?.Add(el2);
        root?.Save("..\\..\\..\\OrderXml.xml");
        return item.Id;
    }

  

    public void Delete(int id)
    {
        XElement? root = XDocument.Load("OrderXml.xml").Root;
        XElement? order = root?.Element("ArrayOfProduct")?.Elements("Order")?.
                              Where(o => o.Attribute("Id")?.Value == id.ToString()).FirstOrDefault();
        order?.Remove();
        root?.Save("..\\..\\..\\OrderXml.xml");
    }

    private DO.Order Casting(XElement item)
    {
        DO.Order o = new();
        o.Id = Convert.ToInt32(item.Attribute("Id"));
        o.CustomerName = Convert.ToString(item.Attribute("CustomerName"));
        o.CustomerEmail = Convert.ToString(item.Attribute("CustomerEmail"));
        o.CustomerAdress = Convert.ToString(item.Attribute("CustomerAdress"));
        o.ShipDate = Convert.ToDateTime(item.Attribute("ShipDate"));
        o.OrderDate = Convert.ToDateTime(item.Attribute("OrderDate"));
        o.DeliveryDate = Convert.ToDateTime(item.Attribute("DeliveryDate"));
        return o;
    }

    public DO.Order Get(int id)
    {
        XElement? root = XDocument.Load("OrderXml.xml").Root;
        XElement? order = root?.Element("ArrayOfOrder")?.Elements("Order")?.
                             Where(o => o.Attribute("Id")?.Value == id.ToString()).FirstOrDefault();
        DO.Order order1 = Casting(order);
        return order1;
    }

    public IEnumerable<DO.Order>? Get(Func<DO.Order, bool>? foo = null)
    {
        XElement? root = XDocument.Load("OrderXml.xml").Root;
        IEnumerable<XElement> ListXElement = root?.Element("ArrayOfOrder")?.Elements("Order")??null;/////
        var orders = (from item in ListXElement
                          select Casting(item));
        return foo == null ? orders : orders.Where(foo).ToList();
    }

    public DO.Order GetSingle(Func<DO.Order, bool>? foo)
    {

        XElement? root = XDocument.Load("OrderXml.xml").Root;
        IEnumerable<XElement> ListXElement = root?.Element("ArrayOfOrder")?.Elements("Order");
        var orders = (from item in ListXElement
                          select Casting(item));
        return orders.Where(foo).ToList()[0];
    }

    public void Update(DO.Order item)
    {
        XElement? root = XDocument.Load("OrderXml.xml").Root;
        XElement? order = root?.Element("ArrayOfOrder")?.Elements("Order")?.
                              Where(o => o.Attribute("Id")?.Value == item.Id.ToString()).FirstOrDefault();
        order?.Remove();
        order?.Add(item);
        root?.Save("..\\..\\..\\OrderXml.xml");
    }
}