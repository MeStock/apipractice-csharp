using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace apiPractice
{

    public class Quote
    {
        static HttpClient Client = new HttpClient();
        public static string API_URL = "http://api.forismatic.com/api/1.0/?method=getQuote&format=json&lang=en";
        public string QuoteText { get; set; }
        public string QuoteAuthor { get; set; }
        public string QuoteLink { get; set; }

        internal static async Task<Quote> GetQuoteAsync()
        {
            Quote quote = null;
            HttpResponseMessage response = await Client.GetAsync(API_URL);
            if (response.IsSuccessStatusCode)
            {
                quote = JsonConvert.DeserializeObject<Quote>(await response.Content.ReadAsStringAsync());
            }
            return quote;
        }

        public override string ToString()
        {
            return $"\"{QuoteText.Trim()}\" by {QuoteAuthor}\n";
        }
    }
}
