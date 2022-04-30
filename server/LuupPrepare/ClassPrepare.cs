using System;
using System.Collections.Generic;

namespace azTriggerToGeneration
{
    public class ClassPrepare
    {
            public headerData Header;
            public Dictionary<String, String> MessageData;

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