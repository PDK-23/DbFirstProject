using DbFirstProject.Application.DTOs;
using DbFirstProject.Application.DTOs.User;

namespace DbFirstProject.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(UserLoginDto dto);
        Task<UserDto> RegisterAsync(UserRegisterDto dto);
    }
}
