using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Dal;

internal class OrderItem : IOrderItem
{
    public int Add(DO.OrderItem item)
    {
        XElement? root = XDocument.Load("../../xml/OrderItemXml.xml").Root;
        XElement? config = XDocument.Load("../../xml/config.xml").Root;
        item.Id = Convert.ToInt32(config?.Elements("productId")?.FirstOrDefault()?.Value);
        XElement el2 = new("OrderItem",
             new XElement("Id", item.Id),
             new XElement("ProductID", item.ProductID),
             new XElement("Price", item.Price),
             new XElement("OrderID", item.OrderID),
             new XElement("Amount", item.Amount));
        root?.Add(el2);
        root?.Save("../../xml/OrderItemXml.xml");
        config?.Element("orderItemId")?.SetValue(Convert.ToInt32(config?.Elements("orderItemId")?.FirstOrDefault()?.Value) + 1);
        config?.Save("../../xml/config.xml");
        return item.Id;
    }



    private DO.OrderItem Casting(XElement item)
    {
        DO.OrderItem oi = new();
        oi.Id = Convert.ToInt32(item.Element("Id").Value);
        oi.Price = Convert.ToInt32(item.Element("Price").Value);
        oi.ProductID = Convert.ToInt32(item.Element("ProductID").Value);
        oi.OrderID = Convert.ToInt32(item.Element("OrderID").Value);
        oi.OrderID = Convert.ToInt32(item.Element("OrderID").Value);
        oi.Amount = Convert.ToInt32(item.Element("Amount").Value);
        return oi;
    }

    public void Delete(int id)
    {
        XElement? root = XDocument.Load("../../xml/OrderItemXml.xml").Root;
        XElement? orderItem = root?.Element("ArrayOfOrderItem")?.Elements("OrderItem")?.
                              Where(o => o.Element("Id")?.Value == id.ToString()).FirstOrDefault();
        orderItem?.Remove();
        root?.Save("..\\..\\..\\OrderItemXml.xml");
    }

    public DO.OrderItem Get(int id)
    {
        XElement? root = XDocument.Load("../../xml/OrderItemXml.xml").Root;
        XElement? oi = root?.Element("ArrayOfOrderItem")?.Elements("OrderItem")?.
                             Where(o => o.Element("Id")?.Value == id.ToString()).FirstOrDefault();
        DO.OrderItem orderItem = Casting(oi);
        return orderItem;
    }

    public IEnumerable<DO.OrderItem>? Get(Func<DO.OrderItem, bool>? foo = null)
    {
        XElement? root = XDocument.Load("../../xml/OrderItemXml.xml").Root;
        IEnumerable<XElement> ListXElement = root?.Descendants("OrderItem") ?? throw new Exception("error in file type");
        var orderItem = from item in ListXElement
                       select Casting(item);
        return foo == null ? orderItem : orderItem.Where(foo).ToList();///
    }

    public DO.OrderItem GetSingle(Func<DO.OrderItem, bool>? foo)
    {
        XElement? root = XDocument.Load("../../xml/OrderItemXml.xml").Root;
        IEnumerable<XElement> ListXElement = root?.Descendants("OrderItem") ?? throw new Exception("error in file type");
        var orderItem = from item in ListXElement
                        select Casting(item);
        return orderItem.Where(foo).ToList()[0];
    }

  

    public void Update(DO.OrderItem item)
    {
        XElement? root = XDocument.Load("../../xml/ProductXml.xml").Root;
        XElement? product = root?.Element("ArrayOfProduct")?.Elements("Product")?.
                              Where(p => p.Element("Id")?.Value == item.Id.ToString()).FirstOrDefault();
        product?.Remove();
        product?.Add(item);
        root?.Save("..\\..\\..\\OrderItemXml.xml");
    }
}
