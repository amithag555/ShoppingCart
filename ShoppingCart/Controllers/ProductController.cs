using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using ShoppingCart.Models;
using ShoppingCart.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ShoppingCart.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        private IHostingEnvironment _environment;

        public ProductController(IProductRepository repository, IHostingEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var ErrorMessage = TempData["ErrorMessage"];
            ViewData["ErrorMessage"] = ErrorMessage;

            return View(_repository.GetProducts());
        }

        public IActionResult OrderByTitle()
        {
            return View("Index", _repository.GetProducts().OrderBy(x => x.Title)); 
        }

        public IActionResult OrderByPostDate()
        {
            return View("Index", _repository.GetProducts().OrderBy(x => x.PostDate));
        }

        public IActionResult Details(int id)
        {
            var product = _repository.GetproductById(id);

            if (product != null)
            {
                return View(product);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public IActionResult CreatePost(Product product)
        {
            if (ModelState.IsValid)
            {
                var user = _repository.GetUserByUserName(User.Identity.Name);
                product.User = user;
                _repository.Createproduct(product);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(product);
            }
        }

        public IActionResult AddItemToCart(int productId)
        {
            List<int> productIdLst = null;

            var product = _repository.GetproductById(productId);
            product.IsForSale = true;
            _repository.SaveChanges();

            if (HttpContext.Session.GetString("ProductLst") != null)
            {
                productIdLst = JsonConvert.DeserializeObject<List<int>>(HttpContext.Session.GetString("ProductLst"));
            }
            else
            {
                productIdLst = new List<int>();
            }

            productIdLst.Add(productId);

            var serialisedDate = JsonConvert.SerializeObject(productIdLst);
            HttpContext.Session.SetString("ProductLst", serialisedDate);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult GetImage(int id, int imageNum)
        {
            Product product = _repository.GetproductById(id);  

            var photo1 = new PhotoHelper(product.PhotoAvatar1, product.ImageName1, product.PhotoFile1, product.ImageMimeType1);
            var photo2 = new PhotoHelper(product.PhotoAvatar2, product.ImageName2, product.PhotoFile2, product.ImageMimeType2);
            var photo3 = new PhotoHelper(product.PhotoAvatar3, product.ImageName3, product.PhotoFile3, product.ImageMimeType3);

            PhotoHelper image = photo1;

            if (imageNum == 2)
            {
                image = photo2;
            }
            else if (imageNum == 3)
            {
                image = photo3;
            }

            if (image != null)
            {
                string webRootpath = _environment.WebRootPath;
                string folderPath = "\\Images\\";
                string fullPath = webRootpath + folderPath + image.ImageName;

                if (System.IO.File.Exists(fullPath))
                {
                    FileStream fileOnDisk = new FileStream(fullPath, FileMode.Open);
                    byte[] fileBytes;

                    using (BinaryReader br = new BinaryReader(fileOnDisk))
                    {
                        fileBytes = br.ReadBytes((int)fileOnDisk.Length);
                    }

                    return File(fileBytes, image.ImageMimeType);
                }
                else
                {
                    if (image.PhotoFile.Length > 0)
                    {
                        return File(image.PhotoFile, image.ImageMimeType);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            else
            {
                return NotFound();
            }
        }
    } 
}