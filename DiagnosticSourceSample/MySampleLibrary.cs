using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

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

        public static async Task DoThingAsync(int id)
        {
            var activity = new Activity(nameof(DoThingAsync));
            if (DiagnosticSource.IsEnabled(MyLibraryFullName))
            {
                DiagnosticSource.StartActivity(activity, new {IdArg = id});
            }

            activity.AddTag("MyTagId", "ValueInTags");
            activity.AddBaggage("MyBaggageId", "ValueInBaggage");

            var httpClient = new HttpClient();
            await httpClient.GetAsync("http://localhost:5000/values");

            if (DiagnosticSource.IsEnabled(MyLibraryFullName))
            {
                DiagnosticSource.StopActivity(activity, new {IdArg = id});
            }
        }
    }
}