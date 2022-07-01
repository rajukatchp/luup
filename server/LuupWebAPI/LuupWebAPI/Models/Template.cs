using System.Collections.Generic;

namespace LuupWebAPI.Models
{
    public  class Template
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TemplateTitle { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string RecipientsTo { get; set; }
        public string RecipientsCC { get; set; }
        public string RecipientsBCC { get; set; }
        public int WorkflowId { get; set; }
        public string? Entity { get; set; }
        public string TemplateAdaptiveCardJSON { get; set; }
    }
}
