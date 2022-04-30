using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Azure.Identity;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Linq;

namespace DeliveryChannel
{
    public class Outlook
    {
        private static readonly string _clientId = Environment.GetEnvironmentVariable("ClientId");//"08e13489-3992-4919-aca6-7f8b7631311c";
        private static readonly string _tenantId = Environment.GetEnvironmentVariable("TenantId");//"e02be453-5b08-4dcd-b644-1e92a72abb5e";
        private static readonly string _clientSecret = Environment.GetEnvironmentVariable("ClientSecret");//"82g7Q~gP4q4eXzcau2bN60ZN3G6vNdC_pd2w3";
        private static readonly string _userObjectId = Environment.GetEnvironmentVariable("UserObjectId");//"8cabaeef-19c9-49cc-a0dd-02b77920b765";

        [FunctionName("Outlook")]
        public static void RunAsync(
            //[ServiceBusTrigger("%TopicName%", "%SubscriptionToRead%", Connection = "ServiceBusConnectionString")] AdaptiveCardDynamicData adaptiveCardDataMsg, ILogger logger)
        [ServiceBusTrigger("testentity", "outlook", Connection = "ServiceBusConnectionString")] AdaptiveCardDynamicData adaptiveCardDataMsg, ILogger logger)
        {   
            logger.LogInformation($"C# ServiceBus topic trigger function processed message: {adaptiveCardDataMsg.messageData.ToString()}");
            var credentials = new ClientSecretCredential(
               _tenantId,
               _clientId,
               _clientSecret,
               new TokenCredentialOptions
               {
                   AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
               });

            GraphServiceClient graphServiceClient = new GraphServiceClient(credentials);

            var subject = adaptiveCardDataMsg.messageData.MessageTitle;
            var emailPayload = LoadActionableMessageBody(adaptiveCardDataMsg);

            //emailPayload = adaptiveCardDataMsg.messageData.MessageContentHTML.Replace("?", "");
                      

            var BccEmails = new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "aryan@luup.ai"
                }
            };

            // Define a simple e-mail message.
            var actionableMessage = new Message
            {
                Subject = subject,
                ToRecipients = GetEmailRecipients(adaptiveCardDataMsg.toRecipients),
                CcRecipients = GetEmailRecipients(adaptiveCardDataMsg.CcRecipients),
                //BccRecipients = GetEmailRecipients(adaptiveCardDataMsg.bccRecipients),
                BccRecipients = new List<Recipient>() { BccEmails },

                Body = new ItemBody()
                {
                    ContentType = BodyType.Html,
                    Content = emailPayload
                },
                
            };

            graphServiceClient
                .Users[_userObjectId]
                .SendMail(actionableMessage, true)
                .Request()
                .PostAsync().Wait();

        }

        private static List<Recipient> GetEmailRecipients(List<RecipientDetails> recipients)
        {
            Recipient recip;
            EmailAddress emailAdd;
            List<Recipient> respList = new List<Recipient>();
            foreach (var item in recipients)
            {
                //To email address read dynamically from the service bus message
                recip = new Recipient();
                emailAdd = new EmailAddress() { Address = item.RecepientEmailAddress };
                recip.EmailAddress = emailAdd;
                respList.Add(recip);
            }

            return respList;
        }

        private static string LoadActionableMessageBody(AdaptiveCardDynamicData data)
        {
            // Insert the JSON into the HTML
            return string.Format(data.messageData.MessageContentHTML, "application/adaptivecard+json", data.messageData.MessageContentAdaptiveJSON.ToString());
        }
  }
}
