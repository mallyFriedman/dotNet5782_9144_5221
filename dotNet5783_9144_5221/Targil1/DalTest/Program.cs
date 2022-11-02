using DO;
using Dal;
using DalList;
namespace Dal;
public class Program
{
    enum Options { exit, product, order };
    enum Poptions { exit, product, order };
    public void Main()
    {
              

    Console.Write("Enter your choice:");
        string choice = Console.ReadLine();
      
        try
        {
            switch (choice)
            {
                case (int)Options.exit:
                    break;
                case 'product':
                    switch (choice)
                    {
                        case 'add':
                            Product obj=new Product();
                            Console.ReadLine(obj.Price);
                            Console.ReadLine(obj.);
                            DalList.DalProduct.Create();
                            break;
                        case 'read':
                            break;
                        case 'read':
                            break;
                        case 'update':
                            break;
                        case 'delete':
                            break;
                    }
                    break;
                case 'order':
                    switch (choice)
                    {
                        case 'add':
                            break;
                        case 'read':

                            break;
                        case 'read':
                            break;
                        case 'update':
                            break;
                        case 'delete':
                            break;
                    }
                    break;
                case 'orderItem':
                    switch (choice)
                    {
                        case 'add':
                            break;
                        case 'read':
                            break;
                        case 'read':
                            break;
                        case 'update':
                            break;
                        case 'delete':
                            break;
                    }
                    break;
            }
