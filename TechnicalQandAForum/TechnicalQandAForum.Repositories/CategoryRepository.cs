using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalQandAForum.DomainModels;

namespace TechnicalQandAForum.Repositories
{
    public interface ICategoryRepository
    {
        void InsertCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryId);
        List<Category> GetCategories();
        List<Category> GetCategoryById(int categoryId);

    }
    public class CategoryRepository : ICategoryRepository
    {
        TechnicalQandAForumDbContext dbContext;

        public CategoryRepository()
        {
            dbContext = new TechnicalQandAForumDbContext();
        }
        public void DeleteCategory(int categoryId)
        {
            Category ct = dbContext.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefault();
            if (ct != null)
            {
                dbContext.Categories.Remove(ct);
                dbContext.SaveChanges();
            }
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = dbContext.Categories.ToList();
            return categories;
        }

        public List<Category> GetCategoryById(int categoryId)
        {
            List<Category> categories = dbContext.Categories.Where(c => c.CategoryId == categoryId).ToList();
            return categories;
        }

        public void InsertCategory(Category category)
        {
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            Category ct = dbContext.Categories.Where(c => c.CategoryId == category.CategoryId).FirstOrDefault();
            if (ct != null)
            {
                ct.CategoryName = category.CategoryName;
                dbContext.SaveChanges();
            }
        }
    }
}
