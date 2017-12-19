using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProxyNETCore.Models;
using WebProxyNETCore.Service;
using WebProxyNETCore.DataAccess;

namespace WebProxyNETCore.Controllers
{
    public class ConfiguracionProxyController : Controller
    {
        private readonly IMongoDBService _mongoDBService;
        private ComunicadorMongoDB<ConfiguracionProxyModel> _comunicador;

        public ConfiguracionProxyController(IMongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
            _comunicador = _mongoDBService.GetInstance<ConfiguracionProxyModel>(_mongoDBService.CollectionConfigProxy);
        }
        // GET: ConfiguracionProxy
        public IActionResult Index()
        {
            return View( _comunicador.Get(x=> true) );
        }

        // GET: ConfiguracionProxy/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracionProxyModel =  _comunicador.Get(m => m.Key == id).FirstOrDefault();
            if (configuracionProxyModel == null)
            {
                return NotFound();
            }

            return View(configuracionProxyModel);
        }

        // GET: ConfiguracionProxy/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConfiguracionProxy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdString,Key,Bloqueada,TipoContador")] ConfiguracionProxyModel configuracionProxyModel)
        {
            if (ModelState.IsValid)
            {
                _comunicador.Insert(configuracionProxyModel);
                return RedirectToAction(nameof(Index));
            }
            return View(configuracionProxyModel);
        }

        // GET: ConfiguracionProxy/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracionProxyModel = _comunicador.Get(m => m.Key == id).FirstOrDefault();

            if (configuracionProxyModel == null)
            {
                return NotFound();
            }
            return View(configuracionProxyModel);
        }

        // POST: ConfiguracionProxy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("IdString,Key,Bloqueada,TipoContador")] ConfiguracionProxyModel configuracionProxyModel)
        {
            if (id != configuracionProxyModel.Key )
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var itemExistente = _comunicador.Get(x => x.Key == configuracionProxyModel.Key).FirstOrDefault();
                    configuracionProxyModel.Id = itemExistente.Id;
                    _comunicador.ReplaceOne(x=> x.Key ==   configuracionProxyModel.Key, configuracionProxyModel);

                }
                catch (Exception e)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(configuracionProxyModel);
        }

        // GET: ConfiguracionProxy/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracionProxyModel = _comunicador.Get(m => m.Key == id).FirstOrDefault();
            if (configuracionProxyModel == null)
            {
                return NotFound();
            }

            return View(configuracionProxyModel);
        }

        // POST: ConfiguracionProxy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var configuracionProxyModel = _comunicador.DeleteOne(m => m.Key == id);
            return RedirectToAction(nameof(Index));
        }

        private bool ConfiguracionProxyModelExists(string id)
        {
            return _comunicador.Get(e => e.Key == id).Count > 0 ;
        }
    }
}
