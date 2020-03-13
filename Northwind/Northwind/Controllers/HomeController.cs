using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Northwind.ViewModels;
using Serilog;

namespace Northwind.Controllers
{
    public class HomeController : Controller
    {



        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            
            var id = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            Log.Error(HttpContext.Features.Get<IExceptionHandlerFeature>().Error, $"Something went wrong. Correlation ID: {id}");
            return View(new ErrorViewModel { RequestId =  id});
        }
    }
}
