namespace LuupWebAPI.Models
{
    public  class Workflow
    {
        public int Id { get; set; }
        public string workflowName { get; set; }
        public string workFlowDesc { get; set; }
        public string frequency { get; set; }
        public Nullable<System.DateTime> frequencyTime { get; set; }
        public Nullable<System.DateTime> lastExecutedOn { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool isActive { get; set; }
        public string Entity { get; set; }
        public string AdaptiveCardJSON { get; set; }        
        public ICollection<Actions>? Actions { get; set; }
        public Template? Template { get; set; }

        public ICollection<Conditions>? Conditions { get; set; }
    }
   
}
