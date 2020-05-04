using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetproductById(int id);
        IEnumerable<Product> GetProductsByUserName(string username);  
        void Createproduct(Product product);
        void Deleteproduct(int id); 
        void SaveChanges();
        User GetUserByUserName(string userName);  
    }
}
