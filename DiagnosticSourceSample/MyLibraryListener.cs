using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                case "DoThingAsync.Start":
                    Console.WriteLine($"DoThingAsync.Start - activity id: {Activity.Current.Id}");
                    break;
                case "DoThingAsync.Stop":
                    Console.WriteLine("DoThingAsync.Stop");
                    if (Activity.Current != null)
                    {
                        foreach (var tag in Activity.Current.Tags)
                        {
                            Console.WriteLine($"{tag.Key} - {tag.Value}");
                        }
                    }
                    break;
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