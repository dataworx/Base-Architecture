using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Baseline.App.Web.MVC.Helpers
{
    [HtmlTargetElement(Attributes = "is-active-route")]
    public class ActiveRouteTagHelper : TagHelper
    {
        #region Fields

        private IDictionary<string, string> routeValues; 
        
        #endregion

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("asp-page-fragment")]
        public string PageFragment { get; set; }

        [HtmlAttributeName("active-class")]
        public string ActiveClass { get; set; }
        
        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public IDictionary<string, string> RouteValues
        {
            get
            {
                return this.routeValues ??= new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }
            set => this.routeValues = value;
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (ShouldBeActive())
            {
                MakeActive(output);
            }

            output.Attributes.RemoveAll("is-active-route");
            output.Attributes.RemoveAll("active-class");
        }

        private bool ShouldBeActive()
        {
            if (ViewContext.RouteData.Values.ContainsKey("Controller"))
            {
                // MVC
                var currentController = ViewContext.RouteData.Values["Controller"].ToString();
                var currentAction = ViewContext.RouteData.Values["Action"].ToString();

                if (!string.IsNullOrWhiteSpace(Controller) && Controller.ToLower() != currentController.ToLower())
                {
                    return false;
                }

                if (!string.IsNullOrWhiteSpace(Action) && Action.ToLower() != currentAction.ToLower())
                {
                    return false;
                }

                foreach (var routeValue in RouteValues)
                {
                    if (!ViewContext.RouteData.Values.ContainsKey(routeValue.Key) ||
                        ViewContext.RouteData.Values[routeValue.Key].ToString() != routeValue.Value)
                    {
                        return false;
                    }
                }

                return true;

            }
            else if (ViewContext.RouteData.Values.ContainsKey("Page"))
            {
                // Razor Pages
                var currentPage = ViewContext.RouteData.Values["Page"].ToString();
                if (!string.IsNullOrWhiteSpace(this.PageFragment) && currentPage.ToLower().StartsWith(this.PageFragment.ToLower()))
                {
                    return true;
                }
            }

            return false;

        }

        private void MakeActive(TagHelperOutput output)
        {
            var classAttr = output.Attributes.FirstOrDefault(a => a.Name == "class");
            if (classAttr == null)
            {
                classAttr = new TagHelperAttribute("class", this.ActiveClass);
                output.Attributes.Add(classAttr);
            }
            else if (classAttr.Value == null || classAttr.Value.ToString().IndexOf(this.ActiveClass) < 0)
            {
                output.Attributes.SetAttribute("class", classAttr.Value == null
                    ? this.ActiveClass
                    : classAttr.Value + $" {this.ActiveClass}");
            }
        }
    }
}
