﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            //using (var db = new VolanteNominaEntities())
            //{
            //    var users = db.Users.ToList();

            //    foreach(var u in users)
            //    {
            //        u.PasswordHash = AjaxController.SHA256(u.PasswordHash);
            //        db.SaveChanges();
            //    }
            //}

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["employeeID"] = null;
            Session["role"] = null;
            Session["email"] = null;

            return RedirectToAction("Login", "User");
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
                        Session["email"] = _user.Email;

                        db.LoginHistories.Add(new LoginHistory
                        {
                            LastLogin = DateTime.Now,
                            UserID = _user.Id
                        });
                        db.SaveChanges();

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
            if (Session["role"] == null) return RedirectToAction("Index", "Home");
            if (Session["role"].ToString() != "Admin") return RedirectToAction("Index", "Home");

            using (var db = new VolanteNominaEntities())
            {
                var users = db.Users.ToList().OrderByDescending(o => o.CreatedDate);
                return View(users);
            }
        }

        public ActionResult RegisterUser()
        {
            if (Session["role"] == null) return RedirectToAction("Login", "User");

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

                        //This Identification exists ?
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
            catch (Exception ex)
            {
                ViewBag.Result = "danger";
                ViewBag.Message = ex.Message;
            }

            ViewBag.Roles = GetRoles();

            return View();
        }

        public ActionResult Edit(Guid? IdHash)
        {
            if (Session["role"] != null && Session["role"].ToString() != "Admin") return RedirectToAction("Index", "Home");

            ViewBag.Roles = GetRoles();

            if (IdHash == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using(var db = new VolanteNominaEntities())
            {
                User _user = db.Users.FirstOrDefault(u => u.IdHash == IdHash);
                if (_user == null)
                {
                    return HttpNotFound();
                }

                return View(_user);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User _user)
        {

            ViewBag.Roles = GetRoles();

            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new VolanteNominaEntities())
                    {
                        User user_edit = db.Users.FirstOrDefault(u => u.IdHash == _user.IdHash);

                        if (user_edit != null)
                        {
                            var users = db.Users.ToList();

                            //This EmployeeId exists ?
                            var _userEmployeeId = users.FirstOrDefault(u => u.EmployeeID == _user.EmployeeID);
                            if (_userEmployeeId != null && user_edit.EmployeeID != _user.EmployeeID) throw new Exception("Este código de empleado ya existe en el sistema.");

                            //This Identification exists ?
                            var _userIdentification = users.FirstOrDefault(u => u.Identification == _user.Identification);
                            if (_userIdentification != null && user_edit.Identification != _user.Identification) throw new Exception("Esta cédula ya esta registrada a un usuario del sistema.");

                            user_edit.Identification = _user.Identification;
                            user_edit.Role = _user.Role;
                            user_edit.Email = _user.Email;
                            user_edit.EmployeeID = _user.EmployeeID;
                            //user_edit.PasswordHash = _pass;

                            db.SaveChanges();
                            return RedirectToAction("UsersList", "User");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Result = "danger";
                    ViewBag.Message = ex.Message;
                }

                return View(_user);
            }

            return View(_user);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                using (var db = new VolanteNominaEntities())
                {
                    User _user = db.Users.FirstOrDefault(u => u.Id == id);
                    if (_user != null)
                    {
                        db.Users.Remove(_user);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = "500", message = ex.Message });
            }

            return Json(new { result = "200", message = "Success" });
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

        [HttpPost]
        public JsonResult RecoverPassword(string employeeId, string email, string identification)
        {
            try
            {
                using (var db = new VolanteNominaEntities())
                {
                    //This EmployeeId exists ?
                    var _userEmployeeId = db.Users.FirstOrDefault(u => u.EmployeeID == employeeId);
                    if (_userEmployeeId == null) throw new Exception("Este código de empleado no fue encontrado.");

                    //This Identification exists ?
                    var _userIdentification = db.Users.FirstOrDefault(u => u.Identification == identification);
                    if (_userIdentification == null) throw new Exception("Esta cédula no fue encontrada.");

                    var _userRelationship = db.Users.FirstOrDefault(u => u.Identification == identification && u.EmployeeID == employeeId);
                    if (_userRelationship == null) throw new Exception("Esta cédula no esta relacionada a este empleado.");

                    var user_edit = db.Users.FirstOrDefault(u => u.EmployeeID == employeeId);
                    string newPassword = Environment.TickCount.ToString().Substring(0, 4);
                    string newPasswordHash = AjaxController.SHA256(newPassword);

                    user_edit.PasswordHash = newPasswordHash;
                    db.SaveChanges();

                    AjaxController.SendRecoverPasswordEmail(newPassword, email);

                    return new JsonResult { Data = "200", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            catch (Exception ex)
            {
                try
                {
                    AjaxController.SendRawEmail("rafaelmersant@sagaracorp.com", "Exception in RecoverPassword", ex.ToString());
                }
                catch (Exception exq)
                {
                    Console.WriteLine(exq.ToString());
                }
                
                return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public ActionResult ChangePassword()
        {
            if (Session["role"] == null) return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public JsonResult ChangePassword(string currentPassword, string newPassword)
        {
            try
            {
                using (var db = new VolanteNominaEntities())
                {
                    string employeeId = Session["employeeID"].ToString();
                    string password = AjaxController.SHA256(currentPassword);

                    var currentUser = db.Users.FirstOrDefault(u => u.EmployeeID == employeeId && u.PasswordHash == password);

                    if(currentUser != null)
                    {
                        currentUser.PasswordHash = AjaxController.SHA256(newPassword);
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("La contraseña actual es incorrecta, favor verificar.");
                    }

                    return new JsonResult { Data = "200", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            catch (Exception ex)
            {
                try
                {
                    AjaxController.SendRawEmail("rafaelmersant@sagaracorp.com", "Exception in ChangePassword", ex.ToString());
                }
                catch (Exception exq)
                {
                    Console.WriteLine(exq.ToString());
                }

                return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public ActionResult ExceptionForEmployees()
        {
            if (Session["role"] == null) return RedirectToAction("Index", "Home");
            if (Session["role"].ToString() != "Admin") return RedirectToAction("Index", "Home");

            using (var db = new VolanteNominaEntities())
            {
                try
                {
                    ViewBag.exceptions = db.ExceptionsEmployees.ToList();

                }
                catch(Exception ex)
                {
                    try
                    {
                        AjaxController.SendRawEmail("rafaelmersant@sagaracorp.com", "Exception in ExceptionForEmployees", ex.ToString());
                    }
                    catch (Exception exq)
                    {
                        Console.WriteLine(exq.ToString());
                    }

                    ViewBag.message = ex.Message;
                }
            }
            
            return View();
        }

        [HttpPost]
        public JsonResult AddExceptionForEmployee(string employeeId)
        {
            try
            {
                using (var db = new VolanteNominaEntities())
                {
                    var exceptions = db.ExceptionsEmployees.FirstOrDefault(e => e.EmployeeId == employeeId);

                    if (exceptions != null)
                    {
                        throw new Exception("Este empleado ya fue agregado en el listado de excepciones.");
                    }
                    else
                    {
                        db.ExceptionsEmployees.Add(new ExceptionsEmployee
                        {
                            EmployeeId = employeeId,
                            DateAdded = DateTime.Now
                        });

                        db.SaveChanges();
                    }

                    return new JsonResult { Data = "200", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            catch (Exception ex)
            {
                try
                {
                    AjaxController.SendRawEmail("rafaelmersant@sagaracorp.com", "Exception in AddExceptionForEmployee", ex.ToString());
                }
                catch (Exception exq)
                {
                    Console.WriteLine(exq.ToString());
                }

                return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult RemoveFromException(string employeeId)
        {
            try
            {
                using (var db = new VolanteNominaEntities())
                {
                    var _employee = db.ExceptionsEmployees.FirstOrDefault(e => e.EmployeeId == employeeId);
                    if (_employee != null)
                    {
                        db.ExceptionsEmployees.Remove(_employee);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = "500", message = ex.Message });
            }

            return Json(new { result = "200", message = "Success" });
        }

        public ActionResult TestEmail()
        {
            try
            {
                AjaxController.SendRawEmail("rafaelmersant@sagaracorp.com", "Testing RC...", "This is a test email");

                return Content("Email Sent");
            }
            catch(Exception ex)
            {
                return Content(ex.ToString());
            }
            
        }
    }
}