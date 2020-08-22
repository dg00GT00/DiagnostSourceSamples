using System;
using System.Diagnostics;

namespace DiagnosticSourceSample
{
    public class Subscriber : IObserver<DiagnosticListener>
    {
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(DiagnosticListener value)
        {
            if (value.Name == typeof(MySampleLibrary).FullName)
            {
                value.Subscribe(new MyLibraryListener());
            }

            if (value.Name == "HttpHandlerDiagnosticListener")
            {
                value.Subscribe(new HttpClientObserver());
            }
        }
    }
}