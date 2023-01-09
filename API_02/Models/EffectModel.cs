using NivelAccesDate_ORM_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_02.Models
{
    public class EffectModel
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
    }
}