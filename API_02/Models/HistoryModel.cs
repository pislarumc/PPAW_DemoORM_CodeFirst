using NivelAccesDate_ORM_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_02.Models
{
    public class HistoryModel
    {
        public DateTime LoggingTime { get; set; }
        public string HistoryId { get; set; }
        public string Description { get; set; }
        public string EffectId { get; set; }
        public string UserId { get; set; }
        public string ImageId { get; set; }
        public UserModel UserModel { get; set; }
        public EffectModel EffectModel { get; set; }
        public ImageModel ImageModel { get; set; }
    }
}