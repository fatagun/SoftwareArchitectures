using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slices.Infra
{
    public static class RazorExtensions
    {

        public static void ConfigureFeatureFolders(this RazorViewEngineOptions options)
        {
            // {0} - Action
            // {1} - Controller
            // {2} - Area
            // {3} - Feature
            options.ViewLocationFormats.Clear();
            options.ViewLocationFormats.Add("/Features/{3}/{1}/{0}.cshtml");
            options.ViewLocationFormats.Add("/Features/{3}/{0}.cshtml");
            options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");

            options.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
        }

        public static void ConfigureFeatureFoldersSideBySideWithStandardViews(this RazorViewEngineOptions options)
        {
            // {0} - Action
            // {1} - Controller
            // {2} - Area
            // {3} - Feature
            options.ViewLocationFormats.Insert(0, "/Features/Shared/{0}.cshtml");
            options.ViewLocationFormats.Insert(0, "/Features/{3}/{0}.cshtml");
            options.ViewLocationFormats.Insert(0, "/Features/{3}/{1}/{0}.cshtml"); 

            options.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
        }
    }
}
