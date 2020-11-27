using AmdarisInternship.API.Dtos.ModuleDtos;
using AmdarisInternship.API.Dtos.ModuleGradingDtos;
using AmdarisInternship.API.Dtos.ModuleWithModuleGradingTdos;
using AmdarisInternship.API.Exceptions;
using AmdarisInternship.API.Services.Interfaces;
using AmdarisInternship.Domain;
using AmdarisInternship.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AmdarisInternship.API.Services.Implementations
{
    public class ModuleModuleGradingsService : IModuleModuleGradingsService
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IRepository<ModuleGrading> _moduleGradingRepository;

        private readonly IMapper _mapper;

        public ModuleModuleGradingsService(IMapper mapper, IModuleRepository moduleRepository, IRepository<ModuleGrading> moduleGradingRepository)
        {
            _mapper = mapper;
            _moduleRepository = moduleRepository;
            _moduleGradingRepository = moduleGradingRepository;
        }

        public ModuleWithModuleGradingDto GetModuleWithModuleGradingsByModuleId(int id)
        {
            var module = _moduleRepository.GetModuleWithModuleGradingsByModuleId(id);

            if (module == null)
            {
                throw new NotFoundException("Module not found");
            }

            ModuleWithModuleGradingDto result = new ModuleWithModuleGradingDto();
            result.Module = _mapper.Map<ModuleDto>(module);

            foreach (var moduleGrading in module.ModuleGradings)
            {
                result.ModuleGradings.Add(_mapper.Map<ModuleGradingDto>(moduleGrading));
            }

            return result;
        }

        public IList<ModuleWithModuleGradingDto> GetModulesWithModuleGradings()
        {
            var modules = _moduleRepository.GetModulesWithModuleGradings();

            IList<ModuleWithModuleGradingDto> result = new List<ModuleWithModuleGradingDto>();

            foreach (var item in modules)
            {
                ModuleWithModuleGradingDto moduleWithGradingDto = new ModuleWithModuleGradingDto();
                moduleWithGradingDto.Module = _mapper.Map<ModuleDto>(item);

                foreach (var moduleGrading in item.ModuleGradings)
                {
                    moduleWithGradingDto.ModuleGradings.Add(_mapper.Map<ModuleGradingDto>(moduleGrading));
                }

                result.Add(moduleWithGradingDto);
            }

            return result;
        }

        public ModuleWithModuleGradingDto AddNewModuleWithModuleGrading(ModuleWithModuleGradingDto dto)
        {
            if (CheckIfModuleExists(dto.Module.Name))
            {
                return null;
            }

            Module module = _mapper.Map<Module>(dto.Module);

            _moduleRepository.Add(module);
            _moduleRepository.Save();

            List<ModuleGrading> moduleGradings = new List<ModuleGrading>();

            for (int i = 0; i < dto.ModuleGradings.Count; i++)
            {
                if (CheckIfModuleGradingExists(dto.ModuleGradings[i].Name, module.Id))
                {
                    continue;
                }

                ModuleGrading moduleGrading = new ModuleGrading
                {
                    Name = dto.ModuleGradings[i].Name,
                    Weight = dto.ModuleGradings[i].Weight,
                    ModuleId = module.Id
                };

                moduleGradings.Add(moduleGrading);
                _moduleGradingRepository.Add(moduleGrading);
            }
            _moduleGradingRepository.Save();

            return GetModuleWithModuleGradingsByModuleId(module.Id);
        }

        public ModuleWithModuleGradingDto UpdateModuleWithModuleGrading(int id, ModuleWithModuleGradingDto dto)
        {
            Module module = _moduleRepository.GetModuleWithModuleGradingsByModuleId(id);

            if (module == null)
            {
                throw new NotFoundException("Module not found");
            }

            module.Name = dto.Module.Name;

            foreach (var item in dto.ModuleGradings)
            {
                var moduleGrading = module.ModuleGradings.FirstOrDefault(x => x.Id == item.Id);

                if (moduleGrading != null)
                {
                    moduleGrading.Name = item.Name;
                    moduleGrading.Weight = item.Weight;
                }

                // new ModuleGrading
                if (item.Id == 0 && CheckIfModuleGradingExists(item.Name, module.Id) == false)
                {
                    module.ModuleGradings.Add(_mapper.Map<ModuleGrading>(item));
                }
            }

            // delete moduleGrading
            var moduleGradingDtoIds = dto.ModuleGradings.Select(x => x.Id);

            var modelGradingToDelete = module.ModuleGradings.Where(x => !moduleGradingDtoIds.Contains(x.Id));

            foreach (var item in modelGradingToDelete)
            {
                _moduleGradingRepository.Delete(item);
            }

            _moduleRepository.Save();

            return GetModuleWithModuleGradingsByModuleId(id);
        }

        private bool CheckIfModuleExists(string name)
        {
            return _moduleRepository.Find(x => x.Name == name) != null;
        }

        private bool CheckIfModuleGradingExists(string name, int moduleId)
        {
            return _moduleGradingRepository.Find(x => x.ModuleId == moduleId && x.Name == name) != null;
        }
    }
}
