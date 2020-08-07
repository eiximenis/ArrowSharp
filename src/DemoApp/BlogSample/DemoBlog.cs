using ArrowSharp.Core;
using DemoApp.BlogSample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    public static class DemoBlog
    {
        public static async Task Run()
        {

            

            /*
            var fetcher = new Fetcher();
            var urls = new[] { new Uri("https://www.google.com"), new Uri("https://www.google.invalid"), new Uri("http://www.microsoft.com") };
            var results = await Task.WhenAll(urls.Select(u => fetcher.Fetch(u)));

            var data = results.Zip(urls)
                .Select(i => i.First.Fold(
                    e => new { Ok = false, Content = e.Message, Url = i.Second }, 
                    c => new { Ok = true, Content = c, Url = i.Second }
                ));

            var x = data.ToList();

            var either = await fetcher.Fetch(new Uri("https://www.google.com"));
            var s = either.Type switch
            {
                EitherType.Left => either.FoldLeft("", e => e.Message),
                _ => either.Fold("", v => v)
            };
            */
        }

}
}
