using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoppingCart.Models;
using ShoppingCart.Repositories;
using ShoppingCart.ViewModels;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller 
    {
        private IProductRepository _repository;
        private List<Product> _currentProductLst;
        private SessionStateViewModel sessionModel; 

        public CartController(IProductRepository repository) 
        {
            _repository = repository;
            _currentProductLst = new List<Product>(); 
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated && HttpContext.Session.GetString("ProductLst") != null) 
            {
                List<int> productsListId = JsonConvert.DeserializeObject<List<int>>(HttpContext.Session.GetString("ProductLst")); 

                foreach (var item in productsListId)
                {
                    var product = _repository.GetproductById(item); 
                    _currentProductLst.Add(product); 
                }

                sessionModel = new SessionStateViewModel
                {
                    SelectedProducts = _currentProductLst 
                };

                return View(sessionModel.SelectedProducts); 
            }

            return View(); 
        }

        public IActionResult RemoveItemFromCart(int productId)  
        {
            var product = _repository.GetproductById(productId); 
            product.IsForSale = false;
            _repository.SaveChanges();

            List<int> productsListId = JsonConvert.DeserializeObject<List<int>>(HttpContext.Session.GetString("ProductLst"));

            var removedItem = productsListId.FirstOrDefault(item => item == product.ProductId);
            productsListId.Remove(removedItem);

            var serialisedDate = JsonConvert.SerializeObject(productsListId); 
            HttpContext.Session.SetString("ProductLst", serialisedDate);

            if (productsListId.Count == 0)
            {
                HttpContext.Session.Remove("ProductLst");
            }

            return RedirectToAction("Index"); 
        }

        public IActionResult ByItem()
        {
            HttpContext.Session.Remove("ProductLst");

            return RedirectToAction("Index", "Product"); 
        } 
    }
}