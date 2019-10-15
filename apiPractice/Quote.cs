using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace apiPractice
{
    public class Quote
    {
        public static string API_URL = "http://api.forismatic.com/api/1.0/?method=getQuote&format=json&lang=en";
        public string QuoteText { get; set; }
        public string QuoteAuthor { get; set; }
        public string QuoteLink { get; set; }

        internal static async Task<Quote> GetQuoteAsync(string path, HttpClient client)
        {
            Quote quote = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                quote = JsonConvert.DeserializeObject<Quote>(await response.Content.ReadAsStringAsync());
            }
            return quote;
        }

        public override string ToString()
        {
            return $"\"{QuoteText.Trim()}\" -{QuoteAuthor}\n";
        }
    }
}
