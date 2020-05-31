using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Helpers
{
    [HtmlTargetElement("a", Attributes = "northwind-id")]
    public class ImageTagHelper : TagHelper
    {
        [HtmlAttributeName("northwind-id")]
        public string NorthwindId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("href", $"/images/{context.AllAttributes["northwind-id"].Value}");
        }
    }
}
