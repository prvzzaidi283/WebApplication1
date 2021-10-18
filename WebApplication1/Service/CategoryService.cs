using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class CategoryServiceException : Exception
    {
        public  string Description { get; set; }
        public CategoryServiceException()
        {

        }
        public CategoryServiceException(string message,string description)
            :base(message)
        {
            Description = description;
        }
    }
    public class CategoryService : ICategoryService
    {
        public readonly ProductContext _productContext;

        public CategoryService()
        {
        }
        public CategoryService(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public bool AddCategory(Category category,ref string result)
        {
            try
            {
                var Checkduplicate = _productContext.Category.Where(x => x.Name == category.Name).Count();
                if(Checkduplicate>0)
                {
                    result = "Category with name "+category.Name+ " already exist. ";
                    throw new CategoryServiceException("Duplicate", result);
                }
                _productContext.Category.Add(category);
                _productContext.SaveChanges();
                result = "Category with name " + category.Name + " is created. ";
                return true;
            }
            catch (Exception ex)
            {
                result = " oops error in adding new category.";
                throw ex;
            }
           
        }
        public Category GetCategorybyID(int categoryId)
        {
            try
            {
                var categorytoread = _productContext.Category.Where(x => x.ID == categoryId).FirstOrDefault();
                if (categorytoread != null)
                {
                    return categorytoread;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCategory(int categoryId)
        {
            try
            {
                var categorytodelete = _productContext.Category.Where(x => x.ID == categoryId).FirstOrDefault();
                if(categorytodelete!=null)
                {
                    categorytodelete.Isdelete = true;
                    _productContext.Entry(categorytodelete).State = System.Data.Entity.EntityState.Modified;
                    _productContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateCategory(int categoryId, Category category,ref string result)
        {
            try
            {
                var CatToUpdate = _productContext.Category.Where(x => x.ID == categoryId).FirstOrDefault();
                if(CatToUpdate!=null)
                {
                    var Checkduplicate = _productContext.Category.Where(x => x.Name == category.Name && x.ID !=category.ID).Count();
                    if (Checkduplicate > 0)
                    {
                        result = "Category with name " + category.Name + " already exist. ";
                        throw new CategoryServiceException("Duplicate", result);
                    }
                    CatToUpdate.Name = category.Name;
                    CatToUpdate.ModifiedDate = DateTime.Now;
                    _productContext.Entry(CatToUpdate).State = System.Data.Entity.EntityState.Modified;
                    _productContext.SaveChanges();
                    result = "Category with name " + category.Name + " is updated. ";
                }
                return true;
            }

            catch (Exception ex)
            {
                result = " oops error in updating  category.";
                throw ex;
            }
        }

       public List<CategoryDetails_Result> Categorieslist(int  start,int pagesize)
        {
            var startfrom = new SqlParameter("@startindex", start);
            var displayRecords = new SqlParameter("@pagesize", pagesize);

            return _productContext.Database.SqlQuery<CategoryDetails_Result>("EXEC WEB_Get_Categries @startindex,@pagesize", startfrom, displayRecords).ToList();
        }
        public List<SelectListItem> GetSelectListItems()
        {
            try
            {
                return _productContext.Category.Where(x=>x.Isdelete==false).Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}