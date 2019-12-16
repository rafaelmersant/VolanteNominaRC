using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VolanteNominaRC.Controllers
{
    public class PayrollController : Controller
    {
        // GET: Payroll
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendPayroll()
        {
            if (Session["role"] == null) return RedirectToAction("Login", "User");

            return View();
        }

        public ActionResult HistoryPayrollSent()
        {
            if (Session["role"] == null) return RedirectToAction("Login", "User");

            return View();
        }

        public ActionResult PayrollsByEmployee()
        {
            if (Session["role"] == null) return RedirectToAction("Login", "User");

            return View();
        }
    }
}