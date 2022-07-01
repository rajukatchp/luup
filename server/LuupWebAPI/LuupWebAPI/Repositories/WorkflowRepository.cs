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

        public async Task<Workflow> GetWorkflowsById(int id)
        {
            return await _context.Workflow.FindAsync(id);
        }

        public async Task<Workflow> Create(Workflow workflow)
        {
            /*foreach(var action in workflow.Actions.ToList())
            {
                _context.Actions.Add(action);
            }*/
            _context.Workflow.Add(workflow);
            await _context.SaveChangesAsync();

            return workflow;
        }

        public async Task Update(Workflow workflow)
        {
            _context.Entry(workflow).State=EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var workflowToDelete= await _context.Workflow.SingleOrDefaultAsync(w => w.Id==id);
            Console.WriteLine(workflowToDelete);
            var actionToDelete =  _context.Actions.Where(w => w.WorkflowId == id);
            var conditionToDelete= _context.Conditions.Where(w => w.WorkflowId == id);
            foreach (var action in actionToDelete)
            {
                _context.Actions.Remove(action);
            }
            foreach (var condition in conditionToDelete)
            {
                _context.Conditions.Remove(condition);
            }
            _context.Workflow.Remove(workflowToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
