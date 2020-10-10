using Microsoft.AspNetCore.Mvc.Filters;

namespace Dcoker.Web.Validation
{
    public abstract class ModelStateTransfer : ActionFilterAttribute
    {
        protected const string Key = nameof(ModelStateTransfer);
    }
}