﻿using System;
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
    public class RawMaterialsController : Controller
    {
        private Entities db = new Entities();

        // GET: RawMaterials
        public ActionResult Index()
        {
            return View(db.RawMaterials.ToList());
        }

        // GET: RawMaterials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterial rawMaterial = db.RawMaterials.Find(id);
            if (rawMaterial == null)
            {
                return HttpNotFound();
            }
            return View(rawMaterial);
        }

        // GET: RawMaterials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RawMaterials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaterialId,MaterialName,MaterialStandardCost,UnitOfMeasure")] RawMaterial rawMaterial)
        {
            if (ModelState.IsValid)
            {
                db.RawMaterials.Add(rawMaterial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rawMaterial);
        }

        // GET: RawMaterials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterial rawMaterial = db.RawMaterials.Find(id);
            if (rawMaterial == null)
            {
                return HttpNotFound();
            }
            return View(rawMaterial);
        }

        // POST: RawMaterials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaterialId,MaterialName,MaterialStandardCost,UnitOfMeasure")] RawMaterial rawMaterial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rawMaterial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rawMaterial);
        }

        // GET: RawMaterials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterial rawMaterial = db.RawMaterials.Find(id);
            if (rawMaterial == null)
            {
                return HttpNotFound();
            }
            return View(rawMaterial);
        }

        // POST: RawMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RawMaterial rawMaterial = db.RawMaterials.Find(id);
            db.RawMaterials.Remove(rawMaterial);
            Supply supplymaterial = db.Supplies.OrderByDescending(p => p.MaterialId).FirstOrDefault();
            db.Supplies.Remove(supplymaterial);
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
