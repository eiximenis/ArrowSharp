using ArrowSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    static class Demo2
    {
        public static void Run()
        {
            Console.WriteLine("Option + Id. See Demo2.cs");
            while (true)
            {
                Console.WriteLine("Enter an ID to see its customer (only ID 1 is ok). Use 0 to exit demo.");
                var sid = Console.ReadLine();
                if (int.TryParse(sid, out var id))
                {
                    if (id == 0) return;
                    var customer = GetCustomer(id);
                    // You can safely use Map
                    Console.WriteLine("Name is " + customer.Map(ci => ci.Name).GetOrElse("No customer found"));
                }
            }
        }



        public static Option<CustomerInfo> GetCustomer(int id)
        {
            var cid = Id.Just(LegacyGetCustomer(id));
            return Option.Some(cid);
        }

        private static CustomerInfo LegacyGetCustomer(int id)
        {
            if (id == 1) return new CustomerInfo() { Id = id, Name = "eiximenis" };
            else return null;
        }
    }
}
