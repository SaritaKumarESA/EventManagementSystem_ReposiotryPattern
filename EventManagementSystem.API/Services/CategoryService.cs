using EventManagementSystem.API.DTOs;
using EventManagementSystem.Infrastructure.Repositories;
using EventManagementSystem.Infrastructure.Repositories.Interfaces;
using Repositories.Models;

namespace EventManagementSystem.API.Services
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> CreateCategoryAsync(CategoryDto categoryDto)
        {
            await _unitOfWork.Categories.AddAsync(new Category
            {
                CategoryName = categoryDto.CategoryName
            });
            var id = await _unitOfWork.SaveChangesAsync();
            return id;
        }
    }

    public interface ICategoryService
    {
        Task<int> CreateCategoryAsync(CategoryDto categoryDto);
    }
}
