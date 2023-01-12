using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Dal;

internal class Order : IOrder
{

    public int Add(DO.Order item)
    {
        XElement? root = XDocument.Load("../../xml/OrderXml.xml").Root;
        XElement? config = XDocument.Load("../../xml/config.xml").Root;
        item.Id = Convert.ToInt32(config?.Elements("orderId")?.FirstOrDefault()?.Value);
        XElement el2 = new("Order",
             new XElement("Id", item.Id),
             new XElement("CustomerName", item.CustomerName),
             new XElement("CustomerEmail", item.CustomerEmail),
             new XElement("CustomerAdress", item.CustomerAdress),
             new XElement("ShipDate", item.ShipDate),
             new XElement("OrderDate", item.OrderDate),
             new XElement("DeliveryDate", item.DeliveryDate));
        root?.Add(el2);
        root?.Save("../../xml/OrderXml.xml");
        config?.Element("orderId")?.SetValue(Convert.ToInt32(config?.Elements("orderId")?.FirstOrDefault()?.Value) + 1);
        config?.Save("../../xml/config.xml");
        return item.Id;
    }



    public void Delete(int id)
    {
        XElement? root = XDocument.Load("../../xml/OrderXml.xml").Root;
        XElement? order = root?.Element("ArrayOfProduct")?.Elements("Order")?.
                              Where(o => o.Element("Id")?.Value == id.ToString()).FirstOrDefault();
        order?.Remove();
        root?.Save("..\\..\\..\\OrderXml.xml");
    }

    private DO.Order Casting(XElement item)
    {
        DO.Order o = new();
        o.Id = Convert.ToInt32(item.Element("Id").Value);
        o.CustomerName = Convert.ToString(item.Element("CustomerName").Value);
        o.CustomerEmail = Convert.ToString(item.Element("CustomerEmail").Value);
        o.CustomerAdress = Convert.ToString(item.Element("CustomerAdress").Value);
        o.ShipDate = Convert.ToDateTime(item.Element("ShipDate").Value);
        o.OrderDate = Convert.ToDateTime(item.Element("OrderDate").Value);
        o.DeliveryDate = Convert.ToDateTime(item.Element("DeliveryDate").Value);
        return o;
    }

    public DO.Order Get(int id)
    {
        XElement? root = XDocument.Load("../../xml/OrderXml.xml").Root;
        XElement? order = root?.Descendants("Order")?.
                             Where(o => o.Element("Id")?.Value == id.ToString()).FirstOrDefault();
        DO.Order order1 = Casting(order);
        return order1;
    }

    public IEnumerable<DO.Order>? Get(Func<DO.Order, bool>? foo = null)
    {
        XElement? root = XDocument.Load("../../xml/OrderXml.xml").Root;
        List<XElement> ListXElement = root.Descendants("Order").ToList();
        var orders = (from item in ListXElement
                      select new DO.Order
                      {
                          Id = Convert.ToInt32(item.Element("Id").Value),
                          CustomerName = Convert.ToString(item.Element("CustomerName").Value),
                          CustomerEmail = Convert.ToString(item.Element("CustomerEmail").Value),
                          CustomerAdress = Convert.ToString(item.Element("CustomerAdress").Value),
                          ShipDate = Convert.ToDateTime(item.Element("ShipDate").Value),
                          OrderDate = Convert.ToDateTime(item.Element("OrderDate").Value),
                          DeliveryDate = Convert.ToDateTime(item.Element("DeliveryDate").Value)
                      });
        return foo == null ? orders : orders.Where(foo).ToList();
}

public DO.Order GetSingle(Func<DO.Order, bool>? foo)
{

    XElement? root = XDocument.Load("../../xml/OrderXml.xml").Root;
    IEnumerable<XElement> ListXElement = root.Descendants("Order").ToList();
    var orders = (from item in ListXElement
                  select Casting(item));
    return orders.Where(foo).FirstOrDefault();
}

public void Update(DO.Order item)
{
    XElement? root = XDocument.Load("../../xml/OrderXml.xml").Root;
        List<XElement> ListXElement = root.Descendants("Order").ToList();
        XElement? order = ListXElement?.
                          Where(o => o.Element("Id")?.Value == item.Id.ToString()).FirstOrDefault();
        ListXElement?.Remove(order);
        order.Remove();
        order?.Add(item);
      //  ListXElement.Add(order);
    root?.Save("..\\..\\..\\OrderXml.xml");
}
}