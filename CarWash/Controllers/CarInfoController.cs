using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarWash.Models;
using CarWash.Models.Cars;
using System.Data.Entity.SqlServer;

namespace CarWash.Controllers
{
    [Authorize]
    public class CarInfoController : Controller
    {
        private CarWashContext db = new CarWashContext();

        // GET: CarInfoe
        public async Task<ActionResult> Index()
        {
            var nowDate = DateTime.Now.ToShortDateString();
            return View(await db.CarInfos
                .OrderBy(x => x.ArrivalTime)
                .Where(x => x.Status != "Rezerwacja")
                .ToListAsync());
        }
        public async Task<ActionResult> IndexAll()
        {
            return View(await db.CarInfos
                .OrderByDescending(x => x.ArrivalTime)
                .Where(x => x.Status != "Rezerwacja")
                .ToListAsync());
        }

        // GET: CarInfoe/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarInfo carInfo = await db.CarInfos.FindAsync(id);
            if (carInfo == null)
            {
                return HttpNotFound();
            }
            return View(carInfo);
        }

        // GET: CarInfoe/Create
        public ActionResult Create()
        {
            #region WashType
            List<SelectListItem> washtype = new List<SelectListItem>();

            washtype.Add(new SelectListItem { Text = "Mycie", Value = "Mycie" });
            washtype.Add(new SelectListItem { Text = "Odkurzanie", Value = "Odkurzanie" });
            washtype.Add(new SelectListItem { Text = "Standard", Value = "Standard", Selected = true });
            washtype.Add(new SelectListItem { Text = "Optimum", Value = "Optimum" });
            washtype.Add(new SelectListItem { Text = "Full", Value = "Full" });

            ViewBag.Wash = washtype;
            #endregion

            #region CarColor
            List<SelectListItem> carcolor = new List<SelectListItem>();

            carcolor.Add(new SelectListItem { Text = "Czarny", Value = "Czarny" });
            carcolor.Add(new SelectListItem { Text = "Czerwony", Value = "Czerwony" });
            carcolor.Add(new SelectListItem { Text = "Pomarańczowy", Value = "Pomarańczowy" });
            carcolor.Add(new SelectListItem { Text = "Żółty", Value = "Żółty" });
            carcolor.Add(new SelectListItem { Text = "Zielony", Value = "Zielony" });
            carcolor.Add(new SelectListItem { Text = "Cyjan", Value = "Cyjan" });
            carcolor.Add(new SelectListItem { Text = "Niebieski", Value = "Niebieski" });
            carcolor.Add(new SelectListItem { Text = "Purpurowy", Value = "Purpurowy" });
            carcolor.Add(new SelectListItem { Text = "Biały", Value = "Biały" });
            carcolor.Add(new SelectListItem { Text = "Granatowy", Value = "Granatowy" });
            carcolor.Add(new SelectListItem { Text = "Brązowy", Value = "Brązowy" });
            carcolor.Add(new SelectListItem { Text = "Czary", Value = "Czary" });
            carcolor.Add(new SelectListItem { Text = "Srebny", Value = "Srebny" });

            ViewBag.Color = carcolor;
            #endregion

            #region CarType
            List<SelectListItem> cartype = new List<SelectListItem>();

            cartype.Add(new SelectListItem { Text = "Sedan", Value = "Sedan" });
            cartype.Add(new SelectListItem { Text = "Hatchback", Value = "Hatchback" });
            cartype.Add(new SelectListItem { Text = "Kombi", Value = "Kombi" });
            cartype.Add(new SelectListItem { Text = "Sportowe/coupe", Value = "Sportowe/coupe" });
            cartype.Add(new SelectListItem { Text = "Kabriolet", Value = "Kabriolet" });
            cartype.Add(new SelectListItem { Text = "Limuzyna", Value = "Limuzyna" });
            cartype.Add(new SelectListItem { Text = "Pickup", Value = "Pickup" });
            cartype.Add(new SelectListItem { Text = "Terenowe", Value = "Terenowe" });
            cartype.Add(new SelectListItem { Text = "Van/minibus", Value = "Van/minibus" });
            cartype.Add(new SelectListItem { Text = "SUV", Value = "SUV" });

            ViewBag.Type = cartype;
            #endregion

            #region TypeOfPayment
            List<SelectListItem> payment = new List<SelectListItem>();

            payment.Add(new SelectListItem { Text = "Gotówka", Value = "Gotówka" });
            payment.Add(new SelectListItem { Text = "Karta", Value = "Karta" });

            ViewBag.TypeOfPayment = payment;
            #endregion
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CarId,Model,Color,ArrivalTime,PickUpTime,WashType,Cost,TypeOfCar,Faktura,Payment")] CarInfo carInfo)
        {
            if (ModelState.IsValid)
            {
                carInfo.ArrivalTime = DateTime.Now;
                carInfo.Status = "Oczekujące";
                //carInfo.PickUpTime = null;
                db.CarInfos.Add(carInfo);
                await db.SaveChangesAsync();
                return RedirectToAction("CarWait");
            }

            return View(carInfo);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarInfo carInfo = await db.CarInfos.FindAsync(id);
            if (carInfo == null)
            {
                return HttpNotFound();
            }
            #region TypeOfPayment
            List<SelectListItem> payment = new List<SelectListItem>();

            payment.Add(new SelectListItem { Text = "Gotówka", Value = "Gotówka" });
            payment.Add(new SelectListItem { Text = "Karta", Value = "Karta" });

            ViewBag.TypeOfPayment = payment;
            #endregion
            return View(carInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CarId,Model,Color,ArrivalTime,PickUpTime,WashType,Cost,PaidConfirmed,TakeConfirmed,TypeOfCar,Status,Faktura,Payment")] CarInfo carInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carInfo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(carInfo);
        }

        // GET: CarInfoe/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarInfo carInfo = await db.CarInfos.FindAsync(id);
            if (carInfo == null)
            {
                return HttpNotFound();
            }
            return View(carInfo);
        }

        // POST: CarInfoe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CarInfo carInfo = await db.CarInfos.FindAsync(id);
            db.CarInfos.Remove(carInfo);
            await db.SaveChangesAsync();
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

        [HttpGet]
        public async Task<ActionResult> CarWait()
        {
            return View(await db.CarInfos.Where(x => x.Status == "Oczekujące").OrderBy(x => x.ArrivalTime).ToListAsync());
        }
        [HttpPost]
        public JsonResult InWait(int id)
        {
            try
            {
                CarInfo car = db.CarInfos.Find(id);
                if (car == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }
                car.Status = "W pracy";
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public async Task<ActionResult> CarWash()
        {
            return View(await db.CarInfos.Where(x => x.Status == "W pracy").OrderBy(x => x.ArrivalTime).ToListAsync());
        }
        public JsonResult InWash(int id)
        {
            try
            {
                CarInfo car = db.CarInfos.Find(id);
                if (car == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }
                car.Status = "Gotowe";
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public async Task<ActionResult> CarFinished()
        {
            return View(await db.CarInfos.Where(x => x.Status == "Gotowe").OrderBy(x => x.ArrivalTime).ToListAsync());
        }
        public JsonResult InFinish(int id)
        {
            try
            {
                CarInfo car = db.CarInfos.Find(id);
                if (car == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }
                car.Status = "Oddane";
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        public async Task<ActionResult> EditFinish(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarInfo carInfo = await db.CarInfos.FindAsync(id);
            if (carInfo == null)
            {
                return HttpNotFound();
            }
            #region TypeOfPayment
            List<SelectListItem> payment = new List<SelectListItem>();

            payment.Add(new SelectListItem { Text = "Gotówka", Value = "Gotówka" });
            payment.Add(new SelectListItem { Text = "Karta", Value = "Karta" });

            ViewBag.TypeOfPayment = payment;
            #endregion
            return View(carInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditFinish([Bind(Include = "CarId,Model,WashType,ArrivalTime,PickUpTime,Cost,Faktura,Color,TypeOfCar,Payment")] CarInfo carInfo)
        {
            if (ModelState.IsValid)
            {
                carInfo.TakeConfirmed = true;
                carInfo.PaidConfirmed = true;
                carInfo.PickUpTime = DateTime.Now;
                carInfo.Status = "Oddane";
                db.Entry(carInfo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(carInfo);
        }

        public ActionResult CreateRezerwation()
        {
            #region WashType
            List<SelectListItem> washtype = new List<SelectListItem>();

            washtype.Add(new SelectListItem { Text = "Mycie", Value = "Mycie" });
            washtype.Add(new SelectListItem { Text = "Odkurzanie", Value = "Odkurzanie" });
            washtype.Add(new SelectListItem { Text = "Standard", Value = "Standard", Selected = true });
            washtype.Add(new SelectListItem { Text = "Optimum", Value = "Optimum" });
            washtype.Add(new SelectListItem { Text = "Full", Value = "Full" });

            ViewBag.Wash = washtype;
            #endregion

            #region CarColor
            List<SelectListItem> carcolor = new List<SelectListItem>();

            carcolor.Add(new SelectListItem { Text = "Czarny", Value = "Czarny" });
            carcolor.Add(new SelectListItem { Text = "Czerwony", Value = "Czerwony" });
            carcolor.Add(new SelectListItem { Text = "Pomarańczowy", Value = "Pomarańczowy" });
            carcolor.Add(new SelectListItem { Text = "Żółty", Value = "Żółty" });
            carcolor.Add(new SelectListItem { Text = "Zielony", Value = "Zielony" });
            carcolor.Add(new SelectListItem { Text = "Cyjan", Value = "Cyjan" });
            carcolor.Add(new SelectListItem { Text = "Niebieski", Value = "Niebieski" });
            carcolor.Add(new SelectListItem { Text = "Purpurowy", Value = "Purpurowy" });
            carcolor.Add(new SelectListItem { Text = "Biały", Value = "Biały" });
            carcolor.Add(new SelectListItem { Text = "Brązowy", Value = "Brązowy" });
            carcolor.Add(new SelectListItem { Text = "Czary", Value = "Czary" });
            carcolor.Add(new SelectListItem { Text = "Srebny", Value = "Srebny" });

            ViewBag.Color = carcolor;
            #endregion

            #region CarType
            List<SelectListItem> cartype = new List<SelectListItem>();

            cartype.Add(new SelectListItem { Text = "Sedan", Value = "Sedan" });
            cartype.Add(new SelectListItem { Text = "Hatchback", Value = "Hatchback" });
            cartype.Add(new SelectListItem { Text = "Kombi", Value = "Kombi" });
            cartype.Add(new SelectListItem { Text = "Sportowe/coupe", Value = "Sportowe/coupe" });
            cartype.Add(new SelectListItem { Text = "Kabriolet", Value = "Kabriolet" });
            cartype.Add(new SelectListItem { Text = "Limuzyna", Value = "Limuzyna" });
            cartype.Add(new SelectListItem { Text = "Pickup", Value = "Pickup" });
            cartype.Add(new SelectListItem { Text = "Terenowe", Value = "Terenowe" });
            cartype.Add(new SelectListItem { Text = "Van/minibus", Value = "Van/minibus" });
            cartype.Add(new SelectListItem { Text = "SUV", Value = "SUV" });

            ViewBag.Type = cartype;
            #endregion
            CarInfo car = new CarInfo();
            DateTime defaulttime = DateTime.Now;
            car.ArrivalTime = defaulttime;
            ViewData.Model = car;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRezerwation([Bind(Include = "CarId,Model,Color,WashType,ArrivalTime,Cost,TypeOfCar,Faktura")] CarInfo carInfo)
        {
            if (ModelState.IsValid)
            {
                carInfo.Status = "Rezerwacja";
                db.CarInfos.Add(carInfo);
                await db.SaveChangesAsync();
                return RedirectToAction("IndexRezerwation");
            }

            return View(carInfo);
        }

        public async Task<ActionResult> IndexRezerwation()
        {
            return View(await db.CarInfos
                .Where(x => x.Status == "Rezerwacja")
                .OrderBy(x => x.ArrivalTime)
                .ToListAsync());
        }

        public JsonResult Rezerwacja(int id)
        {
            try
            {
                CarInfo car = db.CarInfos.Find(id);
                if (car == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }
                car.Payment = "Gotówka";
                car.Status = "Oczekujące";
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public async Task<ActionResult> Historia()
        {
            return View(await db.CarInfos
                .OrderByDescending(x => x.ArrivalTime)
                .Where(x => x.Status != "Rezerwacja")
                .ToListAsync());
        }
    }
}
