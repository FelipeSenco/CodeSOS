using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace CodeSOS
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/Scripts/bootstrap").Include("~/Scripts/jquery-3.6.0.js", "~/Scripts/umd/popper.js", "~/Scripts/bootstrap.js"));
            bundles.Add(new StyleBundle("~/Styles/bootstrap").Include("~/Content/bootstrap.css", "~/Content/bootstrap.css.map"));
            bundles.Add(new StyleBundle("~/Styles/site").Include("~/Content/Style.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}