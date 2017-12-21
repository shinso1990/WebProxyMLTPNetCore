using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProxyNETCore.Models;
using WebProxyNETCore.Service;
using WebProxyNETCore.DataAccess;
using MongoDB.Bson;

namespace WebProxyNETCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Estadisticas")]
    public class EstadisticasController : Controller
    {
        private readonly IMongoDBService _mongoDBService;
        private readonly IRedisService _redisService;
        private ComunicadorMongoDB<InfoReqResModel> _comunicador;

        public EstadisticasController(IMongoDBService mongoDBService, IRedisService redisService)
        {
            _mongoDBService = mongoDBService;
            _comunicador = _mongoDBService.GetInstance<InfoReqResModel>(_mongoDBService.CollectionInfoReqRes );
            _redisService = redisService;
        }

        // GET: api/Estadisticas
        [HttpGet]
        public IEnumerable<InfoReqResModel> Get(string IP, string URI)
        {
            IP = string.IsNullOrEmpty(IP) ? "" : IP;
            URI = string.IsNullOrEmpty(URI) ? "" : URI;

            var res = new Paginacion<InfoReqResModel>();

            res.StartIndex = 0;
            res.PageSize = 15;
            System.Linq.Expressions.Expression<Func<InfoReqResModel, bool>> filter = x =>
            (IP == "" || x.IP == IP) &&
            (URI == "" || x.URI == URI);

            

            return  _comunicador.Get(filter);
            //var itemsTotalCount = _comunicador.Count(filter);

            //res.ItemsCount = itemsTotalCount;

            //return res;
        }

        //GET: api/Estadisticas/5
        [HttpGet("GBIP", Name = "GetGBIP")]
        public IEnumerable<InfoReqResGBIP> GroupByIP(string IP, DateTime? FechaDesde, DateTime? FechaHasta)
        {
            return _comunicador.groupByIP(IP).Select(x=> new InfoReqResGBIP(x));
        }

        [HttpGet("GBURI", Name = "GetGBURI")]
        public IEnumerable<InfoReqResGBURI> GroupByURI(string URI, DateTime? FechaDesde, DateTime? FechaHasta)
        {
            return _comunicador.groupByURI(URI).Select(x => new InfoReqResGBURI(x));
        }

        //public Paginacion<InfoReqResModel> Get(string IP, string URI, DateTime? FechaDesde, DateTime? FechaHasta)
        //{
        //    IP = string.IsNullOrEmpty(IP) ? "" : IP;
        //    URI = string.IsNullOrEmpty(URI) ? "" : URI;

        //    var res = new Paginacion<InfoReqResModel>();

        //    res.StartIndex = 0;
        //    res.PageSize = 15;
        //    System.Linq.Expressions.Expression<Func<InfoReqResModel, bool>> filter = x=>
        //    (IP == "" || x.IP==IP  ) &&
        //    (URI == "" || x.URI == URI) &&
        //    ( (!FechaDesde.HasValue) || x.Fecha >= FechaDesde.Value ) &&
        //    ( (!FechaHasta.HasValue) || x.Fecha <= FechaHasta.Value  );

        //    var items = _comunicador.Get( filter, res.StartIndex, res.PageSize);
        //    var itemsTotalCount = _comunicador.Count(filter);

        //    res.ItemsCount = itemsTotalCount;

        //    return res;
        //}



    }
}
