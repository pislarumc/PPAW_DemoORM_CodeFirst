using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_02.Models
{
    public class ImageModel
    {
        public string ImageId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Dimensions { get; set; }
        public bool Deleted { get; set; }
        public string UserId { get; set; }

        // foreign key 
        public virtual UserModel UserModel { get; set; }

        //for creating new images
        public List<string> Users_list{get; set;}
    }
}