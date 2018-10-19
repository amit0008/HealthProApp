using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthPro2.Models;

namespace HealthPro2.Controllers
{
    public class FoodPlansController : Controller
    {
        private Model1 db = new Model1();

        // GET: FoodPlans
        public ActionResult Index()
        {
            return View(db.FoodPlanInfoes.Where(x => x.UserId == User.Identity.Name || User.Identity.Name == "admin@gmail.com").ToList());
        }

        // GET: FoodPlans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodPlan foodPlan = db.FoodPlans.Find(id);
            if (foodPlan == null)
            {
                return HttpNotFound();
            }
            return View(foodPlan);
        }

        // GET: FoodPlans/Create
        public ActionResult Create()
        {
            ViewBag.FoodId = new SelectList(db.Foods, "FoodId", "FoodName");
            return View();
        }

        // POST: FoodPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Model1 m1)
        {
            if (ModelState.IsValid)
            {
                if (m1.FoodId[0] != m1.FoodId[1] && m1.FoodId[0] != m1.FoodId[2] && m1.FoodId[1] != m1.FoodId[2])
                {
                    FoodPlan fp = new FoodPlan();
                    fp.PlanName = m1.PlanName;
                    fp.StartDate = m1.StartDate;
                    fp.EndDate = m1.EndDate;
                    db.FoodPlans.Add(fp);
                    db.SaveChanges();

                    int currentPlanId = fp.PlanId;
                    FoodPlanInfo fi = new FoodPlanInfo();
                    fi.FoodId = m1.FoodId[0];
                    fi.PlanId = currentPlanId;
                    fi.Quantity = m1.Quantity1;
                    fi.UserId = User.Identity.Name;
                    db.FoodPlanInfoes.Add(fi);
                    db.SaveChanges();

                    FoodPlanInfo fi2 = new FoodPlanInfo();
                    fi2.FoodId = m1.FoodId[1];
                    fi2.PlanId = currentPlanId;
                    fi2.Quantity = m1.Quantity2;
                    fi2.UserId = User.Identity.Name;
                    db.FoodPlanInfoes.Add(fi2);
                    db.SaveChanges();

                    FoodPlanInfo fi3 = new FoodPlanInfo();
                    fi3.FoodId = m1.FoodId[2];
                    fi3.PlanId = currentPlanId;
                    fi3.Quantity = m1.Quantity3;
                    fi3.UserId = User.Identity.Name;
                    db.FoodPlanInfoes.Add(fi3);
                    db.SaveChanges();


                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }

            return View(m1);
        }

        // GET: FoodPlans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodPlan foodPlan = db.FoodPlans.Find(id);
            if (foodPlan == null)
            {
                return HttpNotFound();
            }
            return View(foodPlan);
        }

        // POST: FoodPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlanId,PlanName,StartDate,EndDate")] FoodPlan foodPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(foodPlan);
        }

        // GET: FoodPlans/Delete/5
        public ActionResult Delete(int? id , int? id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodPlan foodPlan = db.FoodPlans.Find(id);
            if (foodPlan == null)
            {
                return HttpNotFound();
            }
            return View(foodPlan);
        }

        
        // POST: FoodPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            
            while (true)
            {
                var user = (from i in db.FoodPlanInfoes where i.PlanId == id select i).FirstOrDefault();
                if(user == null)
                { break; }
                int newid = user.FoodId;
                FoodPlanInfo foodInfo = db.FoodPlanInfoes.Find(id, newid);
                db.FoodPlanInfoes.Remove(foodInfo);
                db.SaveChanges();

            }
                FoodPlan foodPlan = db.FoodPlans.Find(id);
                db.FoodPlans.Remove(foodPlan);
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
