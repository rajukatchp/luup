using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;

namespace azGenerationToDelivery
{
    public class fnGenerationToDelivery
    {
        string caseId = "1";
        string _apiURL = ""; // Environment.GetEnvironmentVariable("WorkflowAPIURL");
        string title = "";
        string description = "";


        [FunctionName("fnGenerationToDelivery")]
        public async Task Run(
            [ServiceBus("%TopicName%", Connection = "AzureWebJobsServiceBus")] IAsyncCollector<Message> outputServiceBus1,
            [ServiceBusTrigger("%TopicName%", "%SubscriptionToRead%", Connection = "AzureWebJobsServiceBus")] ReceivedMessage recvdMessage,
            ILogger log)
        {
            log.LogInformation($"fnPrepareToGeneration executed at: {recvdMessage.Header.MessageId}");

            AdaptiveCardDynamicData _adaptiveCardDynamicData = new AdaptiveCardDynamicData();

            #region API_Call
                    /*
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

                                foreach (Workflow wf in wfJson)
                                {
                                    title = wf.Template.Title;
                                    description = wf.Template.Description;
                                    _adaptiveCardDynamicData.messageData.MessageContentAdaptiveJSON = getAdaptiveCard(title, description, recvdMessage.MessageData["caseId"], wf.AdaptiveCardJSON);
                                }
                            }

                        }
                    */            
            #endregion

            try
            {
                //Deserialize received Workflow json
                ReceivedMessage.headerData rcvdHD = new ReceivedMessage.headerData();
                rcvdHD.Workflow = recvdMessage.Header.Workflow;
                
                _adaptiveCardDynamicData.header = new Header()
                {
                    //Workflow = rcvdHD.Workflow,
                    RoutingParameter = "GenerateSubscription",
                    MessageId = Guid.NewGuid(), // GUID
                    CorrelationId = recvdMessage.Header.CorrelationId,
                    SourceSystemName = "generateService"
                };

                //title = rcvdHD.Workflow.Template.Title;
                //description = rcvdHD.Workflow.Template.Description;

                _adaptiveCardDynamicData.messageData = new MessageData();
                _adaptiveCardDynamicData.messageData.MessageContentAdaptiveJSON = getAdaptiveCard(title, description, "1", rcvdHD.Workflow.AdaptiveCardJSON);
                                
                string strFinalHTMLMsg = "<html> <head> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">  <script type=\"{0}\"> {1} </script>  </head> <body> <div>  </div> </body> </html>";
                //_adaptiveCardDynamicData.messageData.MessageContentHTML = LoadActionableMessageBody(strFinalHTMLMsg, title, description);
                _adaptiveCardDynamicData.messageData.MessageContentHTML = strFinalHTMLMsg;
                _adaptiveCardDynamicData.messageData.MessageID = 1;

                description = description.Replace("@opty.owner.name", recvdMessage.MessageData["opty.owner.name"]);
                description = description.Replace("@opty.duedate", recvdMessage.MessageData["opty.duedate"]);
                description = description.Replace("@opty.name", recvdMessage.MessageData["opty.name"]);


                if (recvdMessage.MessageData["opty.owner.email"] != null)
                {
                    _adaptiveCardDynamicData.toRecipients = new List<RecipientDetails>();
                    _adaptiveCardDynamicData.toRecipients.Add(new RecipientDetails { RecipientName = recvdMessage.MessageData["opty.owner.name"], RecepientEmailAddress = recvdMessage.MessageData["opty.owner.email"] });
                }
                if (recvdMessage.MessageData["opty.owner.manager.email"] != null)
                {
                    _adaptiveCardDynamicData.CcRecipients = new List<RecipientDetails>();
                    _adaptiveCardDynamicData.CcRecipients.Add(new RecipientDetails { RecipientName = recvdMessage.MessageData["opty.owner.manager.name"], RecepientEmailAddress = recvdMessage.MessageData["opty.owner.manager.email"] });
                    _adaptiveCardDynamicData.CcRecipients.Add(new RecipientDetails { RecipientName = "Raju", RecepientEmailAddress = "raju@luup.ai" });
                    //_adaptiveCardDynamicData.CcRecipients.Add(new RecipientDetails { RecipientName = "Arjun", RecepientEmailAddress = "arjun@luup.ai" });
                }

                string _jsonToDelivery = JsonConvert.SerializeObject(_adaptiveCardDynamicData, Formatting.None);
                log.LogInformation($"fnPrepareToGeneration JSON: {_jsonToDelivery}");

                var _msgObj = new Message(Encoding.ASCII.GetBytes(_jsonToDelivery));
                _msgObj.MessageId = _adaptiveCardDynamicData.header.MessageId.ToString();
                _msgObj.CorrelationId = _adaptiveCardDynamicData.header.CorrelationId.ToString();
                _msgObj.ContentType = "application/json";
                _msgObj.Label = "Outlook";
                _msgObj.UserProperties["sender"] = "Generation";

                //Send final Message to Generation
                await outputServiceBus1.AddAsync(_msgObj);
                
                log.LogInformation($"Message send : " + _msgObj.ToString());
            }
            catch (Exception ex)
            {
                log.LogInformation($"Error sending Message : " + ex.ToString());
            }
        }

