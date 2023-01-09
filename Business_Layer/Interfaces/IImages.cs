using NivelAccesDate_ORM_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces
{
    public interface IImages
    {
        List<Image> GetImages();
        Image GetImage(string id);
        bool AddImage(Image image);

        bool UpdateImage(string id, Image image);

        bool DeleteToggleImage(string id, bool deleted);
        List<string> GetImageUsers();
    }
}
