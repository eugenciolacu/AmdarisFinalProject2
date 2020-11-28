using AmdarisInternship.API.Dtos.PromotionDtos;
using AmdarisInternship.API.Services.Interfaces;
using AmdarisInternship.Domain;
using AmdarisInternship.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmdarisInternship.API.Services.Implementations
{
    public class PromotionService : IPromotionService
    {
        private readonly IMapper _mapper;
        private readonly IPromotionRepository _promotionRepository;
        private readonly IRepository<PromotionModule> _promotionModuleRepository;

        public PromotionService(IMapper mapper, IPromotionRepository promotionRepository, IRepository<PromotionModule> promotionModuleRepository)
        {
            _mapper = mapper;
            _promotionRepository = promotionRepository;
            _promotionModuleRepository = promotionModuleRepository;
        }

        public PromotionWithPromotionModuleDto GetPromotionWithPromotionModulesByPromotionId(int id)
        {
            return new PromotionWithPromotionModuleDto();
        }

        public PromotionWithPromotionModuleDto AddNewModuleWithModuleGrading(PromotionWithPromotionModuleDto dto)
        {
            if (CheckIfPromotionExists(dto.Promotion.Name))
            {
                return null;
            }

            Promotion promotion = _mapper.Map<Promotion>(dto.Promotion);

            _promotionRepository.Add(promotion);
            _promotionRepository.Save();

            List<PromotionModule> promotionModules = new List<PromotionModule>();

            for (int i = 0; i < dto.PromotionModules.Count; i++)
            {
                if (CheckIfPromotionModuleExists(dto.PromotionModules[i].PromotionId, dto.PromotionModules[i].ModuleId))
                {
                    continue;
                }

                PromotionModule promotionModule = new PromotionModule
                {
                    PromotionId = dto.PromotionModules[i].PromotionId,
                    ModuleId = dto.PromotionModules[i].ModuleId
                };

                promotionModules.Add(promotionModule);
                _promotionModuleRepository.Add(promotionModule);
            }

            return GetPromotionWithPromotionModulesByPromotionId(promotion.Id);
        }

        private bool CheckIfPromotionExists(string name)
        {
            return _promotionRepository.Find(x => x.Name == name) != null; 
        }

        private bool CheckIfPromotionModuleExists(int promotionId, int moduleId)
        {
            return _promotionModuleRepository.Find(x => x.PromotionId == promotionId && x.ModuleId == moduleId) != null;
        }
    }
}
