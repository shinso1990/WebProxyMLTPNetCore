using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProxyNETCore.Models
{
    public class ConfiguracionProxyModel
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Key { get; set; }
        public bool Bloqueada { get; set; }
        public int TipoContador { get; set; }
        public int CantidadTope { get; set; }

    }
}
