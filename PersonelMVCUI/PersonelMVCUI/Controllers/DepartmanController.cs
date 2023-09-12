using PersonelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonelMVCUI.Controllers
{
    public class DepartmanController : Controller
    {

        PersonelDbEntities db = new PersonelDbEntities();


        // GET: Departman
        public ActionResult Index()
        {
            var model = db.Departman.ToList();

            return View(model);
        }
        [ValidateAntiForgeryToken]
        public ActionResult Yeni()
        {
          
            return View("DepartmanForm" ,new Departman());
        }
        [HttpPost]
        public ActionResult Kaydet(Departman departman)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmanForm");
            }
            if (departman.Id == 0)
            {
                db.Departman.Add(departman);

            }
            else
            {
                var guncellenecekdepartman = db.Departman.Find(departman.Id);
                if (guncellenecekdepartman == null)
                {
                    return HttpNotFound();
                }
                guncellenecekdepartman.Ad = departman.Ad;
            }
            //db.Departman.Add(departman);
            db.SaveChanges();
            return RedirectToAction("Index","Departman");
        }

        public ActionResult Guncelle(int id)
        {
            var model = db.Departman.Find(id);
            if (model  == null)
            {
                return HttpNotFound();
            }
            return View("DepartmanForm",model);
        }
        public ActionResult Sil(int id)
        {
            var silinecekId = db.Departman.Find(id);
            if (silinecekId == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.Departman.Remove(silinecekId);
            }
            db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}