using System;
using System.Transactions;
using ArrowSharp.Core;

namespace DemoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter an ID to see its customer (only ID 1 is ok)");
                var sid = Console.ReadLine();
                if (int.TryParse(sid, out var id))
                {
                    if (id == 0) return;
                    var customer = GetCustomer(id);
                    Console.WriteLine("Name is " + customer.Map(ci => ci.Name).GetOrElse("No customer found"));
                }
            }
        }


        public static Option<CustomerInfo> GetCustomer(int id)
        {
            if (id == 1) return new CustomerInfo() { Id = 1, Name = "Eiximenis" };
            else return null;
        }
    }
}
