using LuupWebAPI.Models;

namespace LuupWebAPI.Repositories
{
    public interface IWorkflowRepository
    {
        Task<IEnumerable<Workflow>> GetWorkflows();
        Task<Workflow> GetWorkflowsById(int id);
        Task<Workflow> GetWorkflowsByName(string name);
        Task Update(Workflow workflow);
        Task<Workflow> Create(Workflow workflow);
        
    }
}
