using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;

namespace DiagnosticSourceSample
{
    public class HttpClientListener : IObserver<KeyValuePair<string, object>>
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(KeyValuePair<string, object> value)
        {
            switch (value.Key)
            {
                case "System.Net.Http.HttpResponseOut.Start":
                    _stopwatch.Start();

                    if (value.Value.GetType().GetTypeInfo().GetDeclaredProperty("Request")?.GetValue(value.Value) is
                        HttpRequestMessage requestMessage)
                    {
                        Console.WriteLine($"HTTP Request start: {requestMessage.Method} - " +
                                          $" {requestMessage.RequestUri} - parentActivity Id: {Activity.Current.ParentId}");
                    }

                    break;
                case "System.Net.Http.HttpRequestOut.Stop":
                    _stopwatch.Stop();

                    if (value.Value.GetType().GetTypeInfo().GetDeclaredProperty("Response")?.GetValue(value.Value) is
                        HttpResponseMessage responseMessage)
                    {
                        Console.WriteLine(
                            $"Http Request finished: took {_stopwatch.ElapsedMilliseconds}ms, status code: {responseMessage.StatusCode} - parentActivity Id: {Activity.Current.ParentId}");
                    }

                    break;
            }
        }
    }
}