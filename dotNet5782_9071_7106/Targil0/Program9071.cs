using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome9071();
            Wellcome7106();
            Console.ReadKey();
        }
        static partial void Wellcome7106();
        private static void welcome9071()
        {
            Console.Write("Enter your name: ");
            string a = Console.ReadLine();
            Console.Write("{0}, welcome to my first console application", a);
        }
    }
}
