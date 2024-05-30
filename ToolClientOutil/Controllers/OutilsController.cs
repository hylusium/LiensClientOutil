using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tool.Data;

namespace ToolClientOutil.Controllers
{
    public class OutilsController : Controller
    {
        private ToolClientOutilEntities db = new ToolClientOutilEntities();

        // GET: Outils
        public ActionResult Index()
        {
            return View(db.Outil.ToList());
        }

        // GET: Outils/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outil outil = db.Outil.Find(id);
            if (outil == null)
            {
                return HttpNotFound();
            }
            return View(outil);
        }

        // GET: Outils/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Outils/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOutil,NomOutil,VersionOutil,DateMaj,état,Commentaire")] Outil outil)
        {
            if (ModelState.IsValid)
            {
                db.Outil.Add(outil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(outil);
        }

        // GET: Outils/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outil outil = db.Outil.Find(id);
            if (outil == null)
            {
                return HttpNotFound();
            }
            return View(outil);
        }

        // POST: Outils/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOutil,NomOutil,VersionOutil,DateMaj,état,Commentaire")] Outil outil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(outil);
        }

        // GET: Outils/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outil outil = db.Outil.Find(id);
            if (outil == null)
            {
                return HttpNotFound();
            }
            return View(outil);
        }

        // POST: Outils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Outil outil = db.Outil.Find(id);
            db.Outil.Remove(outil);
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
