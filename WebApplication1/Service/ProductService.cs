using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class ProductServiceException : Exception
    {
        public string Description { get; set; }
        public ProductServiceException()
        {

        }
        public ProductServiceException(string message, string description)
            : base(message)
        {
            Description = description;
        }
    }
    public class ProductService : IProductService
    {
        public readonly ProductContext _productContext;

        public ProductService()
        {
        }
        public ProductService(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public bool AddProduct(Product product, ref string result)
        {
            try
            {
                var Checkduplicate = _productContext.Product.Where(x => x.Name == product.Name).Count();
                if (Checkduplicate > 0)
                {
                    result = "Product with name " + product.Name + " already exist. ";
                    throw new ProductServiceException("Duplicate", result);
                    
                }
                _productContext.Product.Add(product);
                _productContext.SaveChanges();
                result = "Product with name " + product.Name + " is created. ";
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteProduct(int productId)
        {
            try
            {
                var ProductTodelete = _productContext.Product.Where(x => x.ID == productId).FirstOrDefault();
                if (ProductTodelete != null)
                {
                    ProductTodelete.Isdelete = true;
                    _productContext.Entry(ProductTodelete).State = System.Data.Entity.EntityState.Modified;
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
        public Product GetProductbyID(int productID)
        {
            try
            {
                var productToread = _productContext.Product.Where(x => x.ID == productID).FirstOrDefault();
                if (productToread != null)
                {
                    return productToread;
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

        public bool UpdateProduct(int productId, Product product, ref string result)
        {
            try
            {
                var ProductToUpdate = _productContext.Product.Where(x => x.ID == productId).FirstOrDefault();
                if (ProductToUpdate != null)
                {
                    var Checkduplicate = _productContext.Product.Where(x => x.Name == product.Name && x.ID != product.ID).Count();
                    if (Checkduplicate > 0)
                    {
                        result = "Product with name " + product.Name + " already exist. ";
                        throw new CategoryServiceException("Duplicate", result);
                    }
                    ProductToUpdate.Name = product.Name;
                    ProductToUpdate.ModifiedDate = DateTime.Now;
                    _productContext.Entry(ProductToUpdate).State = System.Data.Entity.EntityState.Modified;
                    _productContext.SaveChanges();
                    result = "Product with name " + product.Name + " is updated successfully. ";
                }
                return true;
            }

            catch (Exception ex)
            {
                result = " oops error in updating  product details.";
                throw ex;
            }
        }
        public List<ProductDetails_Result> Productlist(int start, int pagesize)
        {
            var startfrom = new SqlParameter("@startindex", start);
            var displayRecords = new SqlParameter("@pagesize", pagesize);

            return _productContext.Database.SqlQuery<ProductDetails_Result>("EXEC WEB_GET_Productlist @startindex,@pagesize", startfrom, displayRecords).ToList();
        }
    }
}