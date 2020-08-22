using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiagnosticSourceSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Subscribe();
            var number = new MySampleLibrary().GetRandomNumber();
            var httpClient = new HttpClient();
            await httpClient.GetAsync("https://kalapos.net/");

            Console.WriteLine("Hello World!");
        }

        private static void Subscribe()
        {
            DiagnosticListener.AllListeners.Subscribe(new Subscriber());
        }
    }
}