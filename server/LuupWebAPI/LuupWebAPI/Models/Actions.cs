namespace LuupWebAPI.Models
{
    public  class Actions
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
}
