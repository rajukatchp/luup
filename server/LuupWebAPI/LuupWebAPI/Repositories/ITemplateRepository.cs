using LuupWebAPI.Models;

namespace LuupWebAPI.Repositories
{
    public interface ITemplateRepository
    {
        Task<IEnumerable<Template>> GetTemplates();
        Task<Template> GetTemplateByEntity(string entity);
    }
}
