using LuupWebAPI.Models;

namespace LuupWebAPI.Repositories
{
    public interface IWorkflowRepository
    {
        Task<IEnumerable<Workflow>> GetWorkflows();
        Task<Workflow> GetWorkflowsById(int id);        
        Task Update(Workflow workflow);
        Task<Workflow> Create(Workflow workflow);        
        Task Delete(int id);
    }
}
