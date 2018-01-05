using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Dynamic;
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

            //using built in convert and parse methods
            {
                int value = Convert.ToInt32("32");
                value = int.Parse("42");
                bool success = int.TryParse("42", out value);
            }

            Console.ReadKey();
        }



        //using as and is operators
        void LogStream(Stream stream)
        {
            //using is
            {
                MemoryStream memoryStream = stream as MemoryStream;
                if (stream is MemoryStream)
                {
                    // ....
                }
            }

            //using as
            {
                MemoryStream memoryStream = stream as MemoryStream;
                if (memoryStream != null)
                {
                    // ....
                }
            }

        }

        /*
            before c# 4 there was no dynamic keyword and it was partially static typed language. With dynamic keyword added, where you enter the world of weakly typed language.
            weakly typed is useful if you are communicationg with external resources such as COM, python, JSON etc, or reflection inside c#.

            When C# compiler encounter dynamic keyword,it stops with statically type checking. compiuler saves the intent of the code so that it can be later executed at runtime. 
            this is why dynamic wont produce compile time error.
            */
        static void DisplayInExcel(IEnumerable<dynamic> entities)
        {
            //var excelApp = new Excel.Application();
            //excelApp.Visible = true;
            //excelApp.Workbooks.Add();
            //dynamic workSheet = excelApp.ActiveSheet;
            //workSheet.Cells[1, "A"] = "Header A";
            //workSheet.Cells[1, "B"] = "Header B";
            //var row = 1;
            //foreach (var entity in entities)
            //{
            //    row++;
            //    workSheet.Cells[row, "A"] = entity.ColumnA;
            //    workSheet.Cells[row, "B"] = entity.ColumnB;
            //}
            //workSheet.Columns[1].AutoFit();
            //workSheet.Columns[2].AutoFit();
        }

        static void UseDisplayExcel()
        {
            var entities = new List<dynamic>
            {
                new
                {
                    ColumnA = 1,
                    ColumnB = "Foo"
                },
                new
                {
                    ColumnA= 2,
                    ColumnB= "Bar"
                }
            };
            DisplayInExcel(entities);
        }


    }

    /*
        .Net offers two special classes for dynamic types. DynamicObject is the most flexible
        When inheriting from DynamicObject you can override members that enable you to override operations such as getting or setting a member,calling a method etc...
    */
    public class SampleObject : DynamicObject
    {
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = binder.Name;
            //result += binder.
            return true;
        }
    }
    public class Try
    {
        public static void foo()
        {
            dynamic obj = new SampleObject();
            Console.WriteLine(obj.SomeProperty); //displays 'SomeProperty'
        }
    }


    /*
     The ExpandoObject class enables you to add and delete members of its instances at run time and also to set and get values of these members. 
     This class supports dynamic binding, which enables you to use standard syntax like sampleObject.sampleMember 
     instead of more complex syntax like sampleObject.GetAttribute("sampleMember").
     */
    public class Try2
    {
        public static void foo()
        {
            dynamic sampleObject = new ExpandoObject();

            sampleObject.test = "Dynamic Property";
            Console.WriteLine(sampleObject.test);
            Console.WriteLine(sampleObject.test.GetType());
            // This code example produces the following output:
            // Dynamic Property
            // System.String

            sampleObject.number = 10;
            sampleObject.Increment = (Action)(() => { sampleObject.number++; });

            // Before calling the Increment method.
            Console.WriteLine(sampleObject.number);

            sampleObject.Increment();

            // After calling the Increment method.
            Console.WriteLine(sampleObject.number);
            // This code example produces the following output:
            // 10
            // 11
        }
    }
}
