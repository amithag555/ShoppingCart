using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace ShoppingCart.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public IEnumerable<Product> GetProductsByUserName(string username)  
        {
            var user = _context.Users.Include("ProductsList").FirstOrDefault(foundUser => foundUser.UserName == username);
            return user.ProductsList.ToList(); 
        }

        public Product GetproductById(int id)
        {
            return _context.Products.Include(foundUser => foundUser.User)
                .SingleOrDefault(foundProduct => foundProduct.ProductId == id);
        }

        public void Createproduct(Product product)
        {
            PhotoHelper photo1 = new PhotoHelper(product.PhotoAvatar1, product.ImageName1, product.PhotoFile1, product.ImageMimeType1);
            PhotoHelper photo2 = new PhotoHelper(product.PhotoAvatar2, product.ImageName2, product.PhotoFile2, product.ImageMimeType2);
            PhotoHelper photo3 = new PhotoHelper(product.PhotoAvatar3, product.ImageName3, product.PhotoFile3, product.ImageMimeType3);

            List<PhotoHelper> photoLst = new List<PhotoHelper>();
            photoLst.Add(photo1);
            photoLst.Add(photo2);
            photoLst.Add(photo3);

            foreach (var item in photoLst)
            {
                if (item.PhotoAvatar != null && item.PhotoAvatar.Length > 0)
                {
                    item.ImageMimeType = item.PhotoAvatar.ContentType;
                    item.ImageName = Path.GetFileName(item.PhotoAvatar.FileName);

                    using (var memoryStream = new MemoryStream())
                    {
                        item.PhotoAvatar.CopyTo(memoryStream);
                        item.PhotoFile = memoryStream.ToArray();
                    }
                }
            }

            AddPhoto(product, photo1, photo2, photo3); 

            _context.Add(product);
            _context.SaveChanges();
        }

        public void Deleteproduct(int id)
        {
            var deletedProduct = _context.Products.SingleOrDefault(foundProduct => foundProduct.ProductId == id);
            _context.Remove(deletedProduct);
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public User GetUserByUserName(string userName) 
        {
            return _context.Users.FirstOrDefault(foundUser => foundUser.UserName == userName);  
        }

        private void AddPhoto(Product product, PhotoHelper photo1, PhotoHelper photo2, PhotoHelper photo3)
        {
            if (photo1.ImageName != null)
            {
                product.ImageName1 = photo1.ImageName;
                product.PhotoFile1 = photo1.PhotoFile;
                product.ImageMimeType1 = photo1.ImageMimeType;
            }
            if (photo2.ImageName != null)
            {
                product.ImageName2 = photo2.ImageName;
                product.PhotoFile2 = photo2.PhotoFile;
                product.ImageMimeType2 = photo2.ImageMimeType;
            }
            if (photo3.ImageName != null)
            {
                product.ImageName3 = photo3.ImageName;
                product.PhotoFile3 = photo3.PhotoFile;
                product.ImageMimeType3 = photo3.ImageMimeType;
            }
            if (photo1.ImageName == null && photo2.ImageName == null && photo3.ImageName == null) 
            {
                product.ImageMimeType1 = "image/png";   
                product.ImageName1 = "no_image.png"; 
            }
        }
    }
}
