using AutoMapper;
using DbFirstProject.Application.DTOs;
using DbFirstProject.Application.Interfaces;
using DbFirstProject.Infrastructure.Entities;
using DbFirstProject.Infrastructure.UnitOfWork;

namespace DbFirstProject.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateAsync(UserCreateDto dto)
        {
            // Kiểm tra username đã tồn tại chưa
            var existingUser = (await _unitOfWork.Users.GetAllAsync())
                .FirstOrDefault(u => u.Username == dto.Username);

            if (existingUser != null)
                return null; // Đã tồn tại, không cho tạo mới

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

    }
}
