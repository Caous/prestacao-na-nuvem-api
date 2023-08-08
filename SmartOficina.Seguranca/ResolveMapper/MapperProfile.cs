namespace SmartOficina.Seguranca.ResolveMapper;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserModelDto, UserModel>()
                    .ForMember(p => p.NormalizedEmail, opt => opt.MapFrom(src => src.Email))
                    .ForMember(p => p.Email, opt => opt.MapFrom(src => src.Email))
                    .ReverseMap();
    }
}
