using ArrowSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    static class Demo1
    {
        public static void Run()
        {
            Console.WriteLine("Either demo. See Demo1.cs");
            while (true)
            {
                Console.WriteLine("Enter an ID to see its customer (only ID 1 is ok). Use 0 to exit demo.");
                var sid = Console.ReadLine();
                if (int.TryParse(sid, out var id))
                {
                    if (id == 0) return;
                    var customer = GetCustomer(id);
                    // One: You can use Map and GetOrElse to safely access to the value of the Either
                    Console.WriteLine("Name is " + customer.Map(ci => ci.Name).GetOrElse("No customer found"));
                    // Two: You can use Catch to run some action if either has a left value
                    customer.Catch(ex => Console.WriteLine("Error " + ex.Message));
                }
            }
        }



        public static Either<Exception, CustomerInfo> GetCustomer(int id)
        {
            if (id == 1) return Either.Right<Exception, CustomerInfo>(new CustomerInfo() { Id = 1, Name = "Eiximenis" });
            else return Either.Left<Exception, CustomerInfo>(new ArgumentException($"Customer {id} not found"));
        }
    }
}
