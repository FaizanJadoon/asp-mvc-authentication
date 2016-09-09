using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthEg.Models;

namespace AuthEg.Controllers
{
    public class EmployeeController : Controller
    {
        private organizationEntities db = new organizationEntities();

        // GET: Employee
        [Authorize(Roles = "v,a")]
        public ActionResult Index()
        {
            var tbl_Emp = db.tbl_Emp.Include(t => t.tbl_Dept);
            return View(tbl_Emp.Take(50).ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Emp tbl_Emp = db.tbl_Emp.Find(id);
            if (tbl_Emp == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Emp);
        }

        // GET: Employee/Create
        [Authorize(Roles = "a")]
        public ActionResult Create()
        {
            ViewBag.Did = new SelectList(db.tbl_Dept, "Did", "Dname");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Eid,Ename,Esal,EGen,EDOB,Did")] tbl_Emp tbl_Emp)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Emp.Add(tbl_Emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Did = new SelectList(db.tbl_Dept, "Did", "Dname", tbl_Emp.Did);
            return View(tbl_Emp);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Emp tbl_Emp = db.tbl_Emp.Find(id);
            if (tbl_Emp == null)
            {
                return HttpNotFound();
            }
            ViewBag.Did = new SelectList(db.tbl_Dept, "Did", "Dname", tbl_Emp.Did);
            return View(tbl_Emp);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Eid,Ename,Esal,EGen,EDOB,Did")] tbl_Emp tbl_Emp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Emp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Did = new SelectList(db.tbl_Dept, "Did", "Dname", tbl_Emp.Did);
            return View(tbl_Emp);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Emp tbl_Emp = db.tbl_Emp.Find(id);
            if (tbl_Emp == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Emp);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Emp tbl_Emp = db.tbl_Emp.Find(id);
            db.tbl_Emp.Remove(tbl_Emp);
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
