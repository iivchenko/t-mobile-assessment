using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Stroopwafels
{
    public class MvcApplication : HttpApplication
    {
        private readonly string _startTimeKey;

        private DateTime StartTime
        {
            get { return (DateTime)this.Application[this._startTimeKey]; }
            set { this.Application[this._startTimeKey] = value; }
        }

        public MvcApplication()
        {
            this._startTimeKey = this.GetType().Namespace + ".StartTime";
        }

        protected void Application_Start()
        {
            this.StartTime = DateTime.Now;
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }


    }
}
