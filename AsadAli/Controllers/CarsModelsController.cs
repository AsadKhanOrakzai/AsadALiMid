using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AsadAli.Models;

namespace AsadAli.Controllers
{
    public class CarsModelsController : Controller
    {
        private CarsDBContext db = new CarsDBContext();

        // GET: CarsModels
        public ActionResult Index(string Model, string searchString)
        {
            var CarLst = new List<string>();

            var ModelQry = from d in db.cars
                           orderby d.Model
                          select d.Model;

            CarLst.AddRange(ModelQry.Distinct());
            ViewBag.Model = new SelectList(CarLst);

            var cars = from m in db.cars
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(s => s.Name.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(Model))
            {
                cars = cars.Where(x => x.Model == Model);
            }

            return View(cars);
        }

        // GET: CarsModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarsModel carsModel = db.cars.Find(id);
            if (carsModel == null)
            {
                return HttpNotFound();
            }
            return View(carsModel);
        }

        // GET: CarsModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarsModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,ReleaseDate,Model,Price")] CarsModel carsModel)
        {
            if (ModelState.IsValid)
            {
                db.cars.Add(carsModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carsModel);
        }

        // GET: CarsModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarsModel carsModel = db.cars.Find(id);
            if (carsModel == null)
            {
                return HttpNotFound();
            }
            return View(carsModel);
        }

        // POST: CarsModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,ReleaseDate,Model,Price")] CarsModel carsModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carsModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carsModel);
        }

        // GET: CarsModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarsModel carsModel = db.cars.Find(id);
            if (carsModel == null)
            {
                return HttpNotFound();
            }
            return View(carsModel);
        }

        // POST: CarsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarsModel carsModel = db.cars.Find(id);
            db.cars.Remove(carsModel);
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
    }
}
