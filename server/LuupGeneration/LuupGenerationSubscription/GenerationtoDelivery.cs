using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LuupGenerationSubscription
{
    class GenerationtoDelivery
    {
        public class GenToDelivery
        {
            public List<header> Header { get; set; }
            public List<messageData> MessageData { get; set; }
            public List<toRecipients> ToRecipients { get; set; }
            public List<ccRecipients> CcRecipients { get; set; }
            public List<bccRecipients> BccRecipients { get; set; }

        }

        public class header
        {
            public string Workflow { get; set; }
            public string Source { get; set; }
            public string RoutingParameter { get; set; }
            public string MessageId { get; set; }
            public string CorrelationId { get; set; }
            public string SourceSystemName { get; set; }
            public string Originator { get; set; }


        }

        public class messageData
        {
            public int MessageID { get; set; }
            public string MessageTitle { get; set; }
            public string Message { get; set; }
            public string MessageContentHTML { get; set; }
            public string MessageContentAdaptiveJSON { get; set; }
        }

        public class toRecipients
        {
            public string RecipientName { get; set; }
            public string RecepientEmailAddress { get; set; }
        }
        public class ccRecipients
        {
            public string RecipientName { get; set; }
            public string RecepientEmailAddress { get; set; }
        }
        public class bccRecipients
        {
            public string RecipientName { get; set; }
            public string RecepientEmailAddress { get; set; }
        }
    }
}
