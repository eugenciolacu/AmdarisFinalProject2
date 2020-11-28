using AmdarisInternship.API.Dtos.PromotionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmdarisInternship.API.Services.Interfaces
{
    public interface IPromotionService
    {
        public PromotionWithPromotionModuleDto AddNewModuleWithModuleGrading(PromotionWithPromotionModuleDto dto);
    }
}
