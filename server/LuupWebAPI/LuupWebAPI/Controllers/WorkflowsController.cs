using Microsoft.AspNetCore.Mvc;
using LuupWebAPI.Models;

using LuupWebAPI.Repositories;

namespace LuupWebAPI
{
    
    [ApiController]
    public class WorkflowsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWorkflowRepository _workflowRepository;      

        public WorkflowsController(IConfiguration configuration, IWorkflowRepository workflowRepository)
        {
            _workflowRepository = workflowRepository;
            _configuration = configuration;
        }         
                
        [HttpGet]
        [Route("api/GetWorkflows")]
        public async Task<IEnumerable<Workflow>> GetWorkflows()
        {           

            return await _workflowRepository.GetWorkflows();
        }

        [HttpGet]
        [Route("api/GetWorkflowById/{id}")]
        public async Task<ActionResult<Workflow>> GetWorkflowById(int id)
        {
            return await _workflowRepository.GetWorkflowsById(id);
        }

        [HttpGet]
        [Route("api/GetWorkflowByName/{name}")]
        public async Task<ActionResult<Workflow>> GetWorkflowByName(string name)
        {
            return await _workflowRepository.GetWorkflowsByName(name);
        }

        [HttpPut]
        [Route("api/UpdateWorkflows")]
        public async Task<ActionResult> UpdateWorkflows(int id, [FromBody] Workflow workflow)
        {
            if (id != workflow.Id)
            {
                return BadRequest();
            }

            await _workflowRepository.Update(workflow);

            return NoContent();
        }

        [HttpPost]
        [Route("api/CreateWorkflows")]
        public async Task<ActionResult<Workflow>> CreateWorkflows([FromBody] Workflow workflow)
        {
            var newWorkflow = await _workflowRepository.Create(workflow);
            return CreatedAtAction(nameof(GetWorkflows), new { id = newWorkflow.Id }, newWorkflow);
        }
    
    }
}