using AutoMapper;
using BCrypt.Net;
using DbFirstProject.Application.DTOs;
using DbFirstProject.Application.DTOs.User;
using DbFirstProject.Application.Interfaces;
using DbFirstProject.Infrastructure.Entities;
using DbFirstProject.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DbFirstProject.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _mapper = mapper;
        }

        public async Task<string> LoginAsync(UserLoginDto dto)
        {
            var user = (await _unitOfWork.Users.GetAllAsync())
                .FirstOrDefault(u => u.Username == dto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            // Tạo claims cho JWT
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserDto> RegisterAsync(UserRegisterDto dto)
        {
            // Kiểm tra username đã tồn tại chưa
            var existingUser = (await _unitOfWork.Users.GetAllAsync())
                .FirstOrDefault(u => u.Username == dto.Username);

            if (existingUser != null)
                return null; // Đã tồn tại, không cho tạo mới

            var user = _mapper.Map<User>(dto);
            user.RoleId = 1;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }
    }
}
