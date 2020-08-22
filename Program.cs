using System;
using System.Diagnostics;

namespace DiagnosticSourceSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Subscribe();
            var number = new MySampleLibrary().GetRandomNumber();
            Console.WriteLine("Hello World!");
        }

        private static void Subscribe()
        {
            DiagnosticListener.AllListeners.Subscribe(new Subscriber());
        }
    }
}