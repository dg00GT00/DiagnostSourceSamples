using System;
using System.Collections.Generic;
using System.Reflection;

namespace DiagnosticSourceSample
{
    public class MyLibraryListener : IObserver<KeyValuePair<string, object>>
    {
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
                case "DiagnosticSourceSample.MySampleLibrary.StartGenerateRandomNumber":
                    Console.WriteLine("StartGenerateRandomNumber");
                    break;
                case "DiagnosticSourceSample.MySampleLibrary.EndGenerateRandomNumber":
                    var randomValue = value.Value
                        .GetType()
                        .GetTypeInfo()
                        .GetDeclaredProperty("RandomNumber")?.GetValue(value.Value);
                    Console.WriteLine($"StopGenerateRandomNumber Generated random value: {randomValue}");
                    break;
            }
        }
    }
}