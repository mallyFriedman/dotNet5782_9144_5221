using Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Dal;


internal class Product : IProduct
{
    internal static List<Product> ProductList = new List<Product>();
    static readonly Random rand = new Random();
    public XmlRootAttribute xRoot()
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "ArrayOfProduct";
        xRoot.IsNullable = true;
        return xRoot;
    }

    public int Add(DO.Product product)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>), xRoot());
        StreamReader r = new(@"..\..\xml\ProductXml.xml");
        List<DO.Product>? lst = (List<DO.Product>?)ser.Deserialize(r);
        product.Id = lst.Last().Id + 1;
        lst?.Add(product);
        r.Close();
        StreamWriter w = new(@"..\..\xml\Product.xml");
        ser.Serialize(w, lst);
        w.Close();
        return product.Id;
    }
    public void Delete(int id)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>), xRoot());
        StreamReader r = new(@"..\..\xml\ProductXml.xml");
        List<DO.Product>? lst = (List<DO.Product>?)ser.Deserialize(r);
        lst?.Remove(lst.Where(p => p.Id == id).FirstOrDefault());
        r.Close();
        StreamWriter w = new(@"..\..\xml\Product.xml");
        ser.Serialize(w, lst);
        w.Close();
    }

    public DO.Product Get(int id)
    {

        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>), xRoot());
        StreamReader r = new(@"..\..\xml\ProductXml.xml");
        List<DO.Product>? lst = (List<DO.Product>?)ser.Deserialize(r);
        DO.Product prod = lst.Where(p => p.Id == id).FirstOrDefault();
        r.Close();
        return prod;
    }

    public IEnumerable<DO.Product>? Get(Func<DO.Product, bool>? foo = null)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>), xRoot());
        StreamReader r = new(@"..\..\xml\ProductXml.xml");
        List<DO.Product>? lst = (List<DO.Product>?)ser.Deserialize(r);
        List<DO.Product>? prod = foo == null ? lst : lst.Where(foo).ToList();
        r.Close();
        return prod;
    }

    public DO.Product GetSingle(Func<DO.Product, bool>? foo)
    {

        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>), xRoot());
        StreamReader r = new(@"..\..\xml\ProductXml.xml");
        List<DO.Product>? lst = (List<DO.Product>?)ser.Deserialize(r);
        DO.Product prod = lst.Where(foo).FirstOrDefault();
        r.Close();
        return prod;
    }

    public void Update(DO.Product item)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>), xRoot());
        StreamReader r = new(@"..\..\xml\ProductXml.xml");
        List<DO.Product>? lst = (List<DO.Product>?)ser.Deserialize(r);
        lst?.Remove(lst.Where(p => p.Id == item.Id).FirstOrDefault());
        lst?.Add(item);
        r.Close();
        StreamWriter w = new(@"..\..\xml\Product.xml");
        ser.Serialize(w, lst);
        w.Close();
    }
}
