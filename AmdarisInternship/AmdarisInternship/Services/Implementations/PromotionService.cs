using AmdarisInternship.API.Dtos.PromotionDtos;
using AmdarisInternship.API.Exceptions;
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

        public IList<PromotionDto> GetPromotions()
        {
            var promotions = _promotionRepository.GetAll();

            IList<PromotionDto> result = new List<PromotionDto>();
            
            foreach(var item in promotions)
            {
                PromotionDto dto = _mapper.Map<PromotionDto>(item);
                result.Add(dto);
            }

            return result;
        }

        public PromotionDto GetPromotionById(int id)
        {
            var promotion = _promotionRepository.Find(id);

            if (promotion == null)
            {
                throw new NotFoundException("Promotion not found");
            }

            PromotionDto result = _mapper.Map<PromotionDto>(promotion);

            return result;
        }

        public PromotionDto AddNewPromotionWithPromotionModule(PromotionWithPromotionModuleDto dto)
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
                if (CheckIfPromotionModuleExists(promotion.Id, dto.PromotionModules[i].ModuleId))
                {
                    continue;
                }

                PromotionModule promotionModule = new PromotionModule
                {
                    PromotionId = promotion.Id,
                    ModuleId = dto.PromotionModules[i].ModuleId
                };

                promotionModules.Add(promotionModule);
                _promotionModuleRepository.Add(promotionModule);
            }
            _promotionModuleRepository.Save();

            return GetPromotionById(promotion.Id);
        }

        public PromotionDto UpdatePromotion(int id, PromotionDto dto)
        {
            Promotion promotion = _promotionRepository.Find(id);

            if (promotion == null)
            {
                throw new NotFoundException("Promotion not found");
            }

            var result = _promotionRepository.Find(x => x.Name == dto.Name);

            if (result != null)
            {
                throw new NotFoundException("Promotion with such name already exists");
            }

            promotion.Name = dto.Name;
            promotion.StartDate = dto.StartDate;
            promotion.EndDate = dto.EndDate;

            _promotionRepository.Update(promotion);
            _promotionRepository.Save();

            return GetPromotionById(id);
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
