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
            var controller = ViewContext.RouteData.Values["controller"].ToString();
            var action = ViewContext.RouteData.Values["action"].ToString();
            var breadcrumbs = new List<BreadcrumbViewModel>();
            breadcrumbs.Add(new BreadcrumbViewModel() { DisplayName = "Home", Url = "/" });
            breadcrumbs.Add(new BreadcrumbViewModel() { DisplayName = controller, Url = $"/{controller}" });

            if (!action.Equals("Index"))
            {
                breadcrumbs.Add(new BreadcrumbViewModel() { DisplayName = action });
            }
            return Task.FromResult<IViewComponentResult>(View(breadcrumbs));
        }
    }
}
