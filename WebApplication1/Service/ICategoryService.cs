using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Service
{
   public interface ICategoryService
    {
        bool AddCategory(Category category,ref string result);
        Category GetCategorybyID(int categoryId);
        bool UpdateCategory(int categoryId,Category category, ref string result);
        bool DeleteCategory(int categoryId);
        List<SelectListItem> GetSelectListItems();
        List<CategoryDetails_Result> Categorieslist(int start, int pagesize);

    }
}
