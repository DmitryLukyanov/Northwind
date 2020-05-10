using Microsoft.AspNetCore.Mvc;
using Northwind.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.ViewComponents
{
    public class BreadcrumbsViewComponent: ViewComponent
    {

        public Task<IViewComponentResult> InvokeAsync()
        {
            var controllerName = ViewContext.RouteData.Values["controller"]?.ToString();
            var actionName = ViewContext.RouteData.Values["action"]?.ToString();
            var breadcrumbs = new List<BreadcrumbViewModel>();
            breadcrumbs.Add(new BreadcrumbViewModel() { DisplayName = "Home", Url = "/" });
            if (!string.IsNullOrEmpty(controllerName))
            { 
                breadcrumbs.Add(new BreadcrumbViewModel() { DisplayName = controllerName, Url = $"/{controllerName}" }); 
            }

            if (!string.IsNullOrEmpty(actionName) && !actionName.Equals("Index"))
            {
                breadcrumbs.Add(new BreadcrumbViewModel() { DisplayName = actionName });
            }
            return Task.FromResult<IViewComponentResult>(View(breadcrumbs));
        }
    }
}
