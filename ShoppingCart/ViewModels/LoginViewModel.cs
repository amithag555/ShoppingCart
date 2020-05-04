using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "שם משתמש:")]  
        [Required(ErrorMessage = "Please enter your user name.")]
        public string UserName { get; set; }

        [Display(Name = "סיסמא:")]
        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)] 
        public string Password { get; set; }

        [Display(Name = "זכור אותי")] 
        public bool RememberMe { get; set; } 
    }
}
