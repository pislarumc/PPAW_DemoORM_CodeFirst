using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_01.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        public bool Subscribed { get; set; }
        public IEnumerable<SelectListItem> Roles
        {
            get
            {
                List<string> roles = new List<string> { "Utilizator", "Administrator" };
                return
                    roles.Select(role =>
                        new SelectListItem
                        {
                            Text = role,
                            Value = role,
                            Selected = role.Equals(Role)
                        });
            }
        }

    }
}