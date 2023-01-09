using NivelAccesDate_ORM_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces
{
    public interface IHistories
    {
        List<History> GetHistories();
        History GetHistory(string id);
        bool AddHistory(History history);

        bool UpdateHistory(string id, History history);

        bool DeleteHistory(string id);
    }
}
