using DbFirstProject.Application.DTOs;

namespace DbFirstProject.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> CreateAsync(UserCreateDto dto);
    }
}
