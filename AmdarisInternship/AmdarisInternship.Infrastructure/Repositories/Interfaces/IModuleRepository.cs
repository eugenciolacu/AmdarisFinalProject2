using AmdarisInternship.Domain;
using System.Collections.Generic;

namespace AmdarisInternship.Infrastructure.Repositories.Interfaces
{
    public interface IModuleRepository : IRepository<Module>
    {
        IList<Module> GetModulesWithModuleGradings();

        Module GetModuleWithModuleGradingsByModuleId(int id);
    }
}
