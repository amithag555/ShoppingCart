using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using ShoppingCart.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ShoppingCart.Repositories;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ShoppingCart.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private IProductRepository _repository;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, IProductRepository repository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _repository = repository;
        }

        public IActionResult Login()
        { 
            return RedirectToAction("Index", "Product");
        }

        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, loginModel.RememberMe, false);

                if (result.Succeeded)
                { 
                    return RedirectToAction("Index", "Product");  
                }
            }

            ModelState.AddModelError("", "UserName or password is not valid");

            return RedirectToAction("Index", "Product");  
        }

        public async Task<IActionResult> Logout() 
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ActionName("Register")]
        public async Task<IActionResult> RegisterPost(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    UserName = registerModel.UserName,
                    Email = registerModel.Email,
                    BirthDate = registerModel.BirthDate
                };

                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {
                    var resultSignIn = await _signInManager.PasswordSignInAsync(registerModel.UserName, registerModel.Password, registerModel.RememberMe, false);

                    if (resultSignIn.Succeeded)
                    { 
                        return RedirectToAction("Index", "Product");  
                    }
                } 
            }

            return View();
        }

        public IActionResult Update() 
        { 
            var user = _repository.GetUserByUserName(User.Identity.Name); 

            var updateUser = new RegisterViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                UserName = user.UserName,
            };

            return View(updateUser);
        }

        [HttpPost, ActionName("Update")]
        public IActionResult UpdatePost(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = _repository.GetUserByUserName(registerModel.UserName); 

                user.FirstName = registerModel.FirstName;
                user.LastName = registerModel.LastName;
                user.BirthDate = registerModel.BirthDate;
                user.Email = registerModel.Email;

                _repository.SaveChanges();

                return RedirectToAction("Index", "Product");
            }

            return View();
        }
    }
}