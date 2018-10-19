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
    public class ChartController : Controller
    {
        private Model1 db = new Model1();


        public ActionResult GetfoodPlanInfo()
        {
            
            var data = db.FoodPlanInfoes.Where(x => x.UserId == User.Identity.Name).ToList();
            
            String[] planName = data.Select(x => x.FoodPlan.PlanName).Distinct().ToArray();
            int[] totalCalories = new int[planName.Length];
            int[] Array1 = new int[planName.Length];
            int[] Array2 = new int[planName.Length];
            int[] Array3 = new int[planName.Length];
            String[] Food1 = new String[planName.Length];
            String[] Food2 = new String[planName.Length];
            String[] Food3 = new String[planName.Length];

            for (int i = 0; i < planName.Length; i++)
            {
                int caloriesTotal = data.Where(x => x.FoodPlan.PlanName == planName[i]).Sum(x => x.Food.Calories * x.Quantity);
                totalCalories[i] = caloriesTotal;
                int[] calories = data.Where(x => x.FoodPlan.PlanName == planName[i]).Select(x => x.Food.Calories * x.Quantity).ToArray();
                Array1[i] = calories[0];
                Array2[i] = calories[1];
                Array3[i] = calories[2];
                String[] foodName = data.Where(x => x.FoodPlan.PlanName == planName[i]).Select(x => x.Food.FoodName).ToArray();
                Food1[i] = foodName[0];
                Food2[i] = foodName[1];
                Food3[i] = foodName[2];
            }
            
            Graph obj = new Graph();
            obj.PlanName = planName;
            obj.TotalCalories = totalCalories;
            obj.Array1 = Array1;
            obj.Array2 = Array2;
            obj.Array3 = Array3;
            obj.Food1 = Food1;
            obj.Food2 = Food2;
            obj.Food3 = Food3;
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        public class Graph
        {
            public int[] TotalCalories { get; set; }
            public int[] Array1 { get; set; }
            public int[] Array2 { get; set; }
            public int[] Array3 { get; set; }
            public String[] PlanName { get; set; }
            public String[] Food1 { get; set; }
            public String[] Food2 { get; set; }
            public String[] Food3 { get; set; }



        }

        // GET: Chart
        public ActionResult Index()
        {
            return View(db.FoodPlanInfoes.Where(x => x.UserId == User.Identity.Name || User.Identity.Name == "admin@gmail.com").ToList());
        }

        // GET: Chart/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodPlanInfo foodPlanInfo = db.FoodPlanInfoes.Find(id);
            if (foodPlanInfo == null)
            {
                return HttpNotFound();
            }
            return View(foodPlanInfo);
        }

        // GET: Chart/Create
        public ActionResult Create()
        {
            ViewBag.FoodId = new SelectList(db.Foods, "FoodId", "FoodName");
            ViewBag.PlanId = new SelectList(db.FoodPlans, "PlanId", "PlanName");
            return View();
        }

        // POST: Chart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlanId,FoodId,UserId,Quantity")] FoodPlanInfo foodPlanInfo)
        {
            if (ModelState.IsValid)
            {
                db.FoodPlanInfoes.Add(foodPlanInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FoodId = new SelectList(db.Foods, "FoodId", "FoodName", foodPlanInfo.FoodId);
            ViewBag.PlanId = new SelectList(db.FoodPlans, "PlanId", "PlanName", foodPlanInfo.PlanId);
            return View(foodPlanInfo);
        }

        // GET: Chart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodPlanInfo foodPlanInfo = db.FoodPlanInfoes.Find(id);
            if (foodPlanInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.FoodId = new SelectList(db.Foods, "FoodId", "FoodName", foodPlanInfo.FoodId);
            ViewBag.PlanId = new SelectList(db.FoodPlans, "PlanId", "PlanName", foodPlanInfo.PlanId);
            return View(foodPlanInfo);
        }

        // POST: Chart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlanId,FoodId,UserId,Quantity")] FoodPlanInfo foodPlanInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodPlanInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FoodId = new SelectList(db.Foods, "FoodId", "FoodName", foodPlanInfo.FoodId);
            ViewBag.PlanId = new SelectList(db.FoodPlans, "PlanId", "PlanName", foodPlanInfo.PlanId);
            return View(foodPlanInfo);
        }

        // GET: Chart/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodPlanInfo foodPlanInfo = db.FoodPlanInfoes.Find(id);
            if (foodPlanInfo == null)
            {
                return HttpNotFound();
            }
            return View(foodPlanInfo);
        }

        // POST: Chart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodPlanInfo foodPlanInfo = db.FoodPlanInfoes.Find(id);
            db.FoodPlanInfoes.Remove(foodPlanInfo);
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
