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
    public class OrdenesController : Controller
    {
        private DBPrepagadaEntities db = new DBPrepagadaEntities();
        

        // GET: Ordenes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordenes ordenes = db.Ordenes.Find(id);
            if (ordenes == null)
            {
                return HttpNotFound();
            }
            return View(ordenes);
        }

        // GET: Ordenes/Create
        public ActionResult Create()
        {
            ViewBag.id_ips = new SelectList(db.IPS, "id_ips", "nombre");
            ViewBag.id_membresia = new SelectList(db.Membresias, "id_membresia", "descripcion");
            ViewBag.id_paciente = new SelectList(db.Pacientes, "id_paciente", "nombre");
            ViewBag.id_servicio = new SelectList(db.Servicios.Where(p => p.habilitado), "id_servicio", "descripcion");
            return View();
        }

        // POST: Ordenes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_orden,id_paciente,id_servicio,id_ips,id_membresia")] Ordenes ordenes)
        {

            ordenes.estado = "pendiente";
            ordenes.fecha_solicitud = DateTime.Now;
            ordenes.valor_copago = 0;
            ordenes.valor_cuota_moderadora = 0;
            db.Ordenes.Add(ordenes);
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = ordenes.id_orden });
        }

        // GET: Ordenes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordenes ordenes = db.Ordenes.Find(id);
            if (ordenes == null)
            {
                return HttpNotFound();
            }
            return View(ordenes);
        }

        // POST: Ordenes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            try
            {
                var ordenes = db.Ordenes.Find(id);
                var servicios = db.Ordenes.Where(p => p.id_paciente == ordenes.id_paciente && p.estado == "aprobada");
                var condicion = db.Condiciones.FirstOrDefault(p => p.id_servicio == p.id_servicio);
                if (ordenes.GetReglasValidacion(condicion, servicios.Count()).Count() > 0)
                {
                    ordenes.estado = "rechazada";
                }
                else
                {
                    var negociacion = db.Negociaciones.LastOrDefault(p => p.id_ips == ordenes.id_ips && p.id_servicio == ordenes.id_servicio && p.habilitado);
                    ordenes.estado = "aprobada";
                    ordenes.valor_copago = negociacion.valor * ordenes.Membresias.porcentaje_copago;
                    ordenes.valor_copago = negociacion.valor * ordenes.Membresias.porcentaje_cuota_moderadora;

                }
                db.Entry(ordenes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Edit", new { id = id });
            }
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
