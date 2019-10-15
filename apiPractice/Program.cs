using System;
using System.Net.Http;

namespace apiPractice
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            Quote result = Quote.GetQuoteAsync(Quote.API_URL, client).Result;
            Console.Write(result.ToString());
        }


    }
}
