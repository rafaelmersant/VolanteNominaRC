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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            try
            {
                using(var db = new VolanteNominaEntities())
                {
                    string _pass = AjaxController.SHA256(password);
                    var _user = db.Users.FirstOrDefault(u => u.EmployeeID == username && u.PasswordHash == _pass);
                    if(_user != null)
                    {
                        Session["employeeID"] = username;
                        Session["role"] = _user.Role;

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.EmployeeId = username;
                        ViewBag.Message = "Usuario/Contraseña incorrecto.";
                    }
                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View();
        }

        public ActionResult UsersList()
        {
            if (Session["role"] == null) return RedirectToAction("Login", "User");

            using(var db = new VolanteNominaEntities())
            {
                var users = db.Users.ToList().OrderByDescending(o => o.CreatedDate);
                return View(users);
            }
        }

        public ActionResult RegisterUser()
        {
            //if (Session["role"] == null) return RedirectToAction("Login", "User");

            ViewBag.Roles = GetRoles();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(User user)
        {
            ViewBag.Result = "info";

            try
            {
                using (var db = new VolanteNominaEntities())
                {
                    if (ModelState.IsValid)
                    {
                        var users = db.Users.ToList();

                        //This EmployeeId exists ?
                        var _userEmployeeId = users.FirstOrDefault(u => u.EmployeeID == user.EmployeeID);
                        if (_userEmployeeId != null) throw new Exception("Este código de empleado ya existe en el sistema.");

                        var _userIdentification = users.FirstOrDefault(u => u.Identification == user.Identification);
                        if (_userIdentification != null) throw new Exception("Esta cédula ya esta registrada a un usuario del sistema.");

                        var newUser = new User
                        {
                            CreatedDate = DateTime.Now,
                            IdHash = Guid.NewGuid(),
                            Email = user.Email,
                            EmployeeID = user.EmployeeID,
                            Identification = user.Identification,
                            PasswordHash = AjaxController.SHA256(user.PasswordHash),
                            Role = user.Role
                        };

                        db.Users.Add(newUser);
                        db.SaveChanges();

                        return RedirectToAction("UsersList");
                    }
                }
            }
            catch(Exception ex)
            {
                ViewBag.Result = "danger";
                ViewBag.Message = ex.Message;
            }
            

            ViewBag.Roles = GetRoles();

            return View();
        }

        private IList<SelectListItem> GetRoles()
        {
            IList<SelectListItem> roles = new List<SelectListItem>
            {
                new SelectListItem() {Text="Consulta", Value="Consulta"},
                new SelectListItem() { Text="Admin", Value="Admin"}
            };
            return roles;
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