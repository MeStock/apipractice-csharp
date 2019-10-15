using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace apiPractice
{
    public class Quote
    {
        public string quoteText { get; set; }
        public string quoteAuthor { get; set; }
        public string quoteLink { get; set; }
    }
    class Program
    {
        static HttpClient client = new HttpClient();

        static async Task<Quote> GetQuoteAsync(string path)
        {
            Quote quote = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                quote = JsonConvert.DeserializeObject<Quote>(await response.Content.ReadAsStringAsync());
            }
            return quote;
        }
        static void Main(string[] args)
        {
            Quote result = GetQuoteAsync("http://api.forismatic.com/api/1.0/?method=getQuote&format=json&lang=en").Result;
            Console.Write(result.quoteText);
        }
    }
}
