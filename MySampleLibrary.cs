using System;
using System.Diagnostics;

namespace DiagnosticSourceSample
{
    public class MySampleLibrary
    {
        private static readonly string MyLibraryFullName = typeof(MySampleLibrary).FullName;

        private static readonly DiagnosticSource DiagnosticSource = new DiagnosticListener(MyLibraryFullName);

        public int GetRandomNumber()
        {
            if (DiagnosticSource.IsEnabled(MyLibraryFullName))
            {
                DiagnosticSource.Write($"{typeof(MySampleLibrary).FullName}.StartGenerateRandomNumber", null);
            }

            var number = new Random().Next();
            if (DiagnosticSource.IsEnabled(MyLibraryFullName))
            {
                DiagnosticSource.Write($"{typeof(MySampleLibrary).FullName}.EndGenerateRandomNumber",
                    new {RandomNumber = number});
            }

            return number;
        }
    }
}