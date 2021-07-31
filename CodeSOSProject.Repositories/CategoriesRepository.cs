using System;
using System.Collections.Generic;
using System.Linq;
using CodeSOSProject.DomainModels;

namespace CodeSOSProject.Repositories
{
    public interface ICategoriesRepository
    {
        void InsertCategory(Category c);
        void UpdateCategory(Category c);
        void DeleteCategory(int CategoryID);
        List<Category> GetCategories();
        List<Category> GetCategoryByCategoryID(int CategoryID);
    }
    public class CategoriesRepository : ICategoriesRepository
    {
        CodeSOSDatabaseDbContext db;

        public CategoriesRepository()
        {
            db = new CodeSOSDatabaseDbContext();
        }

        public void InsertCategory(Category c)
        {
            db.Categories.Add(c);
            db.SaveChanges();
        }

        public void UpdateCategory(Category c)
        {
            Category category = db.Categories.Where(temp => temp.CategoryID == c.CategoryID).FirstOrDefault();
            if (category != null)
            {
                category.CategoryName = c.CategoryName;
                db.SaveChanges();
            }
        }

        public void DeleteCategory(int CategoryID)
        {
            Category category = db.Categories.Where(temp => temp.CategoryID == CategoryID).FirstOrDefault();
            if (category != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
        }
        public List<Category> GetCategories()
        {
            List<Category> categories = db.Categories.ToList();
            return categories;
        }

        public List<Category> GetCategoryByCategoryID(int CategoryID)
        {
            List<Category> categories = db.Categories.Where(temp => temp.CategoryID == CategoryID).ToList();
            return categories;
        }
    }
}
