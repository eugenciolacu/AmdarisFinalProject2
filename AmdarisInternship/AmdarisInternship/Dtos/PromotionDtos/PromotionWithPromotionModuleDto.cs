using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmdarisInternship.API.Dtos.PromotionDtos
{
    public class PromotionWithPromotionModuleDto
    {
        public PromotionDto Promotion { get; set; }
        public List<PromotionModuleDto> PromotionModules { get; set; }


        public PromotionWithPromotionModuleDto()
        {
            PromotionModules = new List<PromotionModuleDto>();
        }
    }
}
