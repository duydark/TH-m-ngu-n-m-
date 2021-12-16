using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {

        private Models.ModelHocVien dc = new Models.ModelHocVien();
        // GET: Home
        public ActionResult Index()
        {
            return View(dc.lyliches);
        }
        public ActionResult xemChitiethocvien(string id)
        {
            Models.lylich hv = dc.lyliches.Find(id);
            return View(hv);
        }
        public ActionResult formThemhocvien()
        {
            ViewBag.DSLop = dc.lops.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult themHocVien(Models.lylich hv)
        {
            if (ModelState.IsValid)
            {
                if (dc.lyliches.Find(hv.mshv) == null)
                {
                    dc.lyliches.Add(hv);
                    dc.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult formSuaHocVien(string id)
        {
            ViewBag.DSLop = dc.lops.ToList();
            Models.lylich hv = dc.lyliches.Find(id);
            return View(hv);
        }
        [HttpPost]
        public ActionResult SuaHocVien(Models.lylich hv)
        {
            if (ModelState.IsValid)
            {
                Models.lylich x = dc.lyliches.Find(hv.mshv);
                x.tenhv = hv.tenhv;
                x.ngaysinh = hv.ngaysinh;
                x.phai = hv.phai;
                x.malop = hv.malop;
                dc.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult formXoaHocVien(string id)
        {
            Models.lylich hv = dc.lyliches.Find(id);

            ViewBag.CotheXoa = true;
            if (dc.diemthis.Where(x => x.mshv == id).Count() > 0)
            {
                ViewBag.CotheXoa = false;
            }
            return View(hv);
        }

        public ActionResult XoaHocVien(string id)
        {
            Models.lylich hv = dc.lyliches.Find(id);
            dc.lyliches.Remove(hv);
            dc.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}