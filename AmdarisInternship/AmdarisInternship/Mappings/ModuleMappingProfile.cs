using AmdarisInternship.API.Dtos.ModuleDtos;
using AmdarisInternship.API.Dtos.ModuleGradingDtos;
using AmdarisInternship.API.Dtos.PromotionDtos;
using AmdarisInternship.API.Dtos.LessonDtos;
using AmdarisInternship.Domain;
using AutoMapper;

namespace AmdarisInternship.API.Mappings
{
    public class ModuleMappingProfile : Profile
    {
        public ModuleMappingProfile()
        {
            CreateMap<Module, ModuleDto>();
            CreateMap<ModuleDto, Module>();

            CreateMap<ModuleGrading, ModuleGradingDto>();
            CreateMap<ModuleGradingDto, ModuleGrading>();

            CreateMap<Promotion, PromotionDto>();
            CreateMap<PromotionDto, Promotion>();

            CreateMap<PromotionModule, PromotionModuleDto>();
            CreateMap<PromotionModuleDto, PromotionModule>();

            CreateMap<Lesson, LessonDto>();
            CreateMap<LessonDto, Lesson>();

            CreateMap<Attachment, AttachmentDto>();
            CreateMap<AttachmentDto, Attachment>();
        }
    }
}