        private static string getAdaptiveCard(string title, string description, string caseId, string adaptiveJSON)
        {
            string strCard = adaptiveJSON;
            /*
            if (caseId == "1")
            {
                //Case 1
                strCard = "{\"type\":\"AdaptiveCard\",\"originator\":\"0605b9cd-b26b-4850-9e19-71c6fbc3c1a1\",\"body\":[{\"type\":\"TextBlock\",\"size\":\"medium\",\"weight\":\"bolder\",\"text\":\"${title}\",\"wrap\":true,\"style\":\"heading\"},{\"type\":\"TextBlock\",\"text\":\"${description}\",\"wrap\":true}],\"actions\":[{\"type\":\"Action.ShowCard\",\"card\":{\"type\":\"AdaptiveCard\",\"body\":[{\"type\":\"Input.Date\",\"id\":\"dueDate\"},{\"type\":\"Input.Text\",\"id\":\"comment\",\"placeholder\":\"Add a comment\",\"isMultiline\":true}],\"$schema\":\"http://adaptivecards.io/schemas/adaptive-card.json\"},\"title\":\"Please update the due date\",\"tooltip\":\"Update Due Date\",\"style\":\"positive\"},{\"type\":\"Action.OpenUrl\",\"title\":\"Close Opportunity\",\"style\":\"positive\"}],\"$schema\":\"http://adaptivecards.io/schemas/adaptive-card.json\",\"version\":\"1.4\"}";
            }
            else if (caseId == "2")
            {
                //Case 2
                //strCard = "{\"type\":\"AdaptiveCard\",\"originator\":\"0605b9cd-b26b-4850-9e19-71c6fbc3c1a1\",\"body\":[{\"type\":\"TextBlock\",\"size\":\"medium\",\"weight\":\"bolder\",\"text\":\"${title}\",\"wrap\":true,\"style\":\"heading\"},{\"type\":\"TextBlock\",\"text\":\"${description}\",\"wrap\":true}],\"actions\":[{\"type\":\"Action.Execute\",\"style\":\"positive\",\"title\":\"Update Product Details\"}],\"$schema\":\"http://adaptivecards.io/schemas/adaptive-card.json\",\"version\":\"1.4\"}";
                strCard = "{\"type\":\"AdaptiveCard\",\"originator\":\"0605b9cd-b26b-4850-9e19-71c6fbc3c1a1\",\"body\":[{\"type\":\"TextBlock\",\"size\":\"medium\",\"weight\":\"bolder\",\"text\":\"${title}\",\"wrap\":true,\"style\":\"heading\"},{\"type\":\"TextBlock\",\"text\":\"${description}\",\"wrap\":true}],\"actions\":[{\"type\":\"Action.ShowCard\",\"card\":{\"type\":\"AdaptiveCard\",\"body\":[{\"type\":\"Input.Date\",\"id\":\"dueDate\"},{\"type\":\"Input.Text\",\"id\":\"comment\",\"placeholder\":\"Add a comment\",\"isMultiline\":true}],\"$schema\":\"http://adaptivecards.io/schemas/adaptive-card.json\"},\"title\":\"Update Product Details\",\"tooltip\":\"Update Product Details\",\"style\":\"positive\"},{\"type\":\"Action.OpenUrl\",\"title\":\"Cancel\",\"style\":\"positive\"}],\"$schema\":\"http://adaptivecards.io/schemas/adaptive-card.json\",\"version\":\"1.4\"}";
            }
            */
            //strCard = adaptiveJSON;
            
            strCard = strCard.Replace("AdaptiveCard", "AdaptiveCard\",\"originator\":\"0605b9cd-b26b-4850-9e19-71c6fbc3c1a1");
            
            //strCard = strCard.Replace("${description}", description);

            return strCard;
        }

