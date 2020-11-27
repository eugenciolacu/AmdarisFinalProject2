using AmdarisInternship.Domain;
using AmdarisInternship.Infrastructure.Context;
using AmdarisInternship.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AmdarisInternship.Infrastructure.Repositories.Implementations
{
    public class ModuleRepository : Repository<Module>, IModuleRepository
    {
        public ModuleRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public IList<Module> GetModulesWithModuleGradings()
        {
            return _dbContext.Modules.Include(x => x.ModuleGradings).ToList();
        }

        public Module GetModuleWithModuleGradingsByModuleId(int id)
        {
            return _dbContext.Modules.Include(x => x.ModuleGradings).FirstOrDefault(x => x.Id == id);
        }
    }
}
