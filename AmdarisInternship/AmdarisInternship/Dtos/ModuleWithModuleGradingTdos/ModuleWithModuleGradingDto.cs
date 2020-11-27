using AmdarisInternship.API.Dtos.ModuleDtos;
using AmdarisInternship.API.Dtos.ModuleGradingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmdarisInternship.API.Dtos.ModuleWithModuleGradingTdos
{
    public class ModuleWithModuleGradingDto
    {
        public ModuleDto Module { get; set; }
        public List<ModuleGradingDto> ModuleGradings { get; set; }

        public ModuleWithModuleGradingDto()
        {
            ModuleGradings = new List<ModuleGradingDto>();
        }
    }
}
