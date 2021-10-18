using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        public ICategoryService _categoryService;
        public IProductService _productService;

        public ProductController()
        {

        }
        public ProductController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        #region Category
        public ActionResult Category()
        {
            return View();
        }
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            string result = string.Empty;

            if(ModelState.IsValid)
            {
                try
                {
                    _categoryService.AddCategory(category,ref result);
                    Response.StatusCode = 200;

                }
                catch (CategoryServiceException ex)
                {
                    Response.StatusCode = 400;
                    return Json(ex.Description, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    Response.StatusCode = 422;
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult getCategriesjsonlist()
        {
            List<CategoryDetails_Result> data = new List<CategoryDetails_Result>();
            var start = (Convert.ToInt32(Request["start"]));
            var Length = (Convert.ToInt32(Request["length"])) == 0 ? 10 : (Convert.ToInt32(Request["length"]));
            var recordsTotal = 0;

            data = _categoryService.Categorieslist(start,Length);
            recordsTotal = data.Count > 0 ? data[0].TotalRecords : 0;
            return Json(new { data = data, recordsTotal = recordsTotal, recordsFiltered = recordsTotal }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult DeleteCategory(int Id)
        {
            bool result = _categoryService.DeleteCategory(Id);
            if(result)
            {
                return Json("Category delete successfully.", JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json("Error!! oops issue in deleting category.", JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult EditCategory(int id)
        {
            Category category = new Category();
            try
            {
                category= _categoryService.GetCategorybyID(id);

            }
            catch (Exception ex)
            {
               

            }
            return View(category);
        }
        public ActionResult UpdateCategory(Category category)
        {
            string result = string.Empty;

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryService.UpdateCategory(category.ID, category, ref result);
                    Response.StatusCode = 200;

                }
                catch (CategoryServiceException ex)
                {
                    Response.StatusCode = 400;
                    return Json(ex.Description, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    Response.StatusCode = 422;
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
            return View();
        }
        #endregion

        #region Product
        public ActionResult Products()
        {
            return View();
        }
        public ActionResult CreateProduct()
        {
            ViewBag.Category = GetSelectListItems();
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            string result = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    _productService.AddProduct(product,ref result);
                    ModelState.Clear();
                    Response.StatusCode = 200;

                }
                catch (ProductServiceException ex)
                {
                    Response.StatusCode = 400;
                    return Json(ex.Description, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    Response.StatusCode = 422;
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public List<SelectListItem> GetSelectListItems()
        {
            return _categoryService.GetSelectListItems();
        }
        [HttpPost]
        public JsonResult GetProductlist()
        {
            List<ProductDetails_Result> data = new List<ProductDetails_Result>();
            var start = (Convert.ToInt32(Request["start"]));
            var Length = (Convert.ToInt32(Request["length"])) == 0 ? 10 : (Convert.ToInt32(Request["length"]));
            var recordsTotal = 0;

            data = _productService.Productlist(start, Length);
            recordsTotal = data.Count > 0 ? data[0].TotalRecords : 0;
            return Json(new { data = data, recordsTotal = recordsTotal, recordsFiltered = recordsTotal }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditProduct(int id)
        {
            Product product = new Product();
            try
            {
                ViewBag.Category = GetSelectListItems();
                product = _productService.GetProductbyID(id);

            }
            catch (Exception ex)
            {


            }
            return View(product);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product product)
        {
            string result = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    _productService.UpdateProduct(product.ID, product, ref result);
                    ModelState.Clear();
                    Response.StatusCode = 200;

                }
                catch (ProductServiceException ex)
                {
                    Response.StatusCode = 400;
                    return Json(ex.Description, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    Response.StatusCode = 422;
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeleteProduct(int Id)
        {
            bool result = _productService.DeleteProduct(Id);
            if (result)
            {
                return Json("Product delete successfully.", JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json("Error!! oops issue in deleting Product.", JsonRequestBehavior.AllowGet);

            }
        }

        #endregion
    }
}