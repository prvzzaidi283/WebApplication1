using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Service
{
   public interface IProductService
    {
        bool AddProduct(Product product,ref string result);
        Product GetProductbyID(int categoryId);
        bool UpdateProduct(int productId, Product product,ref string result);
        bool DeleteProduct(int productId);
        List<ProductDetails_Result> Productlist(int start, int pagesize);

    }
}
