using AutoMapper;
using DbFirstProject.Application.DTOs;
using DbFirstProject.Application.Interfaces;
using DbFirstProject.Infrastructure.Entities;
using DbFirstProject.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFirstProject.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) 
        { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        public async Task<ProductDto> CreateAsync(ProductCreateDto dto, int userId)
        {
            var product = _mapper.Map<Product>(dto);
            product.UserId = userId;
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateAsync(int id, ProductCreateDto dto, int userId, int roleId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null) return null;
            if (roleId != 0 && product.UserId != userId) return null;

            _mapper.Map(dto, product);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }


        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null || product.UserId != userId) return false;

            _unitOfWork.Products.Delete(product);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
