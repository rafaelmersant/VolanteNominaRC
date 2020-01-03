using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Data.Odbc;
using System.Data;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using VolanteNominaRC.Models;

namespace VolanteNominaRC.Controllers
{
    public class AjaxController : Controller
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

        // GET: Ajax
        public ActionResult Index()
        {
            return View();
        }

        private string GetCycleFor(int month, int cycle)
        {
            if (cycle == 1)
                return ((month * 2) - 1).ToString().PadLeft(2, '0');

            return (month * cycle).ToString().PadLeft(2, '0');
        }

        [HttpPost]
        public string SendPayrollTemplate(string by, string entityId, string month, string year, string cycle)
        {
            bool sent = false;
            string content = string.Empty;
            string EmployeeId = Session["employeeId"].ToString();

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

                string payrollTemplate = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates/");
                payrollTemplate = System.IO.Path.Combine(payrollTemplate, "payrollDetail.html");
                string messageBody = System.IO.File.ReadAllText(payrollTemplate);

                string _cycle = year + month + GetCycleFor(int.Parse(month), int.Parse(cycle));

                DataSet employees = GetEmployeesPayroll(by, entityId, _cycle);

                foreach (DataRow row in employees.Tables[0].Rows)
                {
                    content = messageBody;

                    string employeeId_ = row.ItemArray[0].ToString();
                    string paytype_ = row.ItemArray[1].ToString();
                    string cycle_ = row.ItemArray[2].ToString();

                    DataSet payroll = GetPayrollDetailForEmployee(employeeId_, cycle_, paytype_);

                    PayrollDetailHeader payrollDetail = GetPayrollDetail(payroll);

                    content = content.Replace("##cedescpago##", payrollDetail.cedescpago);
                    content = content.Replace("##ceciclopag##", payrollDetail.ceciclopag);
                    content = content.Replace("##cenomemple##", payrollDetail.cenomemple);
                    content = content.Replace("##cecodemple##", payrollDetail.cecodemple);
                    content = content.Replace("##cenomcargo##", payrollDetail.cenomcargo);

                    string body = string.Empty;
                    string concept = string.Empty;
                    string rowColorGray = "#e0e0e0";
                    string rowColorWhite = "#ffffff";
                    string rowColor = rowColorWhite;
                    int index = 1;

                    foreach (var item in payrollDetail.detail)
                    {
                        if (index % 2 == 0)
                            rowColor = rowColorGray;
                        else
                            rowColor = rowColorWhite;
                           
                        concept = item.ceingdeduc == "I" ?
                            "<span>" + String.Format("{0:#,##0.00}", item.cevaltrans) + "</span> </td> <td style='width: 20%;'></td>" :
                            "<td style = 'width: 20%; text-align: right'>"+
                                "<span>" + String.Format("{0:#,##0.00}", item.cevaltrans) + "</span> </td>";

                        body += "<tr style='background-color: " + rowColor +"'> <td style='width: 60%'>" + item.ceingdeduc + "-" + item.cetipotrans + " " + item.cedesctran + "</td>" +
                                "<td style='width: 20%; text-align: right'>" + concept + "</tr>";

                        index += 1;
                    }

                    content = content.Replace("##payrollBody##", body);

                    content = content.Replace("##incomeTotal##", String.Format("{0:#,##0.00}", payrollDetail.incomeTotal));
                    content = content.Replace("##discountTotal##", String.Format("{0:#,##0.00}", payrollDetail.discountTotal));
                    content = content.Replace("##total##", String.Format("{0:#,##0.00}", payrollDetail.total));

                    try
                    {
                        //if(payrollDetail.cecorreoel != string.Empty)
                        //{

                        //}

                        SmtpClient smtp = new SmtpClient
                        {
                            Host = ConfigurationManager.AppSettings["smtpClient"],
                            Port = int.Parse(ConfigurationManager.AppSettings["PortMail"]),
                            UseDefaultCredentials = false,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            Credentials = new NetworkCredential(ConfigurationManager.AppSettings["usrEmail"], ConfigurationManager.AppSettings["pwdEmail"]),
                            EnableSsl = true,
                        };

                        MailMessage message = new MailMessage();
                        message.IsBodyHtml = true;
                        message.Body = content;
                        message.Subject = "VOLANTE NOMINA " + payrollDetail.cedescpago + " " + _cycle;
                        message.To.Add(new MailAddress("rafaelmersant@sagaracorp.com")); //payrollDetail.cecorreoel

                        string address = ConfigurationManager.AppSettings["EMail"];
                        string displayName = ConfigurationManager.AppSettings["EMailName"];
                        message.From = new MailAddress(address, displayName);

                        smtp.Send(message);
                        sent = true;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    SavePayrollSent(employeeId_, _cycle, EmployeeId);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            if (sent)
                return "200";
            else
                return "No fue enviado ningun volante, favor verificar que la información más arriba es correcta.";
        }

        private void SavePayrollSent(string employeeId, string cycle, string sentBy)
        {
            using (var db = new VolanteNominaEntities())
            {
                db.PayrollSentHistories.Add(new PayrollSentHistory
                {
                    EmployeeId = employeeId,
                    PayrollCycle = cycle,
                    SentBy = sentBy,
                    Sent = DateTime.Now                    
                });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public JsonResult GetPayrollsSent()
        {
            using (var db = new VolanteNominaEntities())
            {
                try
                {
                    var lastYear = DateTime.Now.AddDays(-365);

                    var payrollsSent = db.PayrollSentHistories.Where(p => p.Sent >= lastYear)
                                         .GroupBy(g => new { y = g.Sent.Year, m = g.Sent.Month, d = g.Sent.Day })
                                         .Select(s => new
                                            {
                                                Date = s.Key.d + "/" + s.Key.m + "/" + s.Key.y,
                                                Count = s.Count()
                                            }).OrderByDescending(o => o.Date).Take(12).ToList();

                    return new JsonResult { Data = payrollsSent, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                catch(Exception ex)
                {
                    return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
        }

        public string GetEmailByEmployeeId(string employeeId)
        {
            try
            {
                using(var db = new VolanteNominaEntities())
                {
                    var employee = db.Users.FirstOrDefault(e => e.EmployeeID == employeeId);
                    if(employee != null)
                    {
                        return employee.Email;
                    }
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

            return string.Empty;
        }

        public static DataSet GetPayrollsBy(string employeeId)
        {
            string sQuery = "SELECT DISTINCT CECICLOPAG, CEDESCPAGO, CETIPOPAGO from [QS36F.RCNOCE00] WHERE CECODEMPLE = " + employeeId + " ORDER BY 1 DESC";

            if (ConfigurationManager.AppSettings["EnvironmentVolante"] == "PROD")
                sQuery = sQuery.Replace("[", "").Replace("]", "");

            return ExecuteDataSetODBC(sQuery, null);
        }

        private static DataSet GetEmployeesPayroll(string by, string entityId, string cycle)
        {
            string sQuery = string.Empty;

            if (by == "all")
            {
                sQuery = "SELECT DISTINCT CECODEMPLE, CETIPOPAGO, CECICLOPAG " +
                " FROM [QS36F.RCNOCE00] WHERE CECICLOPAG = '" + cycle + "' ";
            }
            else if (by == "directorate")
            {
                sQuery = "SELECT DISTINCT CECODEMPLE, CETIPOPAGO, CECICLOPAG " +
                " FROM [QS36F.RCNOCE00] WHERE CENOMDEPTO = '" + entityId + "' " +
                " AND CECICLOPAG = '" + cycle + "'";
            }
            else
            {
                sQuery = "SELECT DISTINCT CECODEMPLE, CETIPOPAGO, CECICLOPAG " +
                " FROM [QS36F.RCNOCE00] WHERE CECODEMPLE = " + entityId +
                " AND CECICLOPAG = '" + cycle + "' GROUP BY CECODEMPLE, CETIPOPAGO, CECICLOPAG";
            }

            if (ConfigurationManager.AppSettings["EnvironmentVolante"] == "PROD")
                sQuery = sQuery.Replace("[", "").Replace("]", "");

            return ExecuteDataSetODBC(sQuery, null);
        }

        public static DataSet GetPayrollDetailForEmployee(string employeeId, string cycle, string paytype)
        {
            string sQuery = string.Empty;

            sQuery = "SELECT CECODIRE, CETIPOPAGO, CEDESCPAGO, CEINGDEDUC, CETIPTRANS, CEDESCTRAN, CEVALTRANS, CECICLOPAG," +
            " CECODEMPLE, CENOMEMPLE, CENOMDEPTO, CENOMCARGO, CECORREOEL FROM [QS36F.RCNOCE00] WHERE CECODEMPLE = " + employeeId +
            " AND CECICLOPAG = '" + cycle + "' AND CETIPOPAGO = '" + paytype + "' ORDER BY CEINGDEDUC DESC";


            if (ConfigurationManager.AppSettings["EnvironmentVolante"] == "PROD")
                sQuery = sQuery.Replace("[", "").Replace("]", "");

            return ExecuteDataSetODBC(sQuery, null);
        }

        //Map to object to get all detail for a specific payroll
        public static PayrollDetailHeader GetPayrollDetail(DataSet data)
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

        public static string SHA256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));

            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("X2"));
            }
            return hash.ToString();
        }

        public static DataSet ExecuteDataSetODBC(string query, OdbcParameter[] parameters = null)
        {
            string sConn = ConfigurationManager.AppSettings["sConnSQLODBC"];

            OdbcConnection oconn = new OdbcConnection(sConn);
            OdbcCommand ocmd = new OdbcCommand(query, oconn);
            OdbcDataAdapter adapter;
            DataSet dsData = new DataSet();

            ocmd.CommandType = CommandType.Text;

            if (parameters != null)
            {
                ocmd.Parameters.Clear();

                foreach (OdbcParameter param in parameters)
                    ocmd.Parameters.Add(param);
            }

            adapter = new OdbcDataAdapter(ocmd);
            adapter.Fill(dsData);

            return dsData;
        }

    }
}