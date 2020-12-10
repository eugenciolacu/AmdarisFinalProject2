using AmdarisInternship.API.Dtos.ModuleDtos;
using AmdarisInternship.API.Dtos.ModuleGradingDtos;
using AmdarisInternship.API.Dtos.PromotionDtos;
using AmdarisInternship.API.Dtos.LessonDtos;
using AmdarisInternship.Domain;
using AutoMapper;
using AmdarisInternship.Domain.Auth;
using AmdarisInternship.API.Dtos.UserDtos;

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

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName));
        }
    }
}
