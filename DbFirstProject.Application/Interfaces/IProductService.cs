using DbFirstProject.Application.DTOs;


namespace DbFirstProject.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(ProductCreateDto dto, int userId);
        Task<ProductDto> UpdateAsync(int id, ProductCreateDto dto, int userId, int roleId);
        Task<bool> DeleteAsync(int id, int userId);
    }
}
