using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProxyNETCore.Models
{
    public class ConfiguracionGeneralModel
    {
        //public ObjectId Id { get; set; }

        [Key]
        public string IdString { get; set; }

        public string Key { get; set; }

        public Dictionary<string, string> Values { get; set; }
    }
}
