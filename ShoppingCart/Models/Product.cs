using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class Product 
    {
        [Key]
        public int ProductId { get; set; } 

        public virtual User User { get; set; }

        [Required(ErrorMessage = "Please enter a product title")] 
        [Display(Name = "כותרת:")] 
        [StringLength(50)] 
        public string Title { get; set; } 

        [Required(ErrorMessage = "Please enter a short product description")]
        [Display(Name = "תיאור קצר:")]
        [StringLength(500)] 
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Please enter a long product description")] 
        [Display(Name = "תיאור ארוך:")]
        [StringLength(4000)] 
        public string LongDescription { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        public DateTime PostDate { get; set; } 

        [Required(ErrorMessage = "Please enter a product price")]
        [DataType(DataType.Currency)]
        [Display(Name = "מחיר:")] 
        public double Price { get; set; }

        [NotMapped]
        [Display(Name = "תמונה 1:")] 
        public IFormFile PhotoAvatar1 { get; set; }

        public string ImageName1 { get; set; }

        public byte[] PhotoFile1 { get; set; }

        public string ImageMimeType1 { get; set; }

        [NotMapped]
        [Display(Name = "תמונה 2:")]
        public IFormFile PhotoAvatar2 { get; set; }

        public string ImageName2 { get; set; }

        public byte[] PhotoFile2 { get; set; }

        public string ImageMimeType2 { get; set; }

        [NotMapped]
        [Display(Name = "תמונה 3:")] 
        public IFormFile PhotoAvatar3 { get; set; }

        public string ImageName3 { get; set; }

        public byte[] PhotoFile3 { get; set; }

        public string ImageMimeType3 { get; set; }

        public bool IsForSale { get; set; } 


        public Product()
        {
            PostDate = DateTime.Now;   
        } 
    }
}
