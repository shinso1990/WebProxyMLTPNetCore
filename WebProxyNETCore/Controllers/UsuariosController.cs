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
    public class UsuariosController : Controller
    {
        private readonly IMongoDBService _mongoDBService;
        private ComunicadorMongoDB<UsuariosModel> _comunicador;

        public UsuariosController(IMongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
            _comunicador = _mongoDBService.GetInstance<UsuariosModel>(_mongoDBService.CollectionUsuarios);
        }

        // GET: UsuariosModels
        public IActionResult Index()
        {
            return View( _comunicador.Get(x=> true ) );
        }

        // GET: UsuariosModels/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosModel = _comunicador.Get(m => m.User == id).FirstOrDefault();
            if (usuariosModel == null)
            {
                return NotFound();
            }

            return View(usuariosModel);
        }

        // GET: UsuariosModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsuariosModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("User,Password")] UsuariosModel usuariosModel)
        {
            if (ModelState.IsValid)
            {
                _comunicador.Insert(usuariosModel);
                return RedirectToAction(nameof(Index));
            }
            return View(usuariosModel);
        }

        // GET: UsuariosModels/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosModel = _comunicador.Get(m => m.User == id).FirstOrDefault();
            if (usuariosModel == null)
            {
                return NotFound();
            }
            return View(usuariosModel);
        }

        // POST: UsuariosModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("User,Password")] UsuariosModel usuariosModel)
        {
            if (id != usuariosModel.User)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = _comunicador.Get(x => x.User == usuariosModel.User).FirstOrDefault();
                    usuariosModel.Id = user.Id;
                    _comunicador.ReplaceOne( x=> x.User == usuariosModel.User, usuariosModel);
                }
                catch (Exception e)
                {
                    throw e;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuariosModel);
        }

        // GET: UsuariosModels/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuariosModel = _comunicador.Get(m => m.User == id).FirstOrDefault();
            if (usuariosModel == null)
            {
                return NotFound();
            }

            return View(usuariosModel);
        }

        // POST: UsuariosModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            _comunicador.DeleteOne(x=> x.User == id);
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosModelExists(string id)
        {
            return _comunicador.Get(e => e.User == id).Count > 0;
        }
    }
}
