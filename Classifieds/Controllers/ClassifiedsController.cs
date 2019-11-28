﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Classifieds.Models;

namespace Classifieds.Controllers
{
    public class ClassifiedsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /
        [Route("/")]
        public async Task<ActionResult> Index()
        {
            return View(await db.ClassifiedAds.ToListAsync());
        }

        // GET: Classifieds/5
        [Route("/Classifieds/{id}")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassifiedAd classifiedAd = await db.ClassifiedAds.FindAsync(id);
            if (classifiedAd == null)
            {
                return HttpNotFound();
            }
            return View(classifiedAd);
        }

        // GET: ClassifiedAds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClassifiedAds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClassifiedAd classifiedAd)
        {
            if (ModelState.IsValid)
            {
                db.ClassifiedAds.Add(classifiedAd);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(classifiedAd);
        }
        /*
        // GET: ClassifiedAds/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassifiedAd classifiedAd = await db.ClassifiedAds.FindAsync(id);
            if (classifiedAd == null)
            {
                return HttpNotFound();
            }
            return View(classifiedAd);
        }

        // POST: ClassifiedAds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TypeId,Title,Description,Created,Expires")] ClassifiedAd classifiedAd)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classifiedAd).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(classifiedAd);
        }

        // GET: ClassifiedAds/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassifiedAd classifiedAd = await db.ClassifiedAds.FindAsync(id);
            if (classifiedAd == null)
            {
                return HttpNotFound();
            }
            return View(classifiedAd);
        }

        // POST: ClassifiedAds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ClassifiedAd classifiedAd = await db.ClassifiedAds.FindAsync(id);
            db.ClassifiedAds.Remove(classifiedAd);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        */
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