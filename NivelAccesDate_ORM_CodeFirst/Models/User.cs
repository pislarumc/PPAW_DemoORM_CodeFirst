using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NivelAccesDate_ORM_CodeFirst.Models
{
    [Serializable]
    public class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        public bool Subscribed { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<History> Histories { get; set; }
    }
}
