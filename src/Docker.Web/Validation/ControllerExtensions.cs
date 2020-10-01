using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Docker.Web.Validation
{
    public static class ControllerExtensions
    {
        public static void SetNotice(this Controller controller, string message)
        {
            controller.TempData[TempDataKeys.Notice] = message;
        }
    }
}
