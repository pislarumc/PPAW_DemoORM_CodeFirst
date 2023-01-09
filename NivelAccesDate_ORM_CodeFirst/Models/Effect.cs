using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NivelAccesDate_ORM_CodeFirst.Models
{
    [Serializable]
    public class Effect
    {
        public string EffectName { get; set; }
        public float Cost { get; set; }
        public string EffectId { get; set; }
        public bool Deleted { get; set; }
        public string CssProperty { get; set; }
        public int PropertyValue { get; set; }
        public int PropertyRangeMin { get; set; }
        public int PropertyRangeMax { get; set; }
        public string PropertyUnit { get; set; }
        public ICollection<History> Histories { get; set; }
    }
}
