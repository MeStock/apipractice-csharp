using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace apiPractice
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string result = Quote.GetQuoteAsync().Result.ToString();

            await TextToSpeech.GetTextToSpeech(result);

            //string workingDirectory = @$"{Environment.CurrentDirectory}\sample.wav";
            //System.Media.SoundPlayer player = new System.Media.SoundPlayer(workingDirectory);
            //player.PlaySync();
        }
    }
}
