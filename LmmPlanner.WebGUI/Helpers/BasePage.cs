using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace LmmPlanner.WebGUI.Helpers
{
    public class BasePageModel : PageModel
    {
        [NonAction]
        public virtual PartialViewResult PartialView(string viewName, object model)
        {

            var myViewData = new ViewDataDictionary(
                            new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(),
                            new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()) { { viewName, model } };
            myViewData.Model = model;
            PartialViewResult result = new PartialViewResult()
            {
                ViewName = viewName,
                ViewData = myViewData,
            };
            return result;

            // ViewData.Model = model;

            // return new PartialViewResult()
            // {
            //     ViewName = viewName,
            //     ViewData = ViewData,
            //     TempData = TempData
            // };
        }
    }
}