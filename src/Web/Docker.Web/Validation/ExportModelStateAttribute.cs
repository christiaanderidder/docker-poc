using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dcoker.Web.Validation
{
    public class ExportModelStateAttribute : ModelStateTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Only export when ModelState is not valid
            if (!filterContext.ModelState.IsValid)
            {
                // Export if we are redirecting
                if (filterContext.Result is RedirectResult
                    || filterContext.Result is RedirectToRouteResult
                    || filterContext.Result is RedirectToActionResult)
                {
                    if (filterContext.Controller is Controller controller && filterContext.ModelState != null)
                    {
                        var modelState = ModelStateHelpers.SerializeModelState(filterContext.ModelState);
                        controller.TempData[Key] = modelState;
                    }
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
}