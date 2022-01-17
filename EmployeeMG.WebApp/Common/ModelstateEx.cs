using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmployeeMG.WebApp.Common
{
    public static class ModelstateEx
    {
        public static string GetErrors(this ModelStateDictionary Modelstate, string Sprator = "<br/>")
        {
            var Messages = string.Join(Sprator, Modelstate.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return Messages;
        }
    }
}
