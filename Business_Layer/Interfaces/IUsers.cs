using NivelAccesDate_ORM_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces
{
    public interface IUsers
    {
        List<User> GetUsers();
        User GetUser(string id);
        bool AddUser(User user);

        //bool UpdateUser(User user);
        bool UpdateUser(string id, User user);

        //bool DeleteUser(User user);
        bool DeleteToggleUser(string id, bool deleted);
    }
}
