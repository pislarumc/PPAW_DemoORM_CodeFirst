using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelAccesDate_ORM_CodeFirst.Models
{
    [Serializable]
    public class Image
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Dimensions { get; set; }
        public string ImageId { get; set; }
        public bool Deleted { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<History> Histories { get; set; }
    }
}
