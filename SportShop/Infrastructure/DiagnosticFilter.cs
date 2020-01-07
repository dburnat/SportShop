using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SportShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SportShop.Infrastructure
{
    public class DiagnosticFilter : IActionFilter
    {
        private Stopwatch stopWatch = new Stopwatch();

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            stopWatch.Reset();
            stopWatch.Start();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            stopWatch.Stop();
            var executionTime = stopWatch.ElapsedMilliseconds;
            if (executionTime > 5000)
            {
                Debug.WriteLine("Action took to much time");
            }
        }

    }
}
