using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace VolanteNominaRC.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        public ActionResult Index()
        {
            return View();
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
    }
}