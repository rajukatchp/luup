using ActionsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static string organizationUrl = "https://org03668bd2.api.crm8.dynamics.com/";
        private static string requestApiUrl = "/api/data/v9.0/";
        private LeadsResponse leadsResponse=new LeadsResponse();

        private SingleCrmLead crmLead = new SingleCrmLead();
        private static string accessToken = "";       
        private static async Task<string> GetAccessTokenAsync()
        {
            var clientId = "78b4df35-fb68-4917-97da-8f76f23dcf93";
            var clientSecret = "6Oz7Q~epcij6rtNyy4GQnltZOa9XQM0jW8Cg3";
            var aadInstanceUrl = "https://login.microsoftonline.com";
            var tenantId = "e02be453-5b08-4dcd-b644-1e92a72abb5e";            

            var clientCred = new ClientCredential(clientId, clientSecret);
            var authenticationContext = new AuthenticationContext($"{aadInstanceUrl}/{tenantId}");
            var authenticationResult = await authenticationContext.AcquireTokenAsync(organizationUrl, clientCred);
            accessToken = authenticationResult.AccessToken;
            Console.WriteLine(accessToken);
            return accessToken;
        }       



        [HttpGet] 
        public LeadsResponse GetValues()
        {
            #region Client configuration
            
            string accessToken = GetAccessTokenAsync().Result;
            var client = new HttpClient
            {                
                BaseAddress = new Uri(organizationUrl + requestApiUrl),
                Timeout = new TimeSpan(0, 2, 0)    
            };
            
            HttpRequestHeaders headers = client.DefaultRequestHeaders;
            headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            headers.Add("OData-MaxVersion", "4.0");
            headers.Add("OData-Version", "4.0");
            headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            #endregion Client configuration

            #region Web API call
            JObject body= new JObject();

            var response = client.GetAsync("leads").Result;
            Console.WriteLine(response);
            
            if (response.IsSuccessStatusCode)
            {
                // Parse the JSON formatted service response to obtain the user ID.  
                body = JObject.Parse(
                    response.Content.ReadAsStringAsync().Result);               

                JsonSerializer serializer=new JsonSerializer();
                leadsResponse = (LeadsResponse)serializer.Deserialize(new JTokenReader(body),typeof(LeadsResponse));               
                
            }

            else
            {
                Console.WriteLine("Web API call failed");
                Console.WriteLine("Reason: " + response.ReasonPhrase);
            }            
            
            // Pause program execution.
            Console.ReadKey();            
            return leadsResponse;

            #endregion Web API Call
        }

        
        [HttpGet("{leadName}")]
        public SingleCrmLead GetIdByLeadName(string leadName)
        {
            #region Client configuration
            string accessToken = GetAccessTokenAsync().Result;
            var client = new HttpClient
            {
                BaseAddress = new Uri(organizationUrl + "/api/data/v9.0/"),
                Timeout = new TimeSpan(0, 2, 0)
            };

            HttpRequestHeaders headers = client.DefaultRequestHeaders;
            headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            headers.Add("OData-MaxVersion", "4.0");
            headers.Add("OData-Version", "4.0");
            headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            #endregion Client configuration

            #region Web API call
            var response = client.GetAsync("leads").Result;           
            
            
            if (response.IsSuccessStatusCode)
            {
                // Parse the JSON formatted service response to obtain the user ID.  
                JObject body = JObject.Parse(
                    response.Content.ReadAsStringAsync().Result);

                JsonSerializer serializer = new JsonSerializer();
                leadsResponse = (LeadsResponse)serializer.Deserialize(new JTokenReader(body), typeof(LeadsResponse));

                foreach(var opportunity in leadsResponse.value)
                {
                    if(opportunity.fullname == leadName)
                    {
                        crmLead.leadid = opportunity.leadid;
                        crmLead.lastname = opportunity.lastname;
                        crmLead.revenue= opportunity.revenue;
                        crmLead.OdataEtag = opportunity.OdataEtag;
                        crmLead.address1_line1 = opportunity.address1_line1;
                    }
                }
                 
            }

            else
            {
                Console.WriteLine("Web API call failed");
                Console.WriteLine("Reason: " + response.ReasonPhrase);
            }

            // Pause program execution.
            Console.ReadKey();            
            return crmLead;

            #endregion Web API Call
        }

        [HttpPost]
        public async Task<LeadsResponse> PostAsync([FromBody] string value)
        {

            #region Client configuration
            string accessToken = GetAccessTokenAsync().Result;
            var client = new HttpClient
            {
                BaseAddress = new Uri(organizationUrl + "/api/data/v9.0/"),
                Timeout = new TimeSpan(0, 2, 0)
            };
            HttpRequestHeaders headers = client.DefaultRequestHeaders;
            headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            headers.Add("OData-MaxVersion", "4.0");
            headers.Add("OData-Version", "4.0");
            headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            #endregion Client configuration

            #region Web API call
            string leadId =  "1b87a5d5-3868-40c6-be64-74c9fe7f6225";
            
            string requestString = string.Format("leads({0})/Microsoft.Dynamics.CRM.QualifyLead", leadId);

            string requestData = @"{
                   'CreateAccount': true,
                   'CreateContact': true,
                   'CreateOpportunity': true,
                   'Status':3
                   }";

            var response = await client.PostAsync(organizationUrl + requestApiUrl + requestString, new StringContent(requestData, System.Text.Encoding.UTF8, "application/json"));
            
            var stringResponse = await response.Content.ReadAsStringAsync();
                       
            return leadsResponse;

            #endregion Web API Call
        }
    }
}
