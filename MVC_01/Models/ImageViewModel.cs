using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_01.Models
{
    public class ImageViewModel
    {
        public string ImageId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Dimensions { get; set; }
        // foreign key 
        public virtual UserViewModel User { get; set; }

        public string UserIDFK { get; set; }

        public IEnumerable<SelectListItem> Users
        {
            get;
            set;        
        }

    }
}