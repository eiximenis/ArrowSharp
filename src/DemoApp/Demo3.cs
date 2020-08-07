using ArrowSharp.Core;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    static class Demo3
    {
        public static void Run()
        {
            Console.WriteLine("Unwraping optionals");
            Console.WriteLine("This calls 20 times a method. 10 times returning null, 10 times returning data.");
            var customers = Enumerable.Range(1, 20).Select(i => GetCustomer(i)).ToList();


            Console.WriteLine($"We have {customers.Count} customers data, but some of them could be Option.None");
            Console.WriteLine("We can call unwrap to get only the real customers data");
            var realCustomers = customers.Unwrap().ToList();

            // Note that same could be achieved by Sequence of course:
            // var c2 = Sequence.Of(Enumerable.Range(1, 20).Select(i => GetCustomer(i).Map(c => c.Name)));

            Console.WriteLine($"We have {realCustomers.Count} real customers data");

            foreach (var customer in realCustomers)
            {
                Console.WriteLine($"Customer ID: {customer.Id}. NAME: {customer.Name}");
            }
        }



        public static Option<CustomerInfo> GetCustomer(int id)
        {
            return Option.Some(LegacyGetCustomer(id));
        }

        private static CustomerInfo LegacyGetCustomer(int id)
        {
            if (id % 2 == 0) return new CustomerInfo() { Id = id, Name = "Customer id " + id };
            else return null;
        }
    }
}
