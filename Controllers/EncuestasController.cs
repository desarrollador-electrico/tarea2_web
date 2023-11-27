using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AriasRomanJonathan_Tarea2;

namespace AriasRomanJonathan_Tarea2.Controllers
{
    public class EncuestasController : Controller
    {
        private EncuestaTecnologiasEntities db = new EncuestaTecnologiasEntities();

      // GET: Encuestas
      public ActionResult Index()
        {
            var encuestas = db.Encuestas.Include(e => e.LenguajesProgramacion).Include(e => e.LenguajesProgramacion1).Include(e => e.Paises).Include(e => e.RolesTI);
            return View(encuestas.ToList());
        }

        // GET: Encuestas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encuestas encuestas = db.Encuestas.Find(id);
            if (encuestas == null)
            {
                return HttpNotFound();
            }
            return View(encuestas);
        }

        // GET: Encuestas/Create
        public ActionResult Create()
        {
            ViewBag.LenguajePrimarioID = new SelectList(db.LenguajesProgramacion, "LenguajeID", "Nombre");
            ViewBag.LenguajeSecundarioID = new SelectList(db.LenguajesProgramacion, "LenguajeID", "Nombre");
            ViewBag.PaisID = new SelectList(db.Paises, "PaisID", "Nombre");
            ViewBag.RolID = new SelectList(db.RolesTI, "RolID", "Descripcion");
            return View();
        }

        // POST: Encuestas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EncuestaID,Nombre,Apellidos,PaisID,RolID,LenguajePrimarioID,LenguajeSecundarioID")] Encuestas encuestas)
        {
            Reportes reportes = new Reportes(); // Objeto local para actualizar tabla
            if (ModelState.IsValid)
            {
               // Aca inicia el modelado
                ActualizaReporte(db, (int)encuestas.LenguajePrimarioID, (int)encuestas.LenguajeSecundarioID); // Llama al metodo
                db.Encuestas.Add(encuestas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LenguajePrimarioID = new SelectList(db.LenguajesProgramacion, "LenguajeID", "Nombre", encuestas.LenguajePrimarioID);
            ViewBag.LenguajeSecundarioID = new SelectList(db.LenguajesProgramacion, "LenguajeID", "Nombre", encuestas.LenguajeSecundarioID);
            ViewBag.PaisID = new SelectList(db.Paises, "PaisID", "Nombre", encuestas.PaisID);
            ViewBag.RolID = new SelectList(db.RolesTI, "RolID", "Descripcion", encuestas.RolID);
            return View(encuestas);
        }

        // GET: Encuestas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encuestas encuestas = db.Encuestas.Find(id);
            if (encuestas == null)
            {
                return HttpNotFound();
            }
            ViewBag.LenguajePrimarioID = new SelectList(db.LenguajesProgramacion, "LenguajeID", "Nombre", encuestas.LenguajePrimarioID);
            ViewBag.LenguajeSecundarioID = new SelectList(db.LenguajesProgramacion, "LenguajeID", "Nombre", encuestas.LenguajeSecundarioID);
            ViewBag.PaisID = new SelectList(db.Paises, "PaisID", "Nombre", encuestas.PaisID);
            ViewBag.RolID = new SelectList(db.RolesTI, "RolID", "Descripcion", encuestas.RolID);
            return View(encuestas);
        }

        // POST: Encuestas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EncuestaID,Nombre,Apellidos,PaisID,RolID,LenguajePrimarioID,LenguajeSecundarioID")] Encuestas encuestas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(encuestas).State = EntityState.Modified;
                //ActualizaReporte("Estoy en Post del Edit Id");
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LenguajePrimarioID = new SelectList(db.LenguajesProgramacion, "LenguajeID", "Nombre", encuestas.LenguajePrimarioID);
            ViewBag.LenguajeSecundarioID = new SelectList(db.LenguajesProgramacion, "LenguajeID", "Nombre", encuestas.LenguajeSecundarioID);
            ViewBag.PaisID = new SelectList(db.Paises, "PaisID", "Nombre", encuestas.PaisID);
            ViewBag.RolID = new SelectList(db.RolesTI, "RolID", "Descripcion", encuestas.RolID);
            return View(encuestas);
        }

        // GET: Encuestas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encuestas encuestas = db.Encuestas.Find(id);
            if (encuestas == null)
            {
                return HttpNotFound();
            }
            return View(encuestas);
        }

        // POST: Encuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Encuestas encuestas = db.Encuestas.Find(id);
            db.Encuestas.Remove(encuestas);
            //ActualizaReporte("Estoy en Eliminar por Id");
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

      // Metodo de modelado
      public void ActualizaReporte(EncuestaTecnologiasEntities dbo, int lenguaje1Id, int lenguaje2Id)
      {
         if (ModelState.IsValid)
         {
            Reportes reportePrimario = dbo.Reportes.Find(lenguaje1Id);
            Reportes reporteSecundario = dbo.Reportes.Find(lenguaje2Id);

            // Logica para validar lenguaje primario
            if (reportePrimario == null)
            {
               // Logica para crear nuevo reporte
               reportePrimario = new Reportes(); // Crea nuevo objeto
               // Actualizacion de datos
               reportePrimario.LenguajeID = lenguaje1Id;
               reportePrimario.NumeroDePosicion = 0;
               reportePrimario.ClasificacionPorcentual = 1;
               reportePrimario.DiferenciaPorcentual = 0;
               dbo.Reportes.Add(reportePrimario);

            } // Final del if
            else
            {
               // Logica para actualizar un reporte existente
               Debug.WriteLine("\nLogica para actualizar un reporte existente\n");
               reportePrimario.ClasificacionPorcentual += 1; // Incrementa en unidad
               dbo.Entry(reportePrimario).State = EntityState.Modified;

            } // Final if-else

            // Logica para validar lenguaje secundario
            if (reporteSecundario == null)
            {
               // Logica para crear nuevo reporte
               reporteSecundario = new Reportes(); // Crea nuevo objeto
               // Actualizacion de datos
               reporteSecundario.LenguajeID = lenguaje2Id;
               reporteSecundario.NumeroDePosicion = 0;
               reporteSecundario.ClasificacionPorcentual = 0.5;
               reporteSecundario.DiferenciaPorcentual = 0;
               dbo.Reportes.Add(reporteSecundario);

            } // Final del if
            else
            {
               // Logica para actualizar un reporte existente
               Debug.WriteLine("\nLogica para actualizar un reporte existente\n");
               reporteSecundario.ClasificacionPorcentual += 0.5; // Incrementa en unidad
               dbo.Entry(reporteSecundario).State = EntityState.Modified;

            } // Final if-else

            // Realiza ordenamiento descendente
            var reportes = dbo.Reportes.Include(r => r.LenguajesProgramacion); // Carga DB
            reportes = reportes.OrderByDescending(x => x.ClasificacionPorcentual); // Ordena y lista
            int contador = 1; // Ranking de posicion

            foreach(var reporte in reportes)
            {
               reporte.NumeroDePosicion = contador; // Actualiza posicion
               dbo.Entry(reporte).State = EntityState.Modified;
               contador++; // Incrementa

            } // Final foreach

            dbo.SaveChanges();

         } // Final if valida estado

      } // Final del metodo






   } // Final del controlador
} // Final del namespace
