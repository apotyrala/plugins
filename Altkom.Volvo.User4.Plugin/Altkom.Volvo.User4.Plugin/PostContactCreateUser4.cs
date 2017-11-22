using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Volvo.User4.Plugin
{
    public class PostContactCreateUser4 : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var tracingService = serviceProvider.GetService(typeof(ITracingService)) as ITracingService;
            var pluginExecutionContext = serviceProvider.GetService(typeof(IPluginExecutionContext)) as IPluginExecutionContext;
            var organizationServiceFactory = serviceProvider.GetService(typeof(IOrganizationServiceFactory)) as IOrganizationServiceFactory;
            try
            {
                tracingService.Trace("Starting Post Contact Create");//nadawanie logow podczas wykonywania pluginu

                var administratorService = organizationServiceFactory.CreateOrganizationService(null);//null-administrator, guid-uzytkownik powolanie kontekstu

                var target = (pluginExecutionContext.InputParameters["Target"] as Entity).ToEntity<alt_user4contact>();//kazda encja dziedziczy z entity //depth-jak gleboko zagniezdzamy sie podczas wykonywania pluginu (plugin1-depth 1, plugin 2 -depth 2); 

                // var targetName = target.Attributes["alt_name"] as string; //late bound, Formatted values-np mozemy pobrac date w formacie uzytkownika, jezyku, itp,     //input parameters-parametry wejsciowe, isolation mode-sandbox lub nie; mode-async/sync;shared variables-info miedzy pluginami
                if (target.alt_name.Contains("Error"))
                {
                    throw new InvalidPluginExecutionException("There was an error in entity name");
                }

               // var postImage(pluginExecutionContext.Inp)
                var service = organizationServiceFactory.CreateOrganizationService(pluginExecutionContext.UserId);
                var contactId = service.Create(new Contact()
                {
                    FirstName = "Jan",
                    LastName = target.alt_name
                });

                tracingService.Trace("Created contact with Id{0}", contactId);

                service.Update(new alt_user4contact()
                {
                    Id = target.Id,
                    alt_referencedcontactid = new EntityReference(Contact.EntityLogicalName, contactId)
                });





            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
