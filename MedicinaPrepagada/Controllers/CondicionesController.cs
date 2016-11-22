using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicinaPrepagada.Models.DBMedicinaPrepagada.Source;

namespace MedicinaPrepagada.Controllers
{
    public class CondicionesController : Controller
    {
        private DBPrepagadaEntities db = new DBPrepagadaEntities();

        // GET: Condiciones
        public ActionResult Index()
        {
            var condiciones = db.Condiciones.Include(c => c.Servicios);
            return View(condiciones.ToList());
        }

        // GET: Condiciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condiciones condiciones = db.Condiciones.Find(id);
            if (condiciones == null)
            {
                return HttpNotFound();
            }
            return View(condiciones);
        }

        // GET: Condiciones/Create
        public ActionResult Create()
        {
            ViewBag.id_servicio = new SelectList(db.Servicios, "id_servicio", "descripcion");
            return View();
        }

        // POST: Condiciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_condicion,id_servicio,edad_minima,edad_maxima,dias_afiliacion,cantidad_maxima_servicios,genero")] Condiciones condiciones)
        {
            if (ModelState.IsValid)
            {
                db.Condiciones.Add(condiciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_servicio = new SelectList(db.Servicios, "id_servicio", "descripcion", condiciones.id_servicio);
            return View(condiciones);
        }

        // GET: Condiciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condiciones condiciones = db.Condiciones.Find(id);
            if (condiciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_servicio = new SelectList(db.Servicios, "id_servicio", "descripcion", condiciones.id_servicio);
            return View(condiciones);
        }

        // POST: Condiciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_condicion,id_servicio,edad_minima,edad_maxima,dias_afiliacion,cantidad_maxima_servicios,genero")] Condiciones condiciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(condiciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_servicio = new SelectList(db.Servicios, "id_servicio", "descripcion", condiciones.id_servicio);
            return View(condiciones);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
