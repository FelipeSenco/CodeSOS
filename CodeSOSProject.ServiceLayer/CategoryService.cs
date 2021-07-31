using System;
using System.Collections.Generic;
using System.Linq;
using CodeSOSProject.DomainModels;
using CodeSOSProject.ViewModels;
using CodeSOSProject.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace CodeSOSProject.ServiceLayer
{
    public interface ICategoryService
    {
        void InsertCategory(CategoryViewModel cvm);
        void UpdateCategory(CategoryViewModel cvm);
        void DeleteCategory(int CategoryID);
        List<CategoryViewModel> GetCategories();
        CategoryViewModel GetCategoryByCategoryID(int CategoryID);
    }
    public  class CategoryService : ICategoryService
    {
        ICategoriesRepository categoriesRepository;
        public CategoryService()
        {
            categoriesRepository = new CategoriesRepository();
        }

        public void InsertCategory(CategoryViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoryViewModel, Category>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category category = mapper.Map<CategoryViewModel, Category>(cvm);
            categoriesRepository.InsertCategory(category);
        }
            
        public void UpdateCategory(CategoryViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoryViewModel, Category>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category category = mapper.Map<CategoryViewModel, Category>(cvm);
            categoriesRepository.UpdateCategory(category);
        }

        public void DeleteCategory(int CategoryID)
        {
            categoriesRepository.DeleteCategory(CategoryID);
        }

        public List<CategoryViewModel> GetCategories()
        {
            List<Category> categories = categoriesRepository.GetCategories();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped(); } );
            IMapper mapper = config.CreateMapper();
            List<CategoryViewModel> cvm = mapper.Map<List<Category>, List<CategoryViewModel>>(categories);
            return cvm;
        }

        public CategoryViewModel GetCategoryByCategoryID(int CategoryID)
        {
            Category category = categoriesRepository.GetCategoryByCategoryID(CategoryID).FirstOrDefault();
            CategoryViewModel cvm = null;

            if (category != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                cvm = mapper.Map<Category, CategoryViewModel>(category);                 
            }
            return cvm;
        }
    }
}
