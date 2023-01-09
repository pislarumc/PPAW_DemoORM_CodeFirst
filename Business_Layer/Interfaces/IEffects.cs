using NivelAccesDate_ORM_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces
{
    public interface IEffects
    {
        List<Effect> GetEffects();
        Effect GetEffect(string id);
        bool AddEffect(Effect effect);

        bool UpdateEffect(string id, Effect effect);

        bool DeleteToggleEffect(string id, bool deleted);
    }
}
