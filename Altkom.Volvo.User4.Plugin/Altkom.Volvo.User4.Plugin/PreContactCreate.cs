using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Volvo.User4.Plugin
{
    class PreContactCreate
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var tracingService = serviceProvider.GetService(typeof(ITracingService)) as ITracingService;
            var pluginExecutionContext = serviceProvider.GetService(typeof(IPluginExecutionContext)) as IPluginExecutionContext;
            var organizationServiceFactory = serviceProvider.GetService(typeof(IOrganizationServiceFactory)) as IOrganizationServiceFactory;
            var target = (pluginExecutionContext.InputParameters["Target"] as Entity).ToEntity<alt_user4contact>();

            target.alt_name = target.alt_name + "added in plugin";
        }
    }
}
