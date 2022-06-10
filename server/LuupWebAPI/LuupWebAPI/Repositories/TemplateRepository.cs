using LuupWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LuupWebAPI.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly DBContext _context;

        public TemplateRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Template>> GetTemplates()
        {
            return await _context.Template.ToListAsync();
        }

        public async Task<Template?> GetTemplateByEntity(string entity)
        {
            Template templateByEntity = _context.Template.Where(x => x.Entity == entity).FirstOrDefault();
            if (templateByEntity != null) return templateByEntity;
            else return null;            
        }
    }
}
