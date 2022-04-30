using System;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;



namespace LuupGenerationSubscription
{
    public static class LuupGenerationSubscription
    {
            

        [FunctionName("LuupGenerationSubscription")]
        public static async Task Pushing([ServiceBusTrigger("testentity", "subspreptogen", Connection = "LuupGenerationSubscription.Connection")] string recdJsonString,
            [ServiceBus("testentity", Connection = "LuupGenerationSubscription.Connection")] IAsyncCollector<Message> outputSerBus,
            ILogger log, ExecutionContext context)
        {
            log.LogInformation($"Getting Message from ServiceBus Topic: " + recdJsonString);

            if (recdJsonString != "")            {

                // Read the template file from the Functions folder for Azure function App
                string templateFile = Path.Combine(context.FunctionAppDirectory, "json", "Adaptivejson.json");
                string adaptivejson = Regex.Replace(File.ReadAllText(templateFile), "[\r\n\t ]+", "");
                //var adaptivejson = JsonConvert.DeserializeObject(stradaptivejson);

                //Deserialize received json
                PreperationToGeneration.PrepToGeneration recs = JsonConvert.DeserializeObject<PreperationToGeneration.PrepToGeneration>(recdJsonString);

                //GenerationtoDelivery obj = new GenerationtoDelivery();
                GenerationtoDelivery.GenToDelivery obj = new GenerationtoDelivery.GenToDelivery();

                var objHeader = new GenerationtoDelivery.header
                {
                    Workflow = recs.header.Workflow,
                    Source = recs.header.Source,
                    RoutingParameter = "GenerateToDelivery",
                    MessageId = Guid.NewGuid().ToString(),
                    CorrelationId = recs.header.CorrelationId,
                    SourceSystemName = "generateService",
                    Originator = "0605b9cd-b26b-4850-9e19-71c6fbc3c1a1"
                };

                var objMessage = new GenerationtoDelivery.messageData
                {
                    MessageID = 0,
                    MessageTitle = "Testing Title",
                    Message = "Hi " + recs.messageData.optyname + ", " + recs.header.Workflow + " is not update for > 10 days whose revenu is 1.5M " + recs.messageData.optyrevenue,
                    MessageContentHTML = "<html><head><meta http-equiv = 'Content-Type' content='text/html; charset=utf-8'><script type = '{0}' > {1} </script></head><body><p>jaison@luup.ai</p><p>This Template 2 WF0580</p>\n<p>This Template 2 WF0635</p>\n<p>This Template 2 WF0637</p>\n<p>This Template 2 WF0640</p>\n<p>This Template 2 WF0702</p>\n<p>This Template 2 WF0783</p>\n<p>This Template 2 WF0790</p>\n<p>This Template 2 WF0792</p>\n<p>This Template 2 WF0795</p>\n<p>This Template 2 WF0871</p>\n<p>This Template 2 WF1050</p>\n<div id=\'unsubscribeLink\' style=\'text-align: left; width: 100%;\'>jaison@luup.ai</div>\n<div style=\'text-align: left; width: 100%;\'>Click <a href=\'[@UnSubscribeLink]\'>here</a> to unsubscribe from this notification <br />Click <a href=\'[@UnSubscribeUILink]\'>here</a> to manage all notifications</div></body></html>", 
                    MessageContentAdaptiveJSON = adaptivejson
                };

                var objToReceipientDetails = new GenerationtoDelivery.toRecipients
                {
                    RecipientName = recs.messageData.optyname,
                    RecepientEmailAddress = recs.messageData.optyowneremail
                };

                var objCCReceipientDetails = new GenerationtoDelivery.ccRecipients
                {
                    RecipientName = "Aryan",
                    RecepientEmailAddress = "aryan@luup.ai"
                };

                var objBccReceipientDetails = new GenerationtoDelivery.bccRecipients
                {
                    RecipientName = "Jaison",
                    RecepientEmailAddress = "jaison@luup.ai"
                };

                if (obj.Header == null)
                    obj.Header = new List<GenerationtoDelivery.header>();

                if (obj.MessageData == null)
                    obj.MessageData = new List<GenerationtoDelivery.messageData>();

                if (obj.ToRecipients == null)
                    obj.ToRecipients = new List<GenerationtoDelivery.toRecipients>();

                if (obj.CcRecipients == null)
                    obj.CcRecipients = new List<GenerationtoDelivery.ccRecipients>();

                if (obj.BccRecipients == null)
                    obj.BccRecipients = new List<GenerationtoDelivery.bccRecipients>();

                obj.Header.Add(objHeader);
                obj.MessageData.Add(objMessage);
                obj.ToRecipients.Add(objToReceipientDetails);
                obj.CcRecipients.Add(objCCReceipientDetails);
                obj.BccRecipients.Add(objBccReceipientDetails);

                //Serialize the modified json for sending
                var jsonString = JsonConvert.SerializeObject(obj);            

                log.LogInformation("After Serializing the Object ready to push" + jsonString);                       

                byte[] msgbytes = Encoding.ASCII.GetBytes(jsonString);
                var message = new Message(msgbytes);
                message.MessageId = Guid.NewGuid().ToString();
                message.CorrelationId = Guid.NewGuid().ToString();
                message.ContentType = "application/text";
                message.Label = "Outlook";
                message.UserProperties["Sender"] = "Generation";

                try
                {
                    // Use the producer client to send the batch of messages to the Service Bus topic
                    await outputSerBus.AddAsync(message);
                    //Console.WriteLine($"A batch of {sbMessage} messages has been published to the topic.");
                    log.LogInformation("Messages pushed to "+ message.Label + " Subscription successfully!");
                }
                finally
                {
                    // resources and other unmanaged objects are properly cleaned up.
                    // Dispose Objects
                    message = null;
                    obj = null;
                }
            }

        }       


    }

    
}
