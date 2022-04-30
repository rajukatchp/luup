using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryChannel
{

    public class Header
    {
        public string Workflow { get; set; }
        public string RoutingParameter { get; set; }
        public string MessageId { get; set; }
        public string CorrelationId { get; set; }
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



	public class Workflow
	{
		public Workflow()
		{
			Template = new Template();
			Actions = new Actions();

			//WorkflowSchedule = new WorkflowSchedule();
			//WorkflowTemplateMapping = new WorkflowTemplateMapping();
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
		public virtual Actions Actions { get; set; }
		public virtual Template Template { get; set; }

		//public virtual WorkflowSchedule WorkflowSchedule { get; set; }

		//public virtual WorkflowTemplateMapping WorkflowTemplateMapping { get; set; }

		/*
		//public virtual ICollection<Actions> actions { get; set; }
		
		public Workflow()
		{
			actions = new HashSet<Actions>();
		}
		*/

	}

	public class Actions
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string AdaptiveJSON { get; set; }
		public string SuccessJSON { get; set; }
		public string ErrorJSON { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string CreatedBy { get; set; }
		public string ModifiedBy { get; set; }
		public bool IncludeActionInBody { get; set; }
		public bool IsActive { get; set; }
		public int WorkflowId { get; set; }
	}

	public class Template
	{

		public int Id { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
		public string Type { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string CreatedBy { get; set; }
		public string ModifiedBy { get; set; }
		public bool IsActive { get; set; }
		public string TemplateType { get; set; }
		public string RecipientsTo { get; set; }
		public string RecipientsCc { get; set; }
		public string RecipientsBcc { get; set; }
		public int WorkflowId { get; set; }
	}

}
