using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProxyNETCore.Models
{
    public class ConfiguracionProxyModel
    {
        public ObjectId Id { get; set; }

        public string Key { get; set; }
        public bool Bloqueada { get; set; }
        public int TipoContador { get; set; }

    }
}
