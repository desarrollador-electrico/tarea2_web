using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AriasRomanJonathan_Tarea2;

namespace AriasRomanJonathan_Tarea2.Controllers
{
    public class ReportesController : Controller
    {
        private EncuestaTecnologiasEntities db = new EncuestaTecnologiasEntities();

      // GET: Reportes
      public ActionResult Index()
      {
         var reportes = db.Reportes.Include(r => r.LenguajesProgramacion);
         reportes = reportes.OrderBy(x => x.ClasificacionPorcentual); // Ordena y lista
         int tamano = reportes.Count();
         int contador = 0; // Ranking de posicion
         float porcentaje_anterior = 0;

         foreach (var reporte in reportes)
         {
            reporte.NumeroDePosicion = tamano - contador; // Actualiza posicion

            if(contador == 0)
            {
               reporte.DiferenciaPorcentual = 0; // Cero dado que no existe dato anterior
            }
            else
            {
               reporte.DiferenciaPorcentual = reporte.ClasificacionPorcentual - porcentaje_anterior;
            } // Final del if-else

            porcentaje_anterior = (float)reporte.ClasificacionPorcentual;
            contador++; // Incrementa

         } // Final foreach

         reportes = reportes.OrderByDescending(x => x.ClasificacionPorcentual); // Ordena y lista

         return View(reportes.ToList());
      }

        
        // POST: Reportes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LenguajeID,NumeroDePosicion,ClasificacionPorcentual,DiferenciaPorcentual")] Reportes reportes)
        {
            if (ModelState.IsValid)
            {
                db.Reportes.Add(reportes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LenguajeID = new SelectList(db.LenguajesProgramacion, "LenguajeID", "Nombre", reportes.LenguajeID);
            return View(reportes);
        }

        // GET: Reportes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reportes reportes = db.Reportes.Find(id);
            if (reportes == null)
            {
                return HttpNotFound();
            }
            ViewBag.LenguajeID = new SelectList(db.LenguajesProgramacion, "LenguajeID", "Nombre", reportes.LenguajeID);
            return View(reportes);
        }

        // POST: Reportes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LenguajeID,NumeroDePosicion,ClasificacionPorcentual,DiferenciaPorcentual")] Reportes reportes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LenguajeID = new SelectList(db.LenguajesProgramacion, "LenguajeID", "Nombre", reportes.LenguajeID);
            return View(reportes);
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
