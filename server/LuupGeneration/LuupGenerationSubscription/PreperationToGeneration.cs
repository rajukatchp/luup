using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LuupGenerationSubscription
{
    class PreperationToGeneration
    {
        public class PrepToGeneration
        {
            public Header header { get; set; }
            public MessageData messageData { get; set; }
            

        }

        public class Header
        {
            public string Workflow { get; set; }
            public string Source { get; set; }
            public string RoutingParameter { get; set; }
            public string MessageId { get; set; }
            public string CorrelationId { get; set; }
            public string SourceSystemName { get; set; }
        }

        public class MessageData
        {
            [JsonProperty(PropertyName = "opty.id")]
            public string optyid { get; set; }

            [JsonProperty(PropertyName = "opty.name")]
            public string optyname { get; set; }

            [JsonProperty(PropertyName = "opty.owner.email")]
            public string optyowneremail { get; set; }

            [JsonProperty(PropertyName = "opty.owner.manager.email")]
            public string optyownermanageremail { get; set; }

            [JsonProperty(PropertyName = "opty.revenue")]
            public string optyrevenue { get; set; }


        }

        
    }
}
