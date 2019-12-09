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
            return View();
        }

        public ActionResult HistoryPayrollSent()
        {
            return View();
        }

        public ActionResult PayrollsByEmployee()
        {
            return View();
        }
    }
}