using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitter.Base.Hangfire
{
    public class BTHangFireAuthorizationFilter:IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
