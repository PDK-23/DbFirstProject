using AutoMapper;
using DbFirstProject.Application.DTOs;
using DbFirstProject.Infrastructure.Entities;

namespace DbFirstProject.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User mapping
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserRegisterDto, User>().ReverseMap();
            // Product mapping
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductCreateDto, Product>();

            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
