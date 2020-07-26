using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using MVCMeTieneJarto.Models;

namespace MVCMeTieneJarto.Controllers
{
    public class EmpleadosController : Controller
    {
        private DbModel db = new DbModel();
        private StoredProceures SP = new StoredProceures();

        // GET: Empleados
        public ActionResult Index()
        {
            var empleados = db.Empleados.Include(e => e.Cargos).Include(e => e.Departamentos);
            return View(empleados.ToList());
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.Cargo_ID = new SelectList(db.Cargos, "ID", "Cargo");
            ViewBag.Departamento_ID = new SelectList(db.Departamentos, "Codigo", "Nombre");
            return View();
        }

        // POST: Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Apellido,Cedula,Salario,Fecha_ingreso,Telefono,Email,Cargo_ID,Departamento_ID")] Empleados empleados)
        {
            if (ModelState.IsValid)
            { 
                db.Empleados.Add(empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.Cargo_ID = new SelectList(db.Cargos, "ID", "Cargo", empleados.Cargo_ID);
            ViewBag.Departamento_ID = new SelectList(db.Departamentos, "Codigo", "Nombre", empleados.Departamento_ID);
            return View(empleados);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cargo_ID = new SelectList(db.Cargos, "ID", "Cargo", empleados.Cargo_ID);
            ViewBag.Departamento_ID = new SelectList(db.Departamentos, "Codigo", "Nombre", empleados.Departamento_ID);
            return View(empleados);
        }

        // POST: Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,Apellido,Cedula,Salario,Fecha_ingreso,Telefono,Email,Cargo_ID,Departamento_ID")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cargo_ID = new SelectList(db.Cargos, "ID", "Cargo", empleados.Cargo_ID);
            ViewBag.Departamento_ID = new SelectList(db.Departamentos, "Codigo", "Nombre", empleados.Departamento_ID);
            return View(empleados);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleados = db.Empleados.Find(id);
            db.Empleados.Remove(empleados);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CalcularNomina ()
        {
            return View();
        }
        public ActionResult Nomina()
        {
            return View(db.Nomina.ToList());
        }
        public ActionResult generate()
        {
            int year = int.Parse(Request.Form["year"]);
            int month = int.Parse(Request.Form["mes"]);

            
            var result = db.Database.ExecuteSqlCommand("EXEC GetSalaries @year, @month", new SqlParameter("year", SqlDbType.Int) { Value = year },
                new SqlParameter("month", SqlDbType.Int) { Value = month });
            db.SaveChanges();
            return RedirectToAction("Nomina");
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
