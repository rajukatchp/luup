using System;
using Microsoft.Extensions.Logging;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Azure.WebJobs;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;

namespace azTriggerToGeneration
{
    public class fnPrepareToGeneration
    {
        int caseId = 1;

        string _apiURL = Environment.GetEnvironmentVariable("WorkflowAPIURL");


        [FunctionName("fnPrepareToGeneration")]
        public async Task Run([TimerTrigger("%MyTimer%")] TimerInfo myTimer,
            [ServiceBus("%TopicName%", Connection = "AzureWebJobsServiceBus")] IAsyncCollector<Message> outputServiceBus1,
            ILogger log)
        {
            log.LogInformation($"fnPrepareToGeneration executed at: {DateTime.Now}");

            
            ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            
            using (var client = new HttpClient())
            {

                //HTTP GET
                _apiURL = @"https://luupwebapi.azurewebsites.net/api/Workflows/GetWorkflows";
                client.BaseAddress = new Uri(_apiURL);
                var responseTask = client.GetAsync("GetWorkflows");
                responseTask.Wait();
                
                var result = responseTask.Result;
                
                if (result.IsSuccessStatusCode)
                {
                    result.EnsureSuccessStatusCode();
                    string resultContentString = await result.Content.ReadAsStringAsync();
                    var resultContent = JsonConvert.DeserializeObject(resultContentString);
                    log.LogInformation($"Got API response at: {DateTime.Now}");

                    //Deserialize received json
                    List<Workflow> wfJson = JsonConvert.DeserializeObject<List<Workflow>>(resultContentString);

                    //Set workflow Message data for Generation
                    foreach (Workflow wf in wfJson)
                    {
                        if (wf.IsActive)
                        {
                            ClassPrepare _clsPrepare = new ClassPrepare();

                            _clsPrepare.Header = new ClassPrepare.headerData();
                            _clsPrepare.Header.Workflow = wf; // Full WorkflowID from Workflow API response
                            _clsPrepare.Header.Source = "Opportunity - ";
                            _clsPrepare.Header.RoutingParameter = "GenerateSubscription";
                            _clsPrepare.Header.MessageId = Guid.NewGuid();
                            _clsPrepare.Header.CorrelationId = Guid.NewGuid();
                            _clsPrepare.Header.SourceSystemName = "prepareService";

                            Template tmpl = wf.Template;
                            _clsPrepare.MessageData = new Dictionary<String, String>();
                            _clsPrepare.MessageData.Add("opty.id", "OPT12345");
                            _clsPrepare.MessageData.Add("opty.name", tmpl.Title); //"Infosys Azure proposal for new facility in Bangalore");
                            _clsPrepare.MessageData.Add("opty.duedate", "25th April, 2022");
                            _clsPrepare.MessageData.Add("opty.owner.name", "Aryan");
                            _clsPrepare.MessageData.Add("opty.owner.email", "Aryan@luup.ai");
                            _clsPrepare.MessageData.Add("opty.owner.manager.name", "Vikas");
                            _clsPrepare.MessageData.Add("opty.owner.manager.email", "Vikas@luup.ai");

                            _clsPrepare.MessageData.Add("caseId", "1");

                            string _jsonToSend = JsonConvert.SerializeObject(_clsPrepare);
                            log.LogInformation($"fnPrepareToGeneration JSON: {_jsonToSend}");

                            byte[] _msgbytes = Encoding.ASCII.GetBytes(_jsonToSend);
                            var _msgObject = new Message(_msgbytes);
                            _msgObject.MessageId = _clsPrepare.Header.MessageId.ToString();
                            _msgObject.CorrelationId = _clsPrepare.Header.CorrelationId.ToString();
                            _msgObject.ContentType = "application/json";
                            _msgObject.Label = "fmTrigger";
                            _msgObject.UserProperties["sender"] = "Prepare";

                            //Send final Message to Generation
                            await outputServiceBus1.AddAsync(_msgObject);

                            log.LogInformation($"Message send : " + _msgObject.MessageId);
                        }
                        else { log.LogInformation($"Workflow not Active : " + wf.Id + "  Description - " + wf.workflowDesc); }
                    }


                }
            }

        }

    }
}

