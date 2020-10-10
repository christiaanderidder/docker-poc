using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Docker.Web.Validation
{
    public static class ActionResultExtensions
    {
        public static IActionResult WithMessage(this ActionResult result, string message)
        {
            return result;
        }
    }
}
