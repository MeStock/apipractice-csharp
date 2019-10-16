using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace apiPractice
{
    public class TextToSpeech
    {
        public static async Task GetTextToSpeech(string text)
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.environment.getenvironmentvariable?view=netframework-4.8
            string accessToken;
            Console.WriteLine("Attempting token exchange. Please wait...\n");

            Authentication auth = new Authentication(Environment.GetEnvironmentVariable("MSFT_ENDPOINT"), Environment.GetEnvironmentVariable("MSFT_API_KEY"));
            try
            {
                accessToken = await auth.FetchTokenAsync().ConfigureAwait(false);
                Console.WriteLine("Successfully obtained an access token. \n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to obtain an access token.");
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
                return;
            }

            string host = "https://westus.tts.speech.microsoft.com/cognitiveservices/v1";
            string body = @"<speak version='1.0' xmlns='https://www.w3.org/2001/10/synthesis' xml:lang='en-US'>
                  <voice name='Microsoft Server Speech Text to Speech Voice (en-US, ZiraRUS)'>" +
                  text + "</voice></speak>";


            using (HttpClient client = new HttpClient())
            {
                using (var request = new HttpRequestMessage())
                {
                    // Set the HTTP method
                    request.Method = HttpMethod.Post;
                    // Construct the URI
                    request.RequestUri = new Uri(host);
                    // Set the content type header
                    request.Content = new StringContent(body, Encoding.UTF8, "application/ssml+xml");
                    // Set additional header, such as Authorization and User-Agent
                    request.Headers.Add("Authorization", "Bearer " + accessToken);
                    request.Headers.Add("Connection", "Keep-Alive");
                    // Update your resource name
                    request.Headers.Add("User-Agent", "QuoteTextToSpeech");
                    request.Headers.Add("X-Microsoft-OutputFormat", "riff-24khz-16bit-mono-pcm");
                    // Create a request
                    Console.WriteLine("Calling the TTS service. Please wait... \n");
                    using (var response = await client.SendAsync(request).ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                        // Asynchronously read the response
                        using (var dataStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            Console.WriteLine("Your speech file is being written to file...");
                            using (var fileStream = new FileStream(@"sample.wav", FileMode.Create, FileAccess.Write, FileShare.Write))
                            {
                                await dataStream.CopyToAsync(fileStream).ConfigureAwait(false);
                                fileStream.Close();
                            }
                            Console.WriteLine("\nYour file is ready. Press any key to exit.");
                            Console.ReadLine();
                        }
                    }
                }
            }
        }
    }
}
