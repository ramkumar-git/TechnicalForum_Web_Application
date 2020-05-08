using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalQandAForum.DomainModels;
using TechnicalQandAForum.ViewModels;
using TechnicalQandAForum.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace TechnicalQandAForum.ServiceLayer
{
    public interface ICategoryService
    {
        void InsertCategory(CategoryViewModel categoryViewModel);
        void UpdateCategory(CategoryViewModel categoryViewModel);
        void DeleteCategory(int categoryId);
        List<CategoryViewModel> GetCategories();
        CategoryViewModel GetCategoryById(int categoryId);
    }
    public class CategoryService : ICategoryService
    {
        ICategoryRepository categoryRepository;

        public CategoryService()
        {
            categoryRepository = new CategoryRepository();
        }

        public void InsertCategory(CategoryViewModel categoryViewModel)
        {
            var config = new MapperConfiguration(c => { c.CreateMap<CategoryViewModel, Category>(); c.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category category = mapper.Map<CategoryViewModel, Category>(categoryViewModel);
            categoryRepository.InsertCategory(category);
        }

        public void UpdateCategory(CategoryViewModel categoryViewModel)
        {
            var config = new MapperConfiguration(c => { c.CreateMap<CategoryViewModel, Category>(); c.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category category = mapper.Map<CategoryViewModel, Category>(categoryViewModel);
            categoryRepository.UpdateCategory(category);
        }
        public void DeleteCategory(int categoryId)
        {
            categoryRepository.DeleteCategory(categoryId);
        }

        public List<CategoryViewModel> GetCategories()
        {
            List<Category> categories = categoryRepository.GetCategories();
            var config = new MapperConfiguration(c => { c.CreateMap<Category, CategoryViewModel>(); c.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<CategoryViewModel> categoryViewModels= mapper.Map<List<Category>, List<CategoryViewModel>>(categories);
            return categoryViewModels;
        }

        public CategoryViewModel GetCategoryById(int categoryId)
        {
            Category category = categoryRepository.GetCategoryById(categoryId).FirstOrDefault();
            CategoryViewModel categoryViewModel = null;
            if (category != null)
            {
                var config = new MapperConfiguration(c => { c.CreateMap<Category, CategoryViewModel>(); c.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                categoryViewModel = mapper.Map<Category,CategoryViewModel>(category);
            }
            return categoryViewModel;
        }
    }
}
