using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Bugeto.Services
{

    public interface IBrand
    {
        int BrandId { get; set; }

    }

    public interface IbrandProxy
    {
        IBrand Brand { get; set; }

    }

    public interface IProductService
    {
        int GetProductPrice(int Id);
        void GetProductPrice(int Id ,out int Price);

        int ProductCount { get; set; }
        Product AddProduct(Product product);
        IbrandProxy IbrandProxy { get; set; }
    }




    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
