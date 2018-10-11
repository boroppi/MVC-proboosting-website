using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace mvc_proboosting.Helpers
{
    public static class CustomHelper
    {
        //Source : https://odetocode.com/blogs/scott/archive/2012/08/25/asp-net-mvc-highlight-current-link.aspx
        public static MvcHtmlString MenuLink(this HtmlHelper helper, string text, string action, string controller)
        {
            var routeData = helper.ViewContext.RouteData.Values;
            //get url controller for instance if it's boosters our url will show /boosters that way we can use a conditional statement and set the class accordingly
            var currentController = routeData["controller"];
            var currentAction = routeData["action"];

            if (string.Equals(controller, currentController as string,
                    StringComparison.OrdinalIgnoreCase))
            {
                return helper.ActionLink(
                    text, action, controller, null,
                    new { @class = "active" }
                );
            }
            return helper.ActionLink(text, action, controller);
        }



    }
}