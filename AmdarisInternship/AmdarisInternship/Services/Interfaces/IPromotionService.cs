using AmdarisInternship.API.Dtos.PromotionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmdarisInternship.API.Services.Interfaces
{
    public interface IPromotionService
    {
        IList<PromotionDto> GetPromotions();

        PromotionDto GetPromotionById(int id);

        PromotionDto AddNewPromotionWithPromotionModule(PromotionWithPromotionModuleDto dto);

        public PromotionDto UpdatePromotion(int id, PromotionDto dto);
    }
}
