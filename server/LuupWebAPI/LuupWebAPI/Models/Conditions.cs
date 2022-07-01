namespace LuupWebAPI.Models
{
    public class Conditions
    {
        public int Id { get; set; }

        public string AttributeName { get; set; }

        public string Operator { get; set; }
        public string AttributeValue { get; set; }
        public string ConditionsOperator { get; set; }
        public int WorkflowId { get; set; }
    }
}
