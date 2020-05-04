using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class User : IdentityUser<long>
    { 
        [Required(ErrorMessage = "Please enter first name")]
        [Display(Name = "First name:")] 
        [StringLength(50)] 
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        [Display(Name = "Last name:")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter birth date")]
        [Display(Name = "Birth date:")] 
        [DataType(DataType.DateTime)] 
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Product> ProductsList { get; set; }

        public User()
        {
            ProductsList = new List<Product>();
        } 
    }
}
