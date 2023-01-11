// See https://aka.ms/new-console-template for more information
using Dal;
using DO;
using System.Xml.Serialization;
a();
b();
c();
void a()
{
    StreamWriter w = new("ProductXml.xml");
    XmlSerializer ser = new(typeof(List<Product>));
    ser.Serialize(w, DataSource.ProductList);
    w.Close();
}

void b()
{
    StreamWriter w = new("OrderXml.xml");
    XmlSerializer ser = new(typeof(List<Order>));
    ser.Serialize(w, DataSource.OrderArr);
    w.Close();
}


void c()
{
    StreamWriter w = new("OrderItemXml.xml");
    XmlSerializer ser = new(typeof(List<OrderItem>));
    ser.Serialize(w, DataSource.OrderItems);
    w.Close();
}

