using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD.Db.Data;
using CRUD.Models;
using CRUD.Reports;

namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        EmployeeData _data = new EmployeeData();

        public HomeController()
        {
            //Session["user"] = null;
        }

        #region View

        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                return View("List");
            }

            return RedirectToAction("Login", "Account");
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Create()
        {
            if (Session["user"] != null)
            {
                return View();
            }

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Update(long id)
        {
            if (Session["user"] != null)
            {
                return View(id);
            }

            return RedirectToAction("Login", "Account");
        }

        #endregion

        #region List

        public JsonResult GetAll()
        {
            return Json(_data.GetAll(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create

        [HttpPost]
        public JsonResult Create(Employee employee)
        {
            return Json(_data.Create(employee), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Update

        [HttpPost]
        public JsonResult Get(long id)
        {
            return Json(_data.Get(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(Employee employee)
        {
            return Json(_data.Update(employee), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Delete

        [HttpPost]
        public JsonResult Delete(Employee employee)
        {
            return Json(_data.Delete(employee), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Report

        public object Report()
        {
            if (Session["user"] != null)
            {
                List<Employee> employees = _data.GetAll();
                string reportPath = Path.Combine(Server.MapPath("~/Reports"), "EmployeeList.rpt");
                return new ReportGenerator(reportPath, employees);
            }

            return RedirectToAction("Login", "Account");
        }

        #endregion
    }
}