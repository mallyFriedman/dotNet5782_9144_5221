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
        XElement? root = XDocument.Load("OrderItemXml.xml").Root;
        XElement el2 = new("OrderItem",
             new XAttribute("Id", item.Id),
             new XAttribute("ProductID", item.ProductID),
             new XAttribute("Price", item.Price),
             new XAttribute("OrderID", item.OrderID),
             new XAttribute("Amount", item.Amount));
        root?.Element("ArrayOfOrderItem")?.Add(el2);
        root?.Save("..\\..\\..\\OrderItemXml.xml");
        return item.Id;
    }
    //public int Id { get; set; }
    //public int ProductID { get; set; }
    //public int OrderID { get; set; }
    //public double Price { get; set; }
    //public int Amount { get; set; }
    private DO.OrderItem Casting(XElement item)
    {
        DO.OrderItem oi = new();
        oi.Id = Convert.ToInt32(item.Attribute("Id"));
        oi.Price = Convert.ToInt32(item.Attribute("Price"));
        oi.ProductID = Convert.ToInt32(item.Attribute("ProductID"));
        oi.OrderID = Convert.ToInt32(item.Attribute("OrderID"));
        oi.OrderID = Convert.ToInt32(item.Attribute("OrderID"));
        oi.Amount = Convert.ToInt32(item.Attribute("Amount"));
        return oi;
    }

    public void Delete(int id)
    {
        XElement? root = XDocument.Load("OrderItemXml.xml").Root;
        XElement? orderItem = root?.Element("ArrayOfOrderItem")?.Elements("OrderItem")?.
                              Where(o => o.Attribute("Id")?.Value == id.ToString()).FirstOrDefault();
        orderItem?.Remove();
        root?.Save("..\\..\\..\\OrderItemXml.xml");
    }

    public DO.OrderItem Get(int id)
    {
        XElement? root = XDocument.Load("OrderItemXml.xml").Root;
        XElement? oi = root?.Element("ArrayOfOrderItem")?.Elements("OrderItem")?.
                             Where(o => o.Attribute("Id")?.Value == id.ToString()).FirstOrDefault();
        DO.OrderItem orderItem = Casting(oi);
        return orderItem;
    }

    public IEnumerable<DO.OrderItem>? Get(Func<DO.OrderItem, bool>? foo = null)
    {
        XElement? root = XDocument.Load("OrderItemXml.xml").Root;
        IEnumerable<XElement> ListXElement = root?.Element("ArrayOfOrderItem")?.Elements("OrderItem");
        List<DO.OrderItem> orderItem = new List<DO.OrderItem>();
        foreach (var item in ListXElement)
        {
            orderItem.Add(Casting(item));
        }
        return foo == null ? orderItem : orderItem.Where(foo).ToList();///
    }

    public DO.OrderItem GetSingle(Func<DO.OrderItem, bool>? foo)
    {
        XElement? root = XDocument.Load("OrderItemXml.xml").Root;
        IEnumerable<XElement> ListXElement = root?.Element("ArrayOfOrderItem")?.Elements("OrderItem");
        List<DO.OrderItem> orderItem = new List<DO.OrderItem>();
        foreach (var item in ListXElement)
        {
            orderItem.Add(Casting(item));
        }
        return orderItem.Where(foo).ToList()[0];
    }

    public IEnumerable<DO.OrderItem> ReadOrderId(int id)
    {
        XElement? root = XDocument.Load("OrderItemXml.xml").Root;
        IEnumerable<XElement> ListXElement = root?.Element("ArrayOfOrderItem")?.Elements("OrderItem");
  /*???*/     List<DO.OrderItem> orderItem = new List<DO.OrderItem>();
  /*???*/     foreach (var item in ListXElement)
  /*???*/     {
  /*???*/         XElement A=root?.Element("ArrayOfOrderItem")?.Elements("OrderItem").
  /*???*/         Where(p => p.Attribute("OrderId")?.Value == id.ToString()).FirstOrDefault();
  /*???*/         if (A != null)
  /*???*/         {
  /*???*/             orderItem.Add(Casting(A));
  /*???*/         }
        }
        return orderItem;
    }

    public void Update(DO.OrderItem item)
    {
        XElement? root = XDocument.Load("ProductXml.xml").Root;
        XElement? product = root?.Element("ArrayOfProduct")?.Elements("Product")?.
                              Where(p => p.Attribute("Id")?.Value == item.Id.ToString()).FirstOrDefault();
        product?.Remove();
        product?.Add(item);
        root?.Save("..\\..\\..\\OrderItemXml.xml");
    }
}
