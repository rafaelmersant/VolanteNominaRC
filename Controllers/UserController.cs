using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VolanteNominaRC.Models;

namespace VolanteNominaRC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(User user)
        {
            using(var db = new VolanteNominaEntities())
            {
                if (ModelState.IsValid)
                {

                }
            }

            return View();
        }

        public ActionResult RecoverPassword()
        {
            return View();
        }

        public ActionResult ExceptionForEmployees()
        {
            return View();
        }
    }
}