using ArrowSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    static class Demo4
    {
        public static void Run()
        {
            Console.WriteLine("Unit demo");
            var opt1 = Option.Some(StringMethod()).Map(s => s.ToUpperInvariant());
            var opt2 = Option.Some(VoidMethod()).Map(u => u.ToString());
            Console.WriteLine("StringMethod returned something:" + Desc(opt1));
            Console.WriteLine("VoidMethod returned something:" + Desc(opt2));

        }

        private static string Desc(Option<string> result)
        {
            return result.IsNone ? "No Result" : result.GetOrElse("").ToString();
        }


        private static string StringMethod()
        {
            var str = "This method returns THIS string";
            Console.WriteLine(str);
            return str;
        }
        private static Unit VoidMethod()
        {
            Console.WriteLine("This method returns nothing, but can be chained :)");
            return Unit.Value;
        }
    }
}
