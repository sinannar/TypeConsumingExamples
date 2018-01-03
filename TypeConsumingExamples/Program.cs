using System;
using System.IO;
using System.Net.Http;

/*
 
    In this project, I re implement the examples that shown on C# certification book

*/

namespace TypeConsumingExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("!!! Consume Types !!!");

            //boxing an integer
            {
                int i = 42;
                object o = i;
                int x = (int)o;
            }

            //implicitly converting an integer to double
            {
                int i = 42;
                double x = i;
            }

            //implicitly converting an object to a base type
            {
                HttpClient client = new HttpClient();
                object o = client;
                IDisposable d = client;
            }

            //casting double to in
            {
                double x = 1234.6;
                int a;
                a = (int)x; // a = 1234
            }

            //explicityl casting a base type to a derived type
            {
                Object stream = new MemoryStream();
                MemoryStream memoryStream = (MemoryStream)stream;
            }

            //user defined Money class, and casting etc... check the class
            {
                Money m = new Money(42.42M);
                decimal amount = m;
                int trunctatedAmount = (int)m; // cast is redundent
            }

            Console.ReadKey();
        }
    }
}
