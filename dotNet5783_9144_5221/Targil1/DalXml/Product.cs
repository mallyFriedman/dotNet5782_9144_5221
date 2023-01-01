using Dal;
using DalApi;
using System.Xml.Linq;

namespace Dal;

internal class Product : IProduct
{
    internal static List<Product> ProductList = new List<Product>();
    static readonly Random rand = new Random();

    public int Add(DO.Product item)
    {
        XElement? root = XDocument.Load("ProductXml.xml").Root;
        XElement el2 = new("Product",
             new XAttribute("Id", item.Id),
             new XAttribute("Name", item.ProductName),
             new XAttribute("Price", item.Price),
             new XAttribute("Category", item.Category),
             new XAttribute("InStock", item.InStock));
        root?.Element("ArrayOfProduct")?.Add(el2);
        root?.Save("..\\..\\..\\ProductXml.xml");
        return item.Id;
    }

    private DO.Product Casting(XElement item)
    {
        DO.Product p = new();
        p.Id = Convert.ToInt32(item.Attribute("ID"));
        p.Price = Convert.ToInt32(item.Attribute("Price"));
        p.InStock = Convert.ToInt32(item.Attribute("InStock"));
        p.ProductName = Convert.ToString(item.Attribute("Name"));
        p.Category = (DO.Enums.Category)Convert.ToInt32(item.Attribute("Category"));
        return p;
    }

    public void Delete(int id)
    {
        XElement? root = XDocument.Load("ProductXml.xml").Root;
        XElement? product = root?.Element("ArrayOfProduct")?.Elements("Product")?.
                              Where(p => p.Attribute("Id")?.Value == id.ToString()).FirstOrDefault();
        product?.Remove();
        root?.Save("..\\..\\..\\Product.xml");

    }

    public DO.Product Get(int id)
    {

        throw new NotImplementedException();
    }

    public IEnumerable<DO.Product>? Get(Func<DO.Product, bool>? foo = null)
    {
        throw new NotImplementedException();

    }

    public DO.Product GetSingle(Func<DO.Product, bool>? foo)
    {

        XElement? root = XDocument.Load("ProductXml.xml").Root;
        XElement? product = root?.Element("ArrayOfProduct")?.Elements("Product")?.
                              Where(p => p.Attribute("Id")?.Value == id.ToString()).FirstOrDefault();
        DO.Product product2 = Casting(product);
        return product2;
    }

    public void Update(DO.Product item)
    {
        throw new NotImplementedException();
    }
}
