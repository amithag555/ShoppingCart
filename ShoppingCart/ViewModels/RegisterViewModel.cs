using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.ViewModels
{
    public class RegisterViewModel : LoginViewModel
    {
        [Display(Name = "שם פרטי:")]
        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; } 

        [Display(Name = "שם משפחה:")]
        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }

        [Display(Name = "אימות סיסמא:")] 
        [Required(ErrorMessage = "Please enter again your password")] 
        [DataType(DataType.Password)]
        [CompareAttribute("Password", ErrorMessage = "The passwords required to be equal")] 
        public string passwordVerification { get; set; } 

        [Required(ErrorMessage = "Please enter birth date")]
        [Display(Name = "תאריך לידה:")]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Please enter your email")] 
        [Display(Name = "אימייל:")] 
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                            ErrorMessage = "Please enter a valid email address")] 
        public string Email { get; set; } 
    }
}
