using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VolanteNominaRC.Models;
using static VolanteNominaRC.Controllers.AjaxController;

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

        public ActionResult ViewPayroll(string period, string paytype)
        {
            if (Session["role"] == null) return RedirectToAction("Login", "User");

            string EmployeeId = Session["employeeId"].ToString();
            DataSet payrollDetail = GetPayrollDetailForEmployee(EmployeeId, period, paytype);

            PayrollDetailHeader payroll = GetPayrollDetail(payrollDetail);
            ViewBag.PayrollDetail = payroll;

            string seenBy = User != null && User.Identity.Name.Length > 0 ? User.Identity.Name : Session["employeeId"].ToString();

            SavePayrollSeenHistory(Session["employeeId"].ToString(), payroll.ceciclopag, seenBy);
            return View();
        }

        public ActionResult PayrollsByEmployee()
        {
            if (Session["role"] == null) return RedirectToAction("Login", "User");

            try
            {
                string EmployeeId = Session["employeeId"].ToString();
                DataSet payrolls = GetPayrollsBy(EmployeeId);

                ViewBag.Payrolls = GetPayrolls(payrolls);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View();
        }

        //Map to object to get all payrolls
        private List<Payroll> GetPayrolls(DataSet data)
        {
            List<Payroll> payrolls = new List<Payroll>();

            if(data.Tables.Count > 0)
            {
                foreach(DataRow row in data.Tables[0].Rows)
                {
                    string period = row.ItemArray[0].ToString();
                    int fortnight = int.Parse(period.Substring(period.Length - 2, 2));

                    payrolls.Add(new Payroll {
                        period = period,
                        fortnight = GetPaymentNo(fortnight), //+ " " + period.Substring(period.Length - 2, 2),
                        year = period.Substring(0, 4),
                        month = GetMonthName(period.Substring(4, 2)),
                        description = row.ItemArray[1].ToString(),
                        paytype = row.ItemArray[2].ToString()
                    });
                }
            }
            return payrolls;
        }
        
        //Save when payroll is seen by someone
        private void SavePayrollSeenHistory(string employeeId, string payrollCycle, string seenBy)
        {
            try
            {
                using (var db = new VolanteNominaEntities())
                {
                    db.PayrollSeenHistories.Add(new PayrollSeenHistory
                    {
                        EmployeeId = employeeId,
                        PayrollCycle = payrollCycle,
                        SeenBy = seenBy,
                        SeenTime = DateTime.Now,
                        Machine = Environment.MachineName
                    });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private string GetMonthName(string month)
        {
            string _month = "Desconocido";

            switch(month)
            {
                case "01": _month = "Enero"; break;
                case "02": _month = "Febrero"; break;
                case "03": _month = "Marzo"; break;
                case "04": _month = "Abril"; break;
                case "05": _month = "Mayo"; break;
                case "06": _month = "Junio"; break;
                case "07": _month = "Julio"; break;
                case "08": _month = "Agosto"; break;
                case "09": _month = "Septiembre"; break;
                case "10": _month = "Octubre"; break;
                case "11": _month = "Noviembre"; break;
                case "12": _month = "Diciembre"; break;
            }

            return _month;
        }

        private string GetPaymentNo(int fortnight)
        {
            if (fortnight % 2 == 0)
                return "2da.";

            return "1ra.";
        }
    }
}