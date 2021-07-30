using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Transactions;
using ArrowSharp.Core;

using ArrowSharp.Core.Extensions;

namespace DemoApp
{


    record Street(string Name, int Number);
    record Address(Street Street, string City);

    class Program
    {
        public async static Task Main(string[] args)
        {
            /*
            var address = new Address(new Street("Main Street", 40), "Igualada");
            var streetNumberLens = new Lens<Street, int>(s => s.Number, (s, n) => s with { Number= n });
            var addressStreetLens = new Lens<Address, Street>(a => a.Street, (a, s) => a with { Street = s });
            var addressNumberLens = Lens.Compose(addressStreetLens, streetNumberLens);
            var number = addressNumberLens.Get(address);   // number is 40
            var newAddress = addressNumberLens.Set(address, 30);
            */
            while (true)
            {
                var sid = Console.ReadLine().Trim();
                if (sid == "?") Help();
                else
                {
                    if (int.TryParse(sid, out var id))
                    {
                        switch (id)
                        {
                            case 1: Demo1.Run(); Help(); break;
                            case 2: Demo2.Run(); Help(); break;
                            case 3: Demo3.Run(); Help(); break;
                            case 4: Demo4.Run(); Help(); break;
                            case 9: await DemoBlog.Run(); Help(); break;
                            case 0: return;
                            default:
                                Console.WriteLine("Invalid Option. Type ? for Help");
                                break;
                        }
                    }
                }
            }
        }

        static void Help()
        {
            Console.WriteLine("Press :");
            Console.WriteLine("? For help");
            Console.WriteLine("1 For Demo1.Run - Either");
            Console.WriteLine("2 For Demo2.Run - Option & ID");
            Console.WriteLine("3 For Demo3.Run - IEnumerable Unwrap");
            Console.WriteLine("4 For Demo4.Run - Unit");
            Console.WriteLine("9 For DemoBlog.Run");
            Console.WriteLine("0 To Exit");
        }

    }
}
