using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infra
{
    public class ModuleViewLocationExpander : IViewLocationExpander
    {
        private const string MODULE_KEY = "module";

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        { 
            context.Values.TryGetValue(MODULE_KEY, out string module);

            var moduleViewLocations = new string[]
            {
                        $"/Modules/{module}/Views/{{0}}.cshtml", 
            };

            viewLocations = moduleViewLocations.Concat(viewLocations);


            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var controllerName = context.ActionContext.ActionDescriptor.DisplayName;
              
            var moduleName = controllerName.Split('(', ')')[1];
            context.Values[MODULE_KEY] = moduleName;
        }
    }
}
