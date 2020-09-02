using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using S2G5_PVFAPP.Models;

namespace S2G5_PVFAPP.Controllers
{
    public class SuppliesController : Controller
    {
        private Entities db = new Entities();

        // GET: Supplies
        public ActionResult Index()
        {
            var supplies = db.Supplies.Include(s => s.RawMaterial).Include(s => s.Vendor);
            return View(supplies.ToList());
        }

        // GET: Supplies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supply supply = db.Supplies.OrderByDescending(x => x.VendorId == id).FirstOrDefault();
            if (supply == null)
            {
                return HttpNotFound();
            }
            return View(supply);
        }

        // GET: Supplies/Create
        public ActionResult Create()
        {
            ViewBag.MaterialId = new SelectList(db.RawMaterials, "MaterialId", "MaterialName");
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorName");
            return View();
        }

        // POST: Supplies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VendorId,MaterialId,SupplyUnitPrice")] Supply supply)
        {
            if (ModelState.IsValid)
            {
                db.Supplies.Add(supply);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaterialId = new SelectList(db.RawMaterials, "MaterialId", "MaterialName", supply.MaterialId);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorName", supply.VendorId);
            return View(supply);
        }

        // GET: Supplies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Supply supply = db.Supplies.Find(id);
            Supply supply = db.Supplies.OrderByDescending(x => x.VendorId == id).FirstOrDefault();

            if (supply == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaterialId = new SelectList(db.RawMaterials, "MaterialId", "MaterialName", supply.MaterialId);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorName", supply.VendorId);
            return View(supply);
        }

        // POST: Supplies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VendorId,MaterialId,SupplyUnitPrice")] Supply supply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaterialId = new SelectList(db.RawMaterials, "MaterialId", "MaterialName", supply.MaterialId);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorName", supply.VendorId);
            return View(supply);
        }

        // GET: Supplies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supply supply = db.Supplies.OrderByDescending(x => x.VendorId == id).FirstOrDefault();
            if (supply == null)
            {
                return HttpNotFound();
            }
            return View(supply);
        }

        // POST: Supplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Supply supply = db.Supplies.Find(id);
            Supply supply = db.Supplies.OrderByDescending(x => x.VendorId == id).FirstOrDefault();
            db.Supplies.Remove(supply);
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
