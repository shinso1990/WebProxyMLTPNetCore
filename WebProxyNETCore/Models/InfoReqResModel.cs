﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProxyNETCore.Models
{
    public class InfoReqResModel
    {
        [Key]
        [BsonId]
        public ObjectId Id { get; set; }
        
        public string Key { get; set; }

        public string IP { get; set; }
        public string URI { get; set; }

        public int ResponseStatus { get; set; }

        public int Permitido { get; set; }
        public int Bloqueado { get; set; }
        public int Limitado { get; set; }
        public int Errado { get; set; }


        public DateTime Fecha { get; set; }

        public string TipooContadorIP { get; set; }
        public string TipooContadorURI { get; set; }
        public string TipooContadorIPURI { get; set; }

        public bool TieneAcceso { get; set; }
        public bool HayError { get; set; }

        public string CausaLimiteOBloqueo { get; set; }
        public string MensajeError { get; set; }

        public string ExceptionMessage { get; set; }
    }
}
