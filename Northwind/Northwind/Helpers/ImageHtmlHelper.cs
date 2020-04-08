using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Northwind.Helpers
{
    public static class ImageHtmlHelper
    {
        public static HtmlString NorthwindImageLink(this IHtmlHelper helper, string imageId, string text)
        {
            return new HtmlString($"<a href='/images/{imageId}'>{text}</a>");
        }
    }
}
