using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VolanteNominaRC.Controllers
{
    public class PayrollController : Controller
    {

        public struct Payroll
        {
            public string period;
            public string year;
            public string month;
            public string fortnight;
            public string description;
            public string paytype;
        }

        public struct PayrollDetailRows
        {
            public string ceingdeduc;
            public string cetipotrans;
            public string cedesctran;
            public decimal cevaltrans;
        }

        public struct PayrollDetailHeader
        {
            public string cecodire;
            public string cetipopago;
            public string cedescpago;
            public string ceciclopag;
            public string cecodemple;
            public string cenomemple;
            public string cenomdepto;
            public string cenomcargo;
            public string cecorreoel;
            public decimal incomeTotal;
            public decimal discountTotal;
            public decimal total;
            public List<PayrollDetailRows> detail;
        }

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
            string sQuery = "SELECT CECODIRE, CETIPOPAGO, CEDESCPAGO, CEINGDEDUC, CETIPTRANS, CEDESCTRAN, CEVALTRANS, CECICLOPAG," +
                " CECODEMPLE, CENOMEMPLE, CENOMDEPTO, CENOMCARGO, CECORREOEL FROM [QS36F.RCNOCE00] WHERE CECODEMPLE = " + EmployeeId + 
                " AND CECICLOPAG = '" + period + "' AND CETIPOPAGO = '" + paytype + "' ORDER BY CEINGDEDUC DESC";

            DataSet payrollDetail = AjaxController.ExecuteDataSetODBC(sQuery, null);

            ViewBag.PayrollDetail = GetPayrollDetail(payrollDetail);
            return View();
        }

        public ActionResult PayrollsByEmployee()
        {
            if (Session["role"] == null) return RedirectToAction("Login", "User");

            string EmployeeId = Session["employeeId"].ToString();
            string sQuery = "SELECT DISTINCT CECICLOPAG, CEDESCPAGO, CETIPOPAGO from [QS36F.RCNOCE00] WHERE CECODEMPLE = " + EmployeeId + " ORDER BY 1 DESC";

            DataSet payrolls = AjaxController.ExecuteDataSetODBC(sQuery, null);

            ViewBag.Payrolls = GetPayrolls(payrolls);

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

                    payrolls.Add(new Payroll {
                        period = period,
                        fortnight = period.Substring(period.Length - 2, 2),
                        year = period.Substring(0, 4),
                        month = GetMonthName(period.Substring(4, 2)),
                        description = row.ItemArray[1].ToString(),
                        paytype = row.ItemArray[2].ToString()
                    });
                }
            }
            return payrolls;
        }

        //Map to object to get all detail for a specific payroll
        private PayrollDetailHeader GetPayrollDetail(DataSet data)
        {
            PayrollDetailHeader payrollDetail = new PayrollDetailHeader();

            if (data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
            {
                decimal amount = 0;

                payrollDetail.discountTotal = 0;
                payrollDetail.incomeTotal = 0;
                payrollDetail.total = 0;

                payrollDetail.cecodire = data.Tables[0].Rows[0].ItemArray[0].ToString();
                payrollDetail.cetipopago = data.Tables[0].Rows[0].ItemArray[1].ToString();
                payrollDetail.cedescpago = data.Tables[0].Rows[0].ItemArray[2].ToString();
                payrollDetail.ceciclopag = data.Tables[0].Rows[0].ItemArray[7].ToString();
                payrollDetail.cecodemple = data.Tables[0].Rows[0].ItemArray[8].ToString();
                payrollDetail.cenomemple = data.Tables[0].Rows[0].ItemArray[9].ToString();
                payrollDetail.cenomdepto = data.Tables[0].Rows[0].ItemArray[10].ToString();
                payrollDetail.cenomcargo = data.Tables[0].Rows[0].ItemArray[11].ToString();
                payrollDetail.cecorreoel = data.Tables[0].Rows[0].ItemArray[12].ToString();
                
                payrollDetail.detail = new List<PayrollDetailRows>();

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    amount = decimal.Parse(row.ItemArray[6].ToString());

                    if (row.ItemArray[3].ToString() == "I") payrollDetail.incomeTotal += amount;
                    if (row.ItemArray[3].ToString() != "I") payrollDetail.discountTotal += amount;
                    
                    payrollDetail.detail.Add(new PayrollDetailRows
                    {
                        ceingdeduc = row.ItemArray[3].ToString(),
                        cetipotrans = row.ItemArray[4].ToString(),
                        cedesctran = row.ItemArray[5].ToString(),
                        cevaltrans = amount
                    });
                }

                payrollDetail.total = payrollDetail.incomeTotal - payrollDetail.discountTotal;
            }
            return payrollDetail;
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
    }
}