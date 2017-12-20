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

        public ActionResult Index(string IP, string URI, string GroupByIP, string GroupByURI, int startIndex, int pageSize  )
        {
            IP = string.IsNullOrEmpty(IP) ? "" : IP.Trim();
            URI = string.IsNullOrEmpty(URI) ? "" : URI.Trim();

            var Items = _comunicador.Get(
                (x =>
                (x.IP == "" || x.IP == IP) &&
                (x.URI == "" || x.URI == URI)));

            return View(Items);
        }



        //// GET: InfoReqRes/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: InfoReqRes/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: InfoReqRes/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: InfoReqRes/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: InfoReqRes/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: InfoReqRes/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: InfoReqRes/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}