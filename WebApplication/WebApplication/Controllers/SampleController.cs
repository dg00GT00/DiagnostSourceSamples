using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class SampleController : ControllerBase
    {
        [HttpGet("values")]
        public ActionResult<IEnumerable<string>> Get()
        {
            var activity = Activity.Current;
            if (activity == null) return new[] {"value1", "value2"};
            foreach (var item in activity.Baggage)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }

            Console.WriteLine($"Activity id: {activity.Id} - parent id: {activity.ParentId}");
            return new [] {"value1", "value2"};
        }
    }
}