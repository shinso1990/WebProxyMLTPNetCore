using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProxyNETCore.Models
{
    public class UsuariosModel
    {

        public ObjectId Id { get; set; }

        [Key]
        public string User { get; set; }

        public string Password { get; set; }


        public UsuariosModel() { }

    }
}
