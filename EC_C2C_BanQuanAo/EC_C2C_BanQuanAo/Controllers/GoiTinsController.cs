using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EC_C2C_BanQuanAo;
using EC_C2C_BanQuanAo.Filter;

namespace EC_C2C_BanQuanAo.Controllers
{
    [BasicAuthFilter]
    public class GoiTinsController : Controller
    {
        private EC_C2C_BanQuanAoDBContext db = new EC_C2C_BanQuanAoDBContext();

        // GET: GoiTins
        public ActionResult Index()
        {
            return View(db.GoiTins.ToList());
        }

        // GET: GoiTins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoiTin goiTin = db.GoiTins.Find(id);
            if (goiTin == null)
            {
                return HttpNotFound();
            }
            return View(goiTin);
        }

        // GET: GoiTins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GoiTins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGoi,TenGoi,SoLuongTin,Gia")] GoiTin goiTin)
        {
            if (ModelState.IsValid)
            {
                db.GoiTins.Add(goiTin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goiTin);
        }

        // GET: GoiTins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoiTin goiTin = db.GoiTins.Find(id);
            if (goiTin == null)
            {
                return HttpNotFound();
            }
            return View(goiTin);
        }

        // POST: GoiTins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGoi,TenGoi,SoLuongTin,Gia")] GoiTin goiTin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goiTin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goiTin);
        }

        // GET: GoiTins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoiTin goiTin = db.GoiTins.Find(id);
            if (goiTin == null)
            {
                return HttpNotFound();
            }
            return View(goiTin);
        }

        // POST: GoiTins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GoiTin goiTin = db.GoiTins.Find(id);
            db.GoiTins.Remove(goiTin);
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
