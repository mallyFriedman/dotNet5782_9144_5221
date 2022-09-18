// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome9144();
            welcome5221();
            Console.ReadKey();
        }

        static partial void welcome9144();
        private static void welcome5221()
        {
            Console.WriteLine();
            Console.WriteLine("enter your name: ");
            string name = Console.ReadLine();
            string family = Console.ReadLine();
            Console.WriteLine("{0} {1}, welcome to my first application", name, family);
        }

    }
}

