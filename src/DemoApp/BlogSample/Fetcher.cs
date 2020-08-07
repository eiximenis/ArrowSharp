using ArrowSharp.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.BlogSample
{

    public class FetcherException : Exception
    {
        public FetcherException(Exception inner) : base(inner.Message, inner) { }
    }
    public class Fetcher
    {
        public async Task<Either<FetcherException, string>> Fetch(Uri uri)
        {
            try
            {
                var sb = new StringBuilder();
                var client = new HttpClient();
                var res = await client.GetAsync(uri);
                res.EnsureSuccessStatusCode();
                var stream = await res.Content.ReadAsStreamAsync();
                using (var sr = new StreamReader(stream))
                {
                    string line = null;
                    do
                    {
                        line = await sr.ReadLineAsync();
                        sb.AppendLine(line);
                    } while (line != null);
                }
                return Either.Right<FetcherException, string>(sb.ToString());
            }
            catch (Exception ex)
            {
                return Either.Left<FetcherException, string>(new FetcherException(ex));
            }
        }

        public async Task<string> OldFetch(Uri uri)
        {
            try
            {
                var sb = new StringBuilder();
                var client = new HttpClient();
                var res = await client.GetAsync(uri);
                res.EnsureSuccessStatusCode();
                var stream = await res.Content.ReadAsStreamAsync();
                using (var sr = new StreamReader(stream))
                {
                    string line = null;
                    do
                    {
                        line = await sr.ReadLineAsync();
                        sb.AppendLine(line);
                    } while (line != null);
                }
                return sb.ToString();
            }
            catch(Exception ex)
            {
                throw new FetcherException(ex);
            }
        }
    }
}
