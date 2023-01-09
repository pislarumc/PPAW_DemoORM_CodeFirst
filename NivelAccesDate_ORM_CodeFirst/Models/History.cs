using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelAccesDate_ORM_CodeFirst.Models
{
    [Serializable]
    public class History
    {
        public DateTime LoggingTime { get; set; }
        public string HistoryId { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string EffectId { get; set; }

        public string ImageId { get; set; }
        public User User { get; set; }
        public Effect Effect { get; set; }
        public Image Image { get; set; }
    }
}
