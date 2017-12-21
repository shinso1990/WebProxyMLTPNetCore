using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProxyNETCore.Service;
using WebProxyNETCore.DataAccess;
using WebProxyNETCore.Models;

namespace WebProxyNETCore.Controllers
{
    public class InfoReqResController : Controller
    {
        private readonly IMongoDBService _mongoDBService;
        private readonly IRedisService _redisService;
        private ComunicadorMongoDB<InfoReqResModel> _comunicador;

        public InfoReqResController(IMongoDBService mongoDBService, IRedisService redisService)
        {
            _mongoDBService = mongoDBService;
            _comunicador = _mongoDBService.GetInstance<InfoReqResModel>(_mongoDBService.CollectionInfoReqRes );
            _redisService = redisService;
        }

        // GET: InfoReqRes
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index(string IP, string URI)
        {
            

            var listado1 = _comunicador.groupByIP(IP).Select(x => new InfoReqResGBIP(x) ).ToList();
            var listado2 = _comunicador.groupByURI(URI).Select(x => new InfoReqResGBURI(x)).ToList();

            return View(new ListadoGBIpUri() { GBIP = listado1, GBURI = listado2 } );
        }

    }

    public class ListadoGBIpUri
    {
        public List<InfoReqResGBIP> GBIP { get; set; }
        public List<InfoReqResGBURI> GBURI { get; set; }
    }

}