using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class PhotoHelper
    {
        public IFormFile PhotoAvatar { get; set; }

        public string ImageName { get; set; }

        public byte[] PhotoFile { get; set; }

        public string ImageMimeType { get; set; }

        public PhotoHelper(IFormFile photoAvatar, string imageName, byte[] photoFile, string imageMimeType)
        {
            PhotoAvatar = photoAvatar;
            ImageName = imageName;
            PhotoFile = photoFile;
            ImageMimeType = imageMimeType;
        } 
    }
}
