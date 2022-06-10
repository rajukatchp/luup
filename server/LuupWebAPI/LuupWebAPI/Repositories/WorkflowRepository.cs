using LuupWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LuupWebAPI.Repositories
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly DBContext _context;

        public WorkflowRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Workflow>> GetWorkflows()
        {
            return await _context.Workflow.ToListAsync();
        }
        public async Task<Workflow> Create(Workflow workflow)
        {
            _context.Workflow.Add(workflow);
            await _context.SaveChangesAsync();

            return workflow;
        }

        public async Task Update(Workflow workflow)
        {
            _context.Entry(workflow).State=EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Workflow> GetWorkflowsById(int id)
        {
            return await _context.Workflow.FindAsync(id);
        }

        public async Task<Workflow> GetWorkflowsByName(string name)
        {
            return await _context.Workflow.FindAsync(name);
        }
    }
}