        /*
                 private static string LoadActionableMessageBody(string htmlTemplate,string title,string description)
        {
            string str = "";
            
            try
            {
                //Get CardJSON from DB
                var cardJson = JObject.Parse(getAdaptiveCard(title, description,"1",null));

                var cardType = cardJson.SelectToken("@type");
                if (cardType == null)
                {
                    cardType = cardJson.SelectToken("type");
                }

                string scriptType = cardType.ToString() == "MessageCard" ? "application/ld+json" : "application/adaptivecard+json";

                str = string.Format(htmlTemplate, scriptType, cardJson.ToString());

                return str;
            }
            catch (Exception) { 
            }
            
            return str;

        }


         */
        public class Header
        {
            public Workflow Workflow { get; set; }
            public string RoutingParameter { get; set; }
            public Guid MessageId { get; set; }
            public Guid CorrelationId { get; set; }
            public string SourceSystemName { get; set; }
        }

        public class MessageData
        {
            public int MessageID { get; set; }
            public string MessageTitle { get; set; }
            public string MessageContentHTML { get; set; }
            public string MessageContentAdaptiveJSON { get; set; }
        }

        public class RecipientDetails
        {
            public string RecipientName { get; set; }
            public string RecepientEmailAddress { get; set; }
        }

        public class AdaptiveCardDynamicData
        {
            public Header header { get; set; }
            public MessageData messageData { get; set; }
            public List<RecipientDetails> toRecipients { get; set; }
            public List<RecipientDetails> CcRecipients { get; set; }
            public List<RecipientDetails> BccRecipients { get; set; }
        }

        
        public class ReceivedMessage
        {
            public headerData Header;
            public Dictionary<string, string> MessageData;
        
            public class headerData
            {
                public Workflow Workflow { get; set; }
                public string Source { get; set; }
                public string RoutingParameter { get; set; }
                public Guid MessageId { get; set; }
                public Guid CorrelationId { get; set; }
                public string SourceSystemName { get; set; }
            }

        }

        public class Workflow
        {

            public Workflow()
            {

                this.Actions = new HashSet<ActionButtons>();

                //this.Actions = new HashSet<Actions>();
                this.Template = new Template();

            }


            public int Id { get; set; }
            public string workflowName { get; set; }
            public string workflowDesc { get; set; }
            public string frequency { get; set; }

            public DateTime frequencyTime { get; set; }

            public DateTime lastExecutedOn { get; set; }

            public DateTime CreatedOn { get; set; }
            public string CreatedBy { get; set; }

            public DateTime ModifiedOn { get; set; }
            public string ModifiedBy { get; set; }
            public bool IsActive { get; set; }
            public string Entity { get; set; }
            public string AdaptiveCardJSON { get; set; }

            public ICollection<ActionButtons> Actions { get; set; }
            public Template Template { get; set; }

        }

        public class ActionButtons
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Type { get; set; }
            public string API { get; set; }
            public string Parameters { get; set; }
            public string SuccessJSON { get; set; }
            public string ErrorJSON { get; set; }
            public int WorkflowId { get; set; }
        }
        public class Template
        {

            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }

            public DateTime CreatedOn { get; set; }

            public DateTime ModifiedOn { get; set; }

            public string CreatedBy { get; set; }

            public string ModifiedBy { get; set; }
            public string RecipientsTo { get; set; }
            public string RecipientsCc { get; set; }
            public string RecipientsBcc { get; set; }

            public int WorkflowId { get; set; }
        }



    }
}
