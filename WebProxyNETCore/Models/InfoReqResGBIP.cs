using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProxyNETCore.Models
{
    public class InfoReqResGBIP
    {
        public string IP { get; set; }
        public int Bloqueado { get; set; }
        public int Permitido { get; set; }
        public int Limitado { get; set; }
        public int Errado { get; set; }

        public InfoReqResGBIP(BsonDocument doc)
        {
            Bloqueado = doc.GetValue("Bloqueado").AsInt32;
            Permitido = doc.GetValue("Permitido").AsInt32;
            Limitado = doc.GetValue("Limitado").AsInt32;
            Errado = doc.GetValue("Errado").AsInt32;
            IP = doc.GetValue("_id").AsString;
        }

    }
}
