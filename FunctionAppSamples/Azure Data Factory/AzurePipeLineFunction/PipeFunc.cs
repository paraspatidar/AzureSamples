using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using Newtonsoft.Json;
using Microsoft.Azure;
using Microsoft.Azure.Management.DataFactories;
using Microsoft.Azure.Management.DataFactories.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Collections.Generic;

namespace AzurePipeLineFunction
{
    public static class PipeFunc
    {
        [FunctionName("PipeFunc")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            string name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                .Value;

            // Get request body
            dynamic data = await req.Content.ReadAsAsync<object>();

            // Set name to query string or body data
            name = name ?? data?.name;

            // Set name to query string or body data
            /* refer : https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-create-service-principal-portal#create-an-azure-active-directory-application
             * https://www.netiq.com/communities/cool-solutions/creating-application-client-id-client-secret-microsoft-azure-new-portal/
             * http://blogs.ultima.com/azure-data-factory
             * https://docs.microsoft.com/en-us/azure/data-factory/quickstart-create-data-factory-dot-net 
             * 
             * 
             * 
             * 
             * 
             * /*/

            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
            
            var activeDirectoryEndpoint = "https://login.windows.net/";
            var resourceManagerEndpoint = "https://management.azure.com/";
            var windowsManagementUri = "https://management.core.windows.net/";
            var subscriptionId = "622XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1fa1";
            var activeDirectoryTenantId = "72f98622XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1fa1b47";

            var clientId = "8504622XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1fa1df4"; //AAD app's application id
            var clientSecret = "rnMiYQTjlH217JpK/NoEIzKolKFUTm0xKAKXxlKoxh0="; // AAD aaps application key

            var resourceGroupName = "AzurePraXXXXXXXXXXXXXXXXXXXXXXXXXXXXX-RouceGroup";
            var dataFactoryName = "AzDXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            var pipelineName = "ANXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";


             var authenticationContext = new AuthenticationContext(activeDirectoryEndpoint + activeDirectoryTenantId);
            var credential = new ClientCredential(clientId: clientId, clientSecret: clientSecret);
            var result = authenticationContext.AcquireTokenAsync(resource: windowsManagementUri, clientCredential: credential).Result;

            if (result == null) throw new InvalidOperationException("Invalid Operation");

            var token = result.AccessToken;

            var aadTokenCredentials = new TokenCloudCredentials(subscriptionId, token);

            var resourceManagerUri = new Uri(resourceManagerEndpoint);
            
            var client = new DataFactoryManagementClient(aadTokenCredentials, resourceManagerUri);


            try
            {
                var slice = DateTime.Now.AddDays(-1);
                var pl = new Pipeline();
                pl.Name = pipelineName;
                pl.Properties = new PipelineProperties();
                pl.Properties.Start = DateTime.Parse($"{slice.Date:yyyy-MM-dd}T00:00:00Z");
                pl.Properties.End = DateTime.Parse($"{slice.Date:yyyy-MM-dd}T23:59:59Z");
                pl.Properties.IsPaused = false;

                pl.Properties.Activities = new List<Activity>();

                client.Pipelines.CreateOrUpdate(resourceGroupName, dataFactoryName, new PipelineCreateOrUpdateParameters()
                {
                    Pipeline = pl
                });
                var df=client.DataFactories.List(resourceGroupName);
                
                var pl2 = client.Pipelines.Get(resourceGroupName, dataFactoryName, pipelineName);

        }
            catch (Exception e)
            {
                log.Info(e.Message);
            }



            // Set name to query string or body data

            log.Info("C# HTTP trigger function end a request   1.");

            return name == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                : req.CreateResponse(HttpStatusCode.OK, "Hello " + name);

            log.Info("C# HTTP trigger function end a request   2.");
        }
    }
}
