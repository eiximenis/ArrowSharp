using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Transactions;
using ArrowSharp.Core;

namespace DemoApp
{
    class Program
    {
        public async static Task Main(string[] args)
        {
            Console.WriteLine("ArrowSharp some demos...");
            Help();

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
            Console.WriteLine("9 For DemoBlog.Run");
            Console.WriteLine("0 To Exit");
        }

    }
}
