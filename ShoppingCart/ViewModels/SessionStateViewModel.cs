using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.ViewModels
{
    public class SessionStateViewModel
    {
        public List<Product> SelectedProducts { get; set; } 
    }
}
