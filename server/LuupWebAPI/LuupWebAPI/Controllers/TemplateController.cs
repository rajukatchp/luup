using LuupWebAPI.Models;
using LuupWebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LuupWebAPI.Controllers
{   
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITemplateRepository _templateRepository;        

        public TemplateController(IConfiguration configuration, ITemplateRepository templateRepository)
        {
            _templateRepository=templateRepository;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("api/GetTemplates")]
        public async Task<IEnumerable<Template>> GetTemplates()
        {
            return await _templateRepository.GetTemplates();
        }

        [HttpGet]
        [Route("api/GetTemplateByEntity/{entity}")]
        public async Task<ActionResult<Template>> GetTemplateByEntity(string entity)
        {
            return await _templateRepository.GetTemplateByEntity(entity);
        }
    }
}
