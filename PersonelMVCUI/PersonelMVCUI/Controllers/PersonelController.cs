using PersonelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using PersonelMVCUI.ViewModels;

namespace PersonelMVCUI.Controllers
{
    public class PersonelController : Controller
    {


        PersonelDbEntities db = new PersonelDbEntities();
        // GET: Personel
        public ActionResult Index()
        {

            //var model = db.Personel.ToList(); (lazyLoading True) --> Her elemanda sorgu yolluyor

            var model = db.Personel.Include(d => d.Departman).ToList(); // ==>  (lazyLoading False) EagerLoading --> SQLe tek sorgu gidiyor 


            return View(model);
        }

        public ActionResult Yeni()
        {

            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personel = new Personel()
            };

            return View("PersonelForm", model);
        }

        public ActionResult Kaydet(Personel personel)
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman,
                Personel = personel
            };
            if (!ModelState.IsValid)
            {
                return View("PersonelForm");
            }

            if (personel.Id == 0) //Ekleme işlemi yapılmak isteniyor
            {
                db.Personel.Add(personel);
            }
            else //Guncelleme
            {
                db.Entry(personel).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Guncelle(int id)
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personel = db.Personel.Find(id)
            };


            return View("PersonelForm",model);
        }

        public ActionResult Sil(int id)
        {
            var silinecekId = db.Personel.Find(id);
            if (silinecekId == null)
            {
               return HttpNotFound();
            }

            db.Personel.Remove(silinecekId);
            db.SaveChanges();

            return RedirectToAction("Index");

        }


    }
}