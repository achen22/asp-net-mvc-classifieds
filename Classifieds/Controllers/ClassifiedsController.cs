using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Classifieds.Models;
using Microsoft.AspNet.Identity;

namespace Classifieds.Controllers
{
    public class ClassifiedsController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: /
        [Route("/")]
        public async Task<ActionResult> Index()
        {
            var ads = await _db.ClassifiedAds.AsNoTracking()
                .Include(c => c.Type)
                .Include(c => c.User.ContactInfo)
                .ToListAsync();
            return View(ads);
        }

        // GET: Classifieds/5
        [Route("/Classifieds/{id}")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassifiedAd classifiedAd = await _db.ClassifiedAds.AsNoTracking()
                .Include(c => c.Type)
                .Include(c => c.User.ContactInfo)
                .SingleOrDefaultAsync(c => c.Id == id);
            if (classifiedAd == null)
            {
                return HttpNotFound();
            }
            return View(classifiedAd);
        }

        // GET: ClassifiedAds/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(_db.ClassifiedTypes, "Id", "Name");
            return View();
        }

        // POST: ClassifiedAds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClassifiedAdViewModel model)
        {
            if (ModelState.IsValid)
            {
                var expireDate = model.ExpireDate ?? DateTime.Today.AddDays(30);
                if (model.ExpireTime == null)
                {
                    expireDate = expireDate.AddDays(1);
                }
                var expireTime = model.ExpireTime ?? DateTime.Today;
                var classifiedAd = new ClassifiedAd()
                {
                    UserId = User.Identity.GetUserId(),
                    TypeId = model.TypeId,
                    Title = model.Title,
                    Description = model.Description,
                    Created = DateTime.Now,
                    Expires = expireDate
                        .AddHours(expireTime.Hour)
                        .AddMinutes(expireTime.Minute)
                        .AddSeconds(expireTime.Second)
                };
                _db.ClassifiedAds.Add(classifiedAd);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TypeId = new SelectList(_db.ClassifiedTypes, "Id", "Name");
            return View(model);
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
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
