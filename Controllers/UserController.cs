using System;
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

        public ActionResult CreateMissingUsers()
        {
            User user1002 = new User() { Email = "N/A", EmployeeID = "1002", Identification = "00107911281", Role = "Consulta", PasswordHash = "1281" }; RegisterUser(user1002);
            User user1364 = new User() { Email = "Radiocentro@claro.net.do", EmployeeID = "1364", Identification = "00109211870", Role = "Consulta", PasswordHash = "1870" }; RegisterUser(user1364);
            User user1673 = new User() { Email = "N/A", EmployeeID = "1673", Identification = "00104125117", Role = "Consulta", PasswordHash = "5117" }; RegisterUser(user1673);
            User user1841 = new User() { Email = "N/A", EmployeeID = "1841", Identification = "00100679836", Role = "Consulta", PasswordHash = "9836" }; RegisterUser(user1841);
            User user9432 = new User() { Email = "N/A", EmployeeID = "9432", Identification = "00108194283", Role = "Consulta", PasswordHash = "4283" }; RegisterUser(user9432);
            User user3463 = new User() { Email = "lguerra@radiocentro.com.do", EmployeeID = "3463", Identification = "40223264439", Role = "Consulta", PasswordHash = "4439" }; RegisterUser(user3463);
            User user2666 = new User() { Email = "miryamdr@radiocentro.com.do", EmployeeID = "2666", Identification = "00101221745", Role = "Consulta", PasswordHash = "1745" }; RegisterUser(user2666);
            User user3383 = new User() { Email = "maguzman@radiocentro.com.do", EmployeeID = "3383", Identification = "22300116377", Role = "Consulta", PasswordHash = "6377" }; RegisterUser(user3383);
            User user3385 = new User() { Email = "ltroncoso@radiocentro.com.do", EmployeeID = "3385", Identification = "40226059026", Role = "Consulta", PasswordHash = "9026" }; RegisterUser(user3385);
            User user3584 = new User() { Email = "adelarosa@radiocentro.com.do", EmployeeID = "3584", Identification = "01600170367", Role = "Consulta", PasswordHash = "0367" }; RegisterUser(user3584);
            User user4763 = new User() { Email = "jbrito@radiocentro.com.do", EmployeeID = "4763", Identification = "04900608490", Role = "Consulta", PasswordHash = "8490" }; RegisterUser(user4763);
            User user4846 = new User() { Email = "staveras@radiocentro.com.do", EmployeeID = "4846", Identification = "00116160490", Role = "Consulta", PasswordHash = "0490" }; RegisterUser(user4846);
            User user3631 = new User() { Email = "rbaez@radiocentro.com.do", EmployeeID = "3631", Identification = "00116209958", Role = "Consulta", PasswordHash = "9958" }; RegisterUser(user3631);
            User user525 = new User() { Email = "svicente@radiocentro.com.do", EmployeeID = "525", Identification = "40224290334", Role = "Consulta", PasswordHash = "0334" }; RegisterUser(user525);
            User user601 = new User() { Email = "cpinales@radiocentro.com.do", EmployeeID = "601", Identification = "01300368220", Role = "Consulta", PasswordHash = "8220" }; RegisterUser(user601);
            User user781 = new User() { Email = "dlacruz@radiocentro.com.do", EmployeeID = "781", Identification = "00101022671", Role = "Consulta", PasswordHash = "2671" }; RegisterUser(user781);
            User user1350 = new User() { Email = "ocruz@radiocentro.com.do", EmployeeID = "1350", Identification = "00101338598", Role = "Consulta", PasswordHash = "8598" }; RegisterUser(user1350);
            User user1383 = new User() { Email = "afernandez@radiocentro.com.do", EmployeeID = "1383", Identification = "00107591836", Role = "Consulta", PasswordHash = "1836" }; RegisterUser(user1383);
            User user1567 = new User() { Email = "fgarcia@radiocentro.com.do", EmployeeID = "1567", Identification = "00109643478", Role = "Consulta", PasswordHash = "3478" }; RegisterUser(user1567);
            User user2769 = new User() { Email = "gguerrero@radiocentro.com.do", EmployeeID = "2769", Identification = "00300386281", Role = "Consulta", PasswordHash = "6281" }; RegisterUser(user2769);
            User user3281 = new User() { Email = "vacosta@radiocentro.com.do", EmployeeID = "3281", Identification = "00115135089", Role = "Consulta", PasswordHash = "5089" }; RegisterUser(user3281);
            User user3282 = new User() { Email = "mamparo@radiocentro.com.do", EmployeeID = "3282", Identification = "00117080630", Role = "Consulta", PasswordHash = "0630" }; RegisterUser(user3282);
            User user3417 = new User() { Email = "mvidal@radiocentro.com.do", EmployeeID = "3417", Identification = "40227228877", Role = "Consulta", PasswordHash = "8877" }; RegisterUser(user3417);
            User user3522 = new User() { Email = "mcamejo@radiocentro.com.do", EmployeeID = "3522", Identification = "00101551711", Role = "Consulta", PasswordHash = "1711" }; RegisterUser(user3522);
            User user3585 = new User() { Email = "movalles@radiocentro.com.do", EmployeeID = "3585", Identification = "40236771651", Role = "Consulta", PasswordHash = "1651" }; RegisterUser(user3585);
            User user5250 = new User() { Email = "ksantana@radiocentro.com.do", EmployeeID = "5250", Identification = "22400201665", Role = "Consulta", PasswordHash = "1665" }; RegisterUser(user5250);
            User user5280 = new User() { Email = "fabreu@radiocentro.com.do", EmployeeID = "5280", Identification = "22400473959", Role = "Consulta", PasswordHash = "3959" }; RegisterUser(user5280);
            User user5529 = new User() { Email = "amedina@radiocentro.com.do", EmployeeID = "5529", Identification = "00117477943", Role = "Consulta", PasswordHash = "7943" }; RegisterUser(user5529);
            User user8622 = new User() { Email = "lrodriguez@radiocentro.com.do", EmployeeID = "8622", Identification = "02400246860", Role = "Consulta", PasswordHash = "6860" }; RegisterUser(user8622);
            User user9138 = new User() { Email = "egalvez@radiocentro.com.do", EmployeeID = "9138", Identification = "04900637937", Role = "Consulta", PasswordHash = "7937" }; RegisterUser(user9138);
            User user9343 = new User() { Email = "emercedes@radiocentro.com.do", EmployeeID = "9343", Identification = "40220351122", Role = "Consulta", PasswordHash = "1122" }; RegisterUser(user9343);
            User user9982 = new User() { Email = "ctoribio@radiocentro.com.do", EmployeeID = "9982", Identification = "04600353868", Role = "Consulta", PasswordHash = "3868" }; RegisterUser(user9982);
            User user161 = new User() { Email = "egonzalez@radiocentro.com.do", EmployeeID = "161", Identification = "13500007342", Role = "Consulta", PasswordHash = "7342" }; RegisterUser(user161);
            User user240 = new User() { Email = "gpuente@radiocentro.com.do", EmployeeID = "240", Identification = "22300209099", Role = "Consulta", PasswordHash = "9099" }; RegisterUser(user240);
            User user241 = new User() { Email = "fespinal@radiocentro.com.do", EmployeeID = "241", Identification = "00118093152", Role = "Consulta", PasswordHash = "3152" }; RegisterUser(user241);
            User user466 = new User() { Email = "lfulcal@radiocentro.com.do", EmployeeID = "466", Identification = "22500477660", Role = "Consulta", PasswordHash = "7660" }; RegisterUser(user466);
            User user590 = new User() { Email = "yreyes@radiocentro.com.do", EmployeeID = "590", Identification = "05401285761", Role = "Consulta", PasswordHash = "5761" }; RegisterUser(user590);
            User user618 = new User() { Email = "dmendoza@radiocentro.com.do", EmployeeID = "618", Identification = "00112665666", Role = "Consulta", PasswordHash = "5666" }; RegisterUser(user618);
            User user621 = new User() { Email = "kcabreja@radiocentro.com.do", EmployeeID = "621", Identification = "22301016295", Role = "Consulta", PasswordHash = "6295" }; RegisterUser(user621);
            User user3181 = new User() { Email = "hogando@radiocentro.com.do", EmployeeID = "3181", Identification = "22500025907", Role = "Consulta", PasswordHash = "5907" }; RegisterUser(user3181);
            User user3256 = new User() { Email = "jfeliz@radiocentro.com.do", EmployeeID = "3256", Identification = "00114835978", Role = "Consulta", PasswordHash = "5978" }; RegisterUser(user3256);
            User user3268 = new User() { Email = "vmendez@radiocentro.com.do", EmployeeID = "3268", Identification = "22400413591", Role = "Consulta", PasswordHash = "3591" }; RegisterUser(user3268);
            User user3269 = new User() { Email = "wacosta@radiocentro.com.do", EmployeeID = "3269", Identification = "00117696336", Role = "Consulta", PasswordHash = "6336" }; RegisterUser(user3269);
            User user3364 = new User() { Email = "cgro@radiocentro.com.do", EmployeeID = "3364", Identification = "40224294815", Role = "Consulta", PasswordHash = "4815" }; RegisterUser(user3364);
            User user3620 = new User() { Email = "apolanco@radiocentro.com.do", EmployeeID = "3620", Identification = "22800032561", Role = "Consulta", PasswordHash = "2561" }; RegisterUser(user3620);
            User user3655 = new User() { Email = "N/A", EmployeeID = "3655", Identification = "00119475598", Role = "Consulta", PasswordHash = "5598" }; RegisterUser(user3655);
            User user3656 = new User() { Email = "N/A", EmployeeID = "3656", Identification = "40223602927", Role = "Consulta", PasswordHash = "2927" }; RegisterUser(user3656);
            User user4913 = new User() { Email = "lfernandez@radiocentro.com.do", EmployeeID = "4913", Identification = "00115566986", Role = "Consulta", PasswordHash = "6986" }; RegisterUser(user4913);
            User user9800 = new User() { Email = "rramirez@radiocentro.com.do", EmployeeID = "9800", Identification = "22300344987", Role = "Consulta", PasswordHash = "4987" }; RegisterUser(user9800);
            User user1804 = new User() { Email = "alalo@radiocentro.com.do", EmployeeID = "1804", Identification = "00119251064", Role = "Consulta", PasswordHash = "1064" }; RegisterUser(user1804);
            User user3473 = new User() { Email = "ethevenin@radiocentro.com.do", EmployeeID = "3473", Identification = "00117819144", Role = "Consulta", PasswordHash = "9144" }; RegisterUser(user3473);
            User user3588 = new User() { Email = "wsanchez@radiocentro.com.do", EmployeeID = "3588", Identification = "22300432642", Role = "Consulta", PasswordHash = "2642" }; RegisterUser(user3588);
            User user8659 = new User() { Email = "franlora@radiocentro.com.do", EmployeeID = "8659", Identification = "00115504649", Role = "Consulta", PasswordHash = "4649" }; RegisterUser(user8659);
            User user1307 = new User() { Email = "amartinez@radiocentro.com.do", EmployeeID = "1307", Identification = "00100753144", Role = "Consulta", PasswordHash = "3144" }; RegisterUser(user1307);
            User user3519 = new User() { Email = "adejesus@radiocentro.com.do", EmployeeID = "3519", Identification = "00119514602", Role = "Consulta", PasswordHash = "4602" }; RegisterUser(user3519);
            User user1744 = new User() { Email = "nadames@radiocentro.com.do", EmployeeID = "1744", Identification = "09300240018", Role = "Consulta", PasswordHash = "0018" }; RegisterUser(user1744);
            User user2168 = new User() { Email = "cmendez@radiocentro.com.do", EmployeeID = "2168", Identification = "00112315874", Role = "Consulta", PasswordHash = "5874" }; RegisterUser(user2168);
            User user2821 = new User() { Email = "cmateo@radiocentro.com.do", EmployeeID = "2821", Identification = "00112797899", Role = "Consulta", PasswordHash = "7899" }; RegisterUser(user2821);
            User user3611 = new User() { Email = "sbryan@radiocentro.com.do", EmployeeID = "3611", Identification = "00111291423", Role = "Consulta", PasswordHash = "1423" }; RegisterUser(user3611);
            User user1017 = new User() { Email = "lmarte@radiocentro.com.do", EmployeeID = "1017", Identification = "00101445724", Role = "Consulta", PasswordHash = "5724" }; RegisterUser(user1017);
            User user1146 = new User() { Email = "epimentel@radiocentro.com.do", EmployeeID = "1146", Identification = "00101250967", Role = "Consulta", PasswordHash = "0967" }; RegisterUser(user1146);
            User user1548 = new User() { Email = "gventura@radiocentro.com.do", EmployeeID = "1548", Identification = "00100166685", Role = "Consulta", PasswordHash = "6685" }; RegisterUser(user1548);
            User user1663 = new User() { Email = "aguerrero@radiocentro.com.do", EmployeeID = "1663", Identification = "02600566521", Role = "Consulta", PasswordHash = "6521" }; RegisterUser(user1663);
            User user2871 = new User() { Email = "dbatista@radiocentro.com.do", EmployeeID = "2871", Identification = "00112312145", Role = "Consulta", PasswordHash = "2145" }; RegisterUser(user2871);
            User user3224 = new User() { Email = "rgomez@radiocentro.com.do", EmployeeID = "3224", Identification = "01000900413", Role = "Consulta", PasswordHash = "0413" }; RegisterUser(user3224);
            User user3318 = new User() { Email = "mgarcia@radiocentro.com.do", EmployeeID = "3318", Identification = "40223849825", Role = "Consulta", PasswordHash = "9825" }; RegisterUser(user3318);
            User user3453 = new User() { Email = "N/A", EmployeeID = "3453", Identification = "40222805141", Role = "Consulta", PasswordHash = "5141" }; RegisterUser(user3453);
            User user3609 = new User() { Email = "Srosario@radiocentro.com.do", EmployeeID = "3609", Identification = "22500710698", Role = "Consulta", PasswordHash = "0698" }; RegisterUser(user3609);
            User user8681 = new User() { Email = "sfigueroa@radiocentro.com.do", EmployeeID = "8681", Identification = "00115563181", Role = "Consulta", PasswordHash = "3181" }; RegisterUser(user8681);
            User user8715 = new User() { Email = "yperez@radiocentro.com.do", EmployeeID = "8715", Identification = "00114123482", Role = "Consulta", PasswordHash = "3482" }; RegisterUser(user8715);
            User user8913 = new User() { Email = "mguzman@radiocentro.com.do", EmployeeID = "8913", Identification = "09300536928", Role = "Consulta", PasswordHash = "6928" }; RegisterUser(user8913);
            User user9670 = new User() { Email = "rrosario@radiocentro.com.do", EmployeeID = "9670", Identification = "04701765341", Role = "Consulta", PasswordHash = "5341" }; RegisterUser(user9670);
            User user778 = new User() { Email = "jabad@radiocentro.com.do", EmployeeID = "778", Identification = "40235339468", Role = "Consulta", PasswordHash = "9468" }; RegisterUser(user778);
            User user1522 = new User() { Email = "msantana@radiocentro.com.do", EmployeeID = "1522", Identification = "00102446523", Role = "Consulta", PasswordHash = "6523" }; RegisterUser(user1522);
            User user3104 = new User() { Email = "kvaldez@radiocentro.com.do", EmployeeID = "3104", Identification = "22800018552", Role = "Consulta", PasswordHash = "8552" }; RegisterUser(user3104);
            User user3624 = new User() { Email = "scruz@radiocentro.com.do", EmployeeID = "3624", Identification = "40224744868", Role = "Consulta", PasswordHash = "4868" }; RegisterUser(user3624);
            User user5199 = new User() { Email = "emorales@radiocentro.com.do", EmployeeID = "5199", Identification = "00116065848", Role = "Consulta", PasswordHash = "5848" }; RegisterUser(user5199);
            User user9420 = new User() { Email = "mmendez@radiocentro.com.do", EmployeeID = "9420", Identification = "00100651199", Role = "Consulta", PasswordHash = "1199" }; RegisterUser(user9420);
            User user2315 = new User() { Email = "N/A", EmployeeID = "2315", Identification = "00107592040", Role = "Consulta", PasswordHash = "2040" }; RegisterUser(user2315);
            User user4263 = new User() { Email = "bcruz@radiocentro.com.do", EmployeeID = "4263", Identification = "00101262673", Role = "Consulta", PasswordHash = "2673" }; RegisterUser(user4263);
            User user67 = new User() { Email = "ahernandez@radiocentro.com.do", EmployeeID = "67", Identification = "00116503822", Role = "Consulta", PasswordHash = "3822" }; RegisterUser(user67);
            User user1211 = new User() { Email = "gportes@radiocentro.com.do", EmployeeID = "1211", Identification = "00200109239", Role = "Consulta", PasswordHash = "9239" }; RegisterUser(user1211);
            User user4927 = new User() { Email = "mabreu@radiocentro.com.do", EmployeeID = "4927", Identification = "22400211797", Role = "Consulta", PasswordHash = "1797" }; RegisterUser(user4927);
            User user5497 = new User() { Email = "mguerrero@radiocentro.com.do", EmployeeID = "5497", Identification = "00103395364", Role = "Consulta", PasswordHash = "5364" }; RegisterUser(user5497);
            User user8311 = new User() { Email = "scaceres@radiocentro.com.do", EmployeeID = "8311", Identification = "04700152582", Role = "Consulta", PasswordHash = "2582" }; RegisterUser(user8311);
            User user9164 = new User() { Email = "N/A", EmployeeID = "9164", Identification = "01000697134", Role = "Consulta", PasswordHash = "7134" }; RegisterUser(user9164);
            User user9818 = new User() { Email = "N/A", EmployeeID = "9818", Identification = "00111839601", Role = "Consulta", PasswordHash = "9601" }; RegisterUser(user9818);
            User user9954 = new User() { Email = "mpaulino@radiocentro.com.do", EmployeeID = "9954", Identification = "00114241508", Role = "Consulta", PasswordHash = "1508" }; RegisterUser(user9954);
            User user153 = new User() { Email = "malmanzar@radiocentro.com.do", EmployeeID = "153", Identification = "00117657619", Role = "Consulta", PasswordHash = "7619" }; RegisterUser(user153);
            User user745 = new User() { Email = "rastacio@radiocentro.com.do", EmployeeID = "745", Identification = "00101529683", Role = "Consulta", PasswordHash = "9683" }; RegisterUser(user745);
            User user3454 = new User() { Email = "pgarrido@radiocentro.com.do", EmployeeID = "3454", Identification = "40225147400", Role = "Consulta", PasswordHash = "7400" }; RegisterUser(user3454);
            User user1217 = new User() { Email = "rramos@radiocentro.com.do", EmployeeID = "1217", Identification = "00106336720", Role = "Consulta", PasswordHash = "6720" }; RegisterUser(user1217);
            User user3105 = new User() { Email = "jlopez@radiocentro.com.do", EmployeeID = "3105", Identification = "40215322955", Role = "Consulta", PasswordHash = "2955" }; RegisterUser(user3105);
            User user3389 = new User() { Email = "jdominguez@radiocentro.com.do", EmployeeID = "3389", Identification = "00109230565", Role = "Consulta", PasswordHash = "0565" }; RegisterUser(user3389);
            User user9060 = new User() { Email = "mmedina@radiocentro.com.do", EmployeeID = "9060", Identification = "07600192475", Role = "Consulta", PasswordHash = "2475" }; RegisterUser(user9060);
            User user3393 = new User() { Email = "jvasquez@radiocentro.com.do", EmployeeID = "3393", Identification = "00116780313", Role = "Consulta", PasswordHash = "0313" }; RegisterUser(user3393);
            User user405 = new User() { Email = "N/A", EmployeeID = "405", Identification = "00111630240", Role = "Consulta", PasswordHash = "0240" }; RegisterUser(user405);
            User user620 = new User() { Email = "N/A", EmployeeID = "620", Identification = "22500316959", Role = "Consulta", PasswordHash = "6959" }; RegisterUser(user620);
            User user633 = new User() { Email = "N/A", EmployeeID = "633", Identification = "04701286116", Role = "Consulta", PasswordHash = "6116" }; RegisterUser(user633);
            User user980 = new User() { Email = "N/A", EmployeeID = "980", Identification = "05600457153", Role = "Consulta", PasswordHash = "7153" }; RegisterUser(user980);
            User user998 = new User() { Email = "N/A", EmployeeID = "998", Identification = "01000678118", Role = "Consulta", PasswordHash = "8118" }; RegisterUser(user998);
            User user1005 = new User() { Email = "N/A", EmployeeID = "1005", Identification = "01800499756", Role = "Consulta", PasswordHash = "9756" }; RegisterUser(user1005);
            User user1011 = new User() { Email = "N/A", EmployeeID = "1011", Identification = "00116407644", Role = "Consulta", PasswordHash = "7644" }; RegisterUser(user1011);
            User user1859 = new User() { Email = "N/A", EmployeeID = "1859", Identification = "00103362745", Role = "Consulta", PasswordHash = "2745" }; RegisterUser(user1859);
            User user2292 = new User() { Email = "N/A", EmployeeID = "2292", Identification = "05900146753", Role = "Consulta", PasswordHash = "6753" }; RegisterUser(user2292);
            User user3010 = new User() { Email = "N/A", EmployeeID = "3010", Identification = "40242650584", Role = "Consulta", PasswordHash = "0584" }; RegisterUser(user3010);
            User user3102 = new User() { Email = "Rrdguez@radiocentro.com.do", EmployeeID = "3102", Identification = "00112971403", Role = "Consulta", PasswordHash = "1403" }; RegisterUser(user3102);
            User user3226 = new User() { Email = "jlora@radiocentro.com.do", EmployeeID = "3226", Identification = "40223530698", Role = "Consulta", PasswordHash = "0698" }; RegisterUser(user3226);
            User user3251 = new User() { Email = "N/A", EmployeeID = "3251", Identification = "22301301713", Role = "Consulta", PasswordHash = "1713" }; RegisterUser(user3251);
            User user3291 = new User() { Email = "lreyes@radiocentro.com.do", EmployeeID = "3291", Identification = "22301740720", Role = "Consulta", PasswordHash = "0720" }; RegisterUser(user3291);
            User user3300 = new User() { Email = "N/A", EmployeeID = "3300", Identification = "00108104340", Role = "Consulta", PasswordHash = "4340" }; RegisterUser(user3300);
            User user3336 = new User() { Email = "N/A", EmployeeID = "3336", Identification = "12100014328", Role = "Consulta", PasswordHash = "4328" }; RegisterUser(user3336);
            User user3338 = new User() { Email = "N/A", EmployeeID = "3338", Identification = "40227348154", Role = "Consulta", PasswordHash = "8154" }; RegisterUser(user3338);
            User user3341 = new User() { Email = "N/A", EmployeeID = "3341", Identification = "01200885695", Role = "Consulta", PasswordHash = "5695" }; RegisterUser(user3341);
            User user3350 = new User() { Email = "N/A", EmployeeID = "3350", Identification = "01400143002", Role = "Consulta", PasswordHash = "3002" }; RegisterUser(user3350);
            User user3352 = new User() { Email = "dgloss@radiocentro.com.do", EmployeeID = "3352", Identification = "00109063594", Role = "Consulta", PasswordHash = "3594" }; RegisterUser(user3352);
            User user3375 = new User() { Email = "N/A", EmployeeID = "3375", Identification = "01201190384", Role = "Consulta", PasswordHash = "0384" }; RegisterUser(user3375);
            User user3405 = new User() { Email = "N/A", EmployeeID = "3405", Identification = "40226884043", Role = "Consulta", PasswordHash = "4043" }; RegisterUser(user3405);
            User user3410 = new User() { Email = "N/A", EmployeeID = "3410", Identification = "11000056132", Role = "Consulta", PasswordHash = "6132" }; RegisterUser(user3410);
            User user3421 = new User() { Email = "N/A", EmployeeID = "3421", Identification = "40225291695", Role = "Consulta", PasswordHash = "1695" }; RegisterUser(user3421);
            User user3430 = new User() { Email = "warias@radiocentro.com.do", EmployeeID = "3430", Identification = "01300489067", Role = "Consulta", PasswordHash = "9067" }; RegisterUser(user3430);
            User user3431 = new User() { Email = "N/A", EmployeeID = "3431", Identification = "40221641158", Role = "Consulta", PasswordHash = "1158" }; RegisterUser(user3431);
            User user3459 = new User() { Email = "N/A", EmployeeID = "3459", Identification = "40214813749", Role = "Consulta", PasswordHash = "3749" }; RegisterUser(user3459);
            User user3487 = new User() { Email = "N/A", EmployeeID = "3487", Identification = "00117445007", Role = "Consulta", PasswordHash = "5007" }; RegisterUser(user3487);
            User user3498 = new User() { Email = "N/A", EmployeeID = "3498", Identification = "40224520029", Role = "Consulta", PasswordHash = "0029" }; RegisterUser(user3498);
            User user3499 = new User() { Email = "N/A", EmployeeID = "3499", Identification = "00119376002", Role = "Consulta", PasswordHash = "6002" }; RegisterUser(user3499);
            User user3502 = new User() { Email = "N/A", EmployeeID = "3502", Identification = "40236476988", Role = "Consulta", PasswordHash = "6988" }; RegisterUser(user3502);
            User user3504 = new User() { Email = "N/A", EmployeeID = "3504", Identification = "40215308285", Role = "Consulta", PasswordHash = "8285" }; RegisterUser(user3504);
            User user3527 = new User() { Email = "N/A", EmployeeID = "3527", Identification = "04100144478", Role = "Consulta", PasswordHash = "4478" }; RegisterUser(user3527);
            User user3571 = new User() { Email = "N/A", EmployeeID = "3571", Identification = "40213973643", Role = "Consulta", PasswordHash = "3643" }; RegisterUser(user3571);
            User user3572 = new User() { Email = "N/A", EmployeeID = "3572", Identification = "40237986415", Role = "Consulta", PasswordHash = "6415" }; RegisterUser(user3572);
            User user3582 = new User() { Email = "N/A", EmployeeID = "3582", Identification = "00116427923", Role = "Consulta", PasswordHash = "7923" }; RegisterUser(user3582);
            User user3583 = new User() { Email = "N/A", EmployeeID = "3583", Identification = "40218270821", Role = "Consulta", PasswordHash = "0821" }; RegisterUser(user3583);
            User user3605 = new User() { Email = "N/A", EmployeeID = "3605", Identification = "22301257295", Role = "Consulta", PasswordHash = "7295" }; RegisterUser(user3605);
            User user3617 = new User() { Email = "N/A", EmployeeID = "3617", Identification = "40200561476", Role = "Consulta", PasswordHash = "1476" }; RegisterUser(user3617);
            User user3626 = new User() { Email = "N/A", EmployeeID = "3626", Identification = "22300603028", Role = "Consulta", PasswordHash = "3028" }; RegisterUser(user3626);
            User user3627 = new User() { Email = "N/A", EmployeeID = "3627", Identification = "22400742007", Role = "Consulta", PasswordHash = "2007" }; RegisterUser(user3627);
            User user3628 = new User() { Email = "N/A", EmployeeID = "3628", Identification = "22700010451", Role = "Consulta", PasswordHash = "0451" }; RegisterUser(user3628);
            User user3634 = new User() { Email = "N/A", EmployeeID = "3634", Identification = "40234787345", Role = "Consulta", PasswordHash = "7345" }; RegisterUser(user3634);
            User user3635 = new User() { Email = "N/A", EmployeeID = "3635", Identification = "40227963887", Role = "Consulta", PasswordHash = "3887" }; RegisterUser(user3635);
            User user3636 = new User() { Email = "N/A", EmployeeID = "3636", Identification = "40211507351", Role = "Consulta", PasswordHash = "7351" }; RegisterUser(user3636);
            User user3637 = new User() { Email = "N/A", EmployeeID = "3637", Identification = "40234568455", Role = "Consulta", PasswordHash = "8455" }; RegisterUser(user3637);
            User user3638 = new User() { Email = "N/A", EmployeeID = "3638", Identification = "00119482461", Role = "Consulta", PasswordHash = "2461" }; RegisterUser(user3638);
            User user3639 = new User() { Email = "N/A", EmployeeID = "3639", Identification = "40214475572", Role = "Consulta", PasswordHash = "5572" }; RegisterUser(user3639);
            User user3641 = new User() { Email = "N/A", EmployeeID = "3641", Identification = "01400162903", Role = "Consulta", PasswordHash = "2903" }; RegisterUser(user3641);
            User user3643 = new User() { Email = "N/A", EmployeeID = "3643", Identification = "00115631632", Role = "Consulta", PasswordHash = "1632" }; RegisterUser(user3643);
            User user3658 = new User() { Email = "N/A", EmployeeID = "3658", Identification = "40224605192", Role = "Consulta", PasswordHash = "5192" }; RegisterUser(user3658);
            User user3659 = new User() { Email = "N/A", EmployeeID = "3659", Identification = "40215641107", Role = "Consulta", PasswordHash = "1107" }; RegisterUser(user3659);
            User user4277 = new User() { Email = "N/A", EmployeeID = "4277", Identification = "00111094728", Role = "Consulta", PasswordHash = "4728" }; RegisterUser(user4277);
            User user4757 = new User() { Email = "rmendez@radiocentro.com.do", EmployeeID = "4757", Identification = "12900030805", Role = "Consulta", PasswordHash = "0805" }; RegisterUser(user4757);
            User user231 = new User() { Email = "N/A", EmployeeID = "231", Identification = "05900209874", Role = "Consulta", PasswordHash = "9874" }; RegisterUser(user231);
            User user303 = new User() { Email = "N/A", EmployeeID = "303", Identification = "11100002994", Role = "Consulta", PasswordHash = "2994" }; RegisterUser(user303);
            User user485 = new User() { Email = "N/A", EmployeeID = "485", Identification = "00500211388", Role = "Consulta", PasswordHash = "1388" }; RegisterUser(user485);
            User user625 = new User() { Email = "lcepeda@radiocentro.com.do", EmployeeID = "625", Identification = "40209688445", Role = "Consulta", PasswordHash = "8445" }; RegisterUser(user625);
            User user665 = new User() { Email = "N/A", EmployeeID = "665", Identification = "00119138162", Role = "Consulta", PasswordHash = "8162" }; RegisterUser(user665);
            User user839 = new User() { Email = "N/A", EmployeeID = "839", Identification = "40209516364", Role = "Consulta", PasswordHash = "6364" }; RegisterUser(user839);
            User user923 = new User() { Email = "N/A", EmployeeID = "923", Identification = "00109586016", Role = "Consulta", PasswordHash = "6016" }; RegisterUser(user923);
            User user994 = new User() { Email = "N/A", EmployeeID = "994", Identification = "22900030408", Role = "Consulta", PasswordHash = "0408" }; RegisterUser(user994);
            User user3020 = new User() { Email = "N/A", EmployeeID = "3020", Identification = "09000225756", Role = "Consulta", PasswordHash = "5756" }; RegisterUser(user3020);
            User user3109 = new User() { Email = "N/A", EmployeeID = "3109", Identification = "07600219427", Role = "Consulta", PasswordHash = "9427" }; RegisterUser(user3109);
            User user3184 = new User() { Email = "N/A", EmployeeID = "3184", Identification = "00111051926", Role = "Consulta", PasswordHash = "1926" }; RegisterUser(user3184);
            User user3252 = new User() { Email = "N/A", EmployeeID = "3252", Identification = "00117619130", Role = "Consulta", PasswordHash = "9130" }; RegisterUser(user3252);
            User user3311 = new User() { Email = "N/A", EmployeeID = "3311", Identification = "01000721637", Role = "Consulta", PasswordHash = "1637" }; RegisterUser(user3311);
            User user3358 = new User() { Email = "N/A", EmployeeID = "3358", Identification = "00200976306", Role = "Consulta", PasswordHash = "6306" }; RegisterUser(user3358);
            User user3367 = new User() { Email = "N/A", EmployeeID = "3367", Identification = "00115778102", Role = "Consulta", PasswordHash = "8102" }; RegisterUser(user3367);
            User user3398 = new User() { Email = "N/A", EmployeeID = "3398", Identification = "00108364803", Role = "Consulta", PasswordHash = "4803" }; RegisterUser(user3398);
            User user3399 = new User() { Email = "N/A", EmployeeID = "3399", Identification = "00114037039", Role = "Consulta", PasswordHash = "7039" }; RegisterUser(user3399);
            User user3407 = new User() { Email = "N/A", EmployeeID = "3407", Identification = "09300603546", Role = "Consulta", PasswordHash = "3546" }; RegisterUser(user3407);
            User user3412 = new User() { Email = "N/A", EmployeeID = "3412", Identification = "22500170372", Role = "Consulta", PasswordHash = "0372" }; RegisterUser(user3412);
            User user3434 = new User() { Email = "jcuevas@radiocentro.com.do", EmployeeID = "3434", Identification = "22400383455", Role = "Consulta", PasswordHash = "3455" }; RegisterUser(user3434);
            User user3441 = new User() { Email = "sguillen@radiocentro.com.do", EmployeeID = "3441", Identification = "00118775683", Role = "Consulta", PasswordHash = "5683" }; RegisterUser(user3441);
            User user3492 = new User() { Email = "N/A", EmployeeID = "3492", Identification = "08700149761", Role = "Consulta", PasswordHash = "9761" }; RegisterUser(user3492);
            User user3507 = new User() { Email = "N/A", EmployeeID = "3507", Identification = "00118677988", Role = "Consulta", PasswordHash = "7988" }; RegisterUser(user3507);
            User user3530 = new User() { Email = "N/A", EmployeeID = "3530", Identification = "00119366599", Role = "Consulta", PasswordHash = "6599" }; RegisterUser(user3530);
            User user3532 = new User() { Email = "N/A", EmployeeID = "3532", Identification = "22500054899", Role = "Consulta", PasswordHash = "4899" }; RegisterUser(user3532);
            User user3590 = new User() { Email = "N/A", EmployeeID = "3590", Identification = "02300457773", Role = "Consulta", PasswordHash = "7773" }; RegisterUser(user3590);
            User user3591 = new User() { Email = "N/A", EmployeeID = "3591", Identification = "22301290825", Role = "Consulta", PasswordHash = "0825" }; RegisterUser(user3591);
            User user3592 = new User() { Email = "N/A", EmployeeID = "3592", Identification = "00117344572", Role = "Consulta", PasswordHash = "4572" }; RegisterUser(user3592);
            User user3606 = new User() { Email = "N/A", EmployeeID = "3606", Identification = "00110933314", Role = "Consulta", PasswordHash = "3314" }; RegisterUser(user3606);
            User user3607 = new User() { Email = "N/A", EmployeeID = "3607", Identification = "00110669942", Role = "Consulta", PasswordHash = "9942" }; RegisterUser(user3607);
            User user3625 = new User() { Email = "N/A", EmployeeID = "3625", Identification = "22300725144", Role = "Consulta", PasswordHash = "5144" }; RegisterUser(user3625);
            User user3653 = new User() { Email = "N/A", EmployeeID = "3653", Identification = "40235370935", Role = "Consulta", PasswordHash = "0935" }; RegisterUser(user3653);
            User user3657 = new User() { Email = "N/A", EmployeeID = "3657", Identification = "00115820029", Role = "Consulta", PasswordHash = "0029" }; RegisterUser(user3657);
            User user3660 = new User() { Email = "N/A", EmployeeID = "3660", Identification = "02200347389", Role = "Consulta", PasswordHash = "7389" }; RegisterUser(user3660);
            User user3661 = new User() { Email = "N/A", EmployeeID = "3661", Identification = "40235514441", Role = "Consulta", PasswordHash = "4441" }; RegisterUser(user3661);
            User user479 = new User() { Email = "ovelez@radiocentro.com.do", EmployeeID = "479", Identification = "22900305487", Role = "Consulta", PasswordHash = "5487" }; RegisterUser(user479);
            User user615 = new User() { Email = "aeusebio@radiocentro.com.do", EmployeeID = "615", Identification = "40220599209", Role = "Consulta", PasswordHash = "9209" }; RegisterUser(user615);
            User user999 = new User() { Email = "cperalta@radiocentro.com.do", EmployeeID = "999", Identification = "40223650611", Role = "Consulta", PasswordHash = "0611" }; RegisterUser(user999);
            User user3290 = new User() { Email = "N/A", EmployeeID = "3290", Identification = "40212640581", Role = "Consulta", PasswordHash = "0581" }; RegisterUser(user3290);
            User user3357 = new User() { Email = "amendez@radiocentro.com.do", EmployeeID = "3357", Identification = "40234157929", Role = "Consulta", PasswordHash = "7929" }; RegisterUser(user3357);
            User user3390 = new User() { Email = "cgeronimo@radiocentro.com.do", EmployeeID = "3390", Identification = "40241998075", Role = "Consulta", PasswordHash = "8075" }; RegisterUser(user3390);
            User user3391 = new User() { Email = "mvargas@radiocentro.com.do", EmployeeID = "3391", Identification = "22300505108", Role = "Consulta", PasswordHash = "5108" }; RegisterUser(user3391);
            User user3392 = new User() { Email = "hbrito@radiocentro.com.do", EmployeeID = "3392", Identification = "22500444595", Role = "Consulta", PasswordHash = "4595" }; RegisterUser(user3392);
            User user3414 = new User() { Email = "N/A", EmployeeID = "3414", Identification = "40212374132", Role = "Consulta", PasswordHash = "4132" }; RegisterUser(user3414);
            User user3464 = new User() { Email = "agrullon@radiocentro.com.do", EmployeeID = "3464", Identification = "40215618998", Role = "Consulta", PasswordHash = "8998" }; RegisterUser(user3464);
            User user3470 = new User() { Email = "N/A", EmployeeID = "3470", Identification = "40218391155", Role = "Consulta", PasswordHash = "1155" }; RegisterUser(user3470);
            User user3558 = new User() { Email = "N/A", EmployeeID = "3558", Identification = "40212242123", Role = "Consulta", PasswordHash = "2123" }; RegisterUser(user3558);
            User user3618 = new User() { Email = "N/A", EmployeeID = "3618", Identification = "40235926520", Role = "Consulta", PasswordHash = "6520" }; RegisterUser(user3618);
            User user3642 = new User() { Email = "N/A", EmployeeID = "3642", Identification = "40234257000", Role = "Consulta", PasswordHash = "7000" }; RegisterUser(user3642);
            User user4415 = new User() { Email = "N/A", EmployeeID = "4415", Identification = "00112820295", Role = "Consulta", PasswordHash = "0295" }; RegisterUser(user4415);
            User user4858 = new User() { Email = "jbrea@radiocentro.com.do", EmployeeID = "4858", Identification = "00105040448", Role = "Consulta", PasswordHash = "0448" }; RegisterUser(user4858);
            User user9318 = new User() { Email = "llopez@radiocentro.com.do", EmployeeID = "9318", Identification = "22300875188", Role = "Consulta", PasswordHash = "5188" }; RegisterUser(user9318);
            User user9427 = new User() { Email = "N/A", EmployeeID = "9427", Identification = "00102587714", Role = "Consulta", PasswordHash = "7714" }; RegisterUser(user9427);
            User user301 = new User() { Email = "N/A", EmployeeID = "301", Identification = "40224057162", Role = "Consulta", PasswordHash = "7162" }; RegisterUser(user301);
            User user478 = new User() { Email = "N/A", EmployeeID = "478", Identification = "40225249107", Role = "Consulta", PasswordHash = "9107" }; RegisterUser(user478);
            User user1015 = new User() { Email = "N/A", EmployeeID = "1015", Identification = "00113314975", Role = "Consulta", PasswordHash = "4975" }; RegisterUser(user1015);
            User user1086 = new User() { Email = "N/A", EmployeeID = "1086", Identification = "00104007364", Role = "Consulta", PasswordHash = "7364" }; RegisterUser(user1086);
            User user1119 = new User() { Email = "N/A", EmployeeID = "1119", Identification = "40209698709", Role = "Consulta", PasswordHash = "8709" }; RegisterUser(user1119);
            User user1680 = new User() { Email = "N/A", EmployeeID = "1680", Identification = "06000117603", Role = "Consulta", PasswordHash = "7603" }; RegisterUser(user1680);
            User user1692 = new User() { Email = "N/A", EmployeeID = "1692", Identification = "00102445541", Role = "Consulta", PasswordHash = "5541" }; RegisterUser(user1692);
            User user2082 = new User() { Email = "N/A", EmployeeID = "2082", Identification = "00101988426", Role = "Consulta", PasswordHash = "8426" }; RegisterUser(user2082);
            User user2617 = new User() { Email = "gabreu@radiocentro.com.do", EmployeeID = "2617", Identification = "04100109448", Role = "Consulta", PasswordHash = "9448" }; RegisterUser(user2617);
            User user3129 = new User() { Email = "N/A", EmployeeID = "3129", Identification = "00110426103", Role = "Consulta", PasswordHash = "6103" }; RegisterUser(user3129);
            User user3183 = new User() { Email = "aamparo@radiocentro.com.do", EmployeeID = "3183", Identification = "40226457485", Role = "Consulta", PasswordHash = "7485" }; RegisterUser(user3183);
            User user3319 = new User() { Email = "N/A", EmployeeID = "3319", Identification = "01000718484", Role = "Consulta", PasswordHash = "8484" }; RegisterUser(user3319);
            User user3381 = new User() { Email = "N/A", EmployeeID = "3381", Identification = "40237088816", Role = "Consulta", PasswordHash = "8816" }; RegisterUser(user3381);
            User user3511 = new User() { Email = "N/A", EmployeeID = "3511", Identification = "00118208651", Role = "Consulta", PasswordHash = "8651" }; RegisterUser(user3511);
            User user3518 = new User() { Email = "lcalderon@radiocentro.com.do", EmployeeID = "3518", Identification = "40238348490", Role = "Consulta", PasswordHash = "8490" }; RegisterUser(user3518);
            User user3531 = new User() { Email = "N/A", EmployeeID = "3531", Identification = "40215269040", Role = "Consulta", PasswordHash = "9040" }; RegisterUser(user3531);
            User user3565 = new User() { Email = "mruiz@radiocentro.com.do", EmployeeID = "3565", Identification = "40215682564", Role = "Consulta", PasswordHash = "2564" }; RegisterUser(user3565);
            User user3569 = new User() { Email = "N/A", EmployeeID = "3569", Identification = "40220629568", Role = "Consulta", PasswordHash = "9568" }; RegisterUser(user3569);
            User user3612 = new User() { Email = "yciprian@radiocentro.com.do", EmployeeID = "3612", Identification = "22500203686", Role = "Consulta", PasswordHash = "3686" }; RegisterUser(user3612);
            User user8302 = new User() { Email = "N/A", EmployeeID = "8302", Identification = "00104403407", Role = "Consulta", PasswordHash = "3407" }; RegisterUser(user8302);
            User user9535 = new User() { Email = "rdecena@radiocentro.com.do", EmployeeID = "9535", Identification = "00115972499", Role = "Consulta", PasswordHash = "2499" }; RegisterUser(user9535);
            User user9867 = new User() { Email = "N/A", EmployeeID = "9867", Identification = "09300681856", Role = "Consulta", PasswordHash = "1856" }; RegisterUser(user9867);
            User user9960 = new User() { Email = "N/A", EmployeeID = "9960", Identification = "00201325610", Role = "Consulta", PasswordHash = "5610" }; RegisterUser(user9960);
            User user872 = new User() { Email = "mramirez@radiocentro.com.do", EmployeeID = "872", Identification = "22500797984", Role = "Consulta", PasswordHash = "7984" }; RegisterUser(user872);
            User user1215 = new User() { Email = "jogando@radiocentro.com.do", EmployeeID = "1215", Identification = "22400782235", Role = "Consulta", PasswordHash = "2235" }; RegisterUser(user1215);
            User user1569 = new User() { Email = "N/A", EmployeeID = "1569", Identification = "40220918219", Role = "Consulta", PasswordHash = "8219" }; RegisterUser(user1569);
            User user3408 = new User() { Email = "N/A", EmployeeID = "3408", Identification = "00201632536", Role = "Consulta", PasswordHash = "2536" }; RegisterUser(user3408);
            User user4424 = new User() { Email = "wdubuisson@radiocentro.com.do", EmployeeID = "4424", Identification = "22400333443", Role = "Consulta", PasswordHash = "3443" }; RegisterUser(user4424);
            User user9534 = new User() { Email = "egarcia@radiocentro.com.do", EmployeeID = "9534", Identification = "09300597649", Role = "Consulta", PasswordHash = "7649" }; RegisterUser(user9534);
            User user3286 = new User() { Email = "N/A", EmployeeID = "3286", Identification = "22900254867", Role = "Consulta", PasswordHash = "4867" }; RegisterUser(user3286);
            User user3490 = new User() { Email = "icuevas@radiocentro.com.do", EmployeeID = "3490", Identification = "00201347689", Role = "Consulta", PasswordHash = "7689" }; RegisterUser(user3490);
            User user3523 = new User() { Email = "N/A", EmployeeID = "3523", Identification = "40242813570", Role = "Consulta", PasswordHash = "3570" }; RegisterUser(user3523);
            User user3529 = new User() { Email = "N/A", EmployeeID = "3529", Identification = "14000047838", Role = "Consulta", PasswordHash = "7838" }; RegisterUser(user3529);
            User user3570 = new User() { Email = "N/A", EmployeeID = "3570", Identification = "40224056180", Role = "Consulta", PasswordHash = "6180" }; RegisterUser(user3570);
            User user3654 = new User() { Email = "ecaraballo@radiocentro.com.do", EmployeeID = "3654", Identification = "01800700716", Role = "Consulta", PasswordHash = "0716" }; RegisterUser(user3654);
            User user4118 = new User() { Email = "N/A", EmployeeID = "4118", Identification = "00116981960", Role = "Consulta", PasswordHash = "1960" }; RegisterUser(user4118);
            User user4381 = new User() { Email = "N/A", EmployeeID = "4381", Identification = "10400190384", Role = "Consulta", PasswordHash = "0384" }; RegisterUser(user4381);
            User user5632 = new User() { Email = "N/A", EmployeeID = "5632", Identification = "00107599177", Role = "Consulta", PasswordHash = "9177" }; RegisterUser(user5632);
            User user9682 = new User() { Email = "N/A", EmployeeID = "9682", Identification = "00100244979", Role = "Consulta", PasswordHash = "4979" }; RegisterUser(user9682);
            User user287 = new User() { Email = "N/A", EmployeeID = "287", Identification = "00116721937", Role = "Consulta", PasswordHash = "1937" }; RegisterUser(user287);
            User user843 = new User() { Email = "N/A", EmployeeID = "843", Identification = "00103751913", Role = "Consulta", PasswordHash = "1913" }; RegisterUser(user843);
            User user926 = new User() { Email = "N/A", EmployeeID = "926", Identification = "00108159567", Role = "Consulta", PasswordHash = "9567" }; RegisterUser(user926);
            User user1094 = new User() { Email = "N/A", EmployeeID = "1094", Identification = "02200304729", Role = "Consulta", PasswordHash = "4729" }; RegisterUser(user1094);
            User user1334 = new User() { Email = "N/A", EmployeeID = "1334", Identification = "00108853557", Role = "Consulta", PasswordHash = "3557" }; RegisterUser(user1334);
            User user1913 = new User() { Email = "N/A", EmployeeID = "1913", Identification = "00110291770", Role = "Consulta", PasswordHash = "1770" }; RegisterUser(user1913);
            User user3241 = new User() { Email = "N/A", EmployeeID = "3241", Identification = "00102851185", Role = "Consulta", PasswordHash = "1185" }; RegisterUser(user3241);
            User user3321 = new User() { Email = "N/A", EmployeeID = "3321", Identification = "05700119026", Role = "Consulta", PasswordHash = "9026" }; RegisterUser(user3321);
            User user3322 = new User() { Email = "N/A", EmployeeID = "3322", Identification = "00300725793", Role = "Consulta", PasswordHash = "5793" }; RegisterUser(user3322);
            User user3360 = new User() { Email = "N/A", EmployeeID = "3360", Identification = "00109970889", Role = "Consulta", PasswordHash = "0889" }; RegisterUser(user3360);
            User user3415 = new User() { Email = "N/A", EmployeeID = "3415", Identification = "00116353210", Role = "Consulta", PasswordHash = "3210" }; RegisterUser(user3415);
            User user3416 = new User() { Email = "N/A", EmployeeID = "3416", Identification = "22500253988", Role = "Consulta", PasswordHash = "3988" }; RegisterUser(user3416);
            User user3526 = new User() { Email = "N/A", EmployeeID = "3526", Identification = "40214521698", Role = "Consulta", PasswordHash = "1698" }; RegisterUser(user3526);
            User user3557 = new User() { Email = "N/A", EmployeeID = "3557", Identification = "40238311977", Role = "Consulta", PasswordHash = "1977" }; RegisterUser(user3557);
            User user3564 = new User() { Email = "N/A", EmployeeID = "3564", Identification = "22500734763", Role = "Consulta", PasswordHash = "4763" }; RegisterUser(user3564);
            User user3644 = new User() { Email = "N/A", EmployeeID = "3644", Identification = "00118597285", Role = "Consulta", PasswordHash = "7285" }; RegisterUser(user3644);
            User user3645 = new User() { Email = "N/A", EmployeeID = "3645", Identification = "11000049210", Role = "Consulta", PasswordHash = "9210" }; RegisterUser(user3645);
            User user5998 = new User() { Email = "wfernandez@radiocentro.com.do", EmployeeID = "5998", Identification = "04900508062", Role = "Consulta", PasswordHash = "8062" }; RegisterUser(user5998);
            User user8368 = new User() { Email = "N/A", EmployeeID = "8368", Identification = "00111667788", Role = "Consulta", PasswordHash = "7788" }; RegisterUser(user8368);
            User user8682 = new User() { Email = "N/A", EmployeeID = "8682", Identification = "01600054157", Role = "Consulta", PasswordHash = "4157" }; RegisterUser(user8682);
            User user9043 = new User() { Email = "N/A", EmployeeID = "9043", Identification = "00111671681", Role = "Consulta", PasswordHash = "1681" }; RegisterUser(user9043);
            User user9255 = new User() { Email = "N/A", EmployeeID = "9255", Identification = "00111880001", Role = "Consulta", PasswordHash = "0001" }; RegisterUser(user9255);
            User user489 = new User() { Email = "Sdoval@radiocentro.com.do", EmployeeID = "489", Identification = "01001020120", Role = "Consulta", PasswordHash = "0120" }; RegisterUser(user489);
            User user3173 = new User() { Email = "ramezquita@radiocentro.com.do", EmployeeID = "3173", Identification = "00117197731", Role = "Consulta", PasswordHash = "7731" }; RegisterUser(user3173);
            User user3205 = new User() { Email = "dabreu@radiocentro.com.do", EmployeeID = "3205", Identification = "22500234426", Role = "Consulta", PasswordHash = "4426" }; RegisterUser(user3205);
            User user3603 = new User() { Email = "N/A", EmployeeID = "3603", Identification = "40212831040", Role = "Consulta", PasswordHash = "1040" }; RegisterUser(user3603);
            User user4262 = new User() { Email = "jpaulino@radiocentro.com.do", EmployeeID = "4262", Identification = "00103166575", Role = "Consulta", PasswordHash = "6575" }; RegisterUser(user4262);
            User user213 = new User() { Email = "N/A", EmployeeID = "213", Identification = "00107431199", Role = "Consulta", PasswordHash = "1199" }; RegisterUser(user213);
            User user302 = new User() { Email = "N/A", EmployeeID = "302", Identification = "00300581121", Role = "Consulta", PasswordHash = "1121" }; RegisterUser(user302);
            User user3119 = new User() { Email = "N/A", EmployeeID = "3119", Identification = "01900169531", Role = "Consulta", PasswordHash = "9531" }; RegisterUser(user3119);
            User user9407 = new User() { Email = "N/A", EmployeeID = "9407", Identification = "00108072893", Role = "Consulta", PasswordHash = "2893" }; RegisterUser(user9407);
            User user9621 = new User() { Email = "N/A", EmployeeID = "9621", Identification = "00111385381", Role = "Consulta", PasswordHash = "5381" }; RegisterUser(user9621);
            User user9879 = new User() { Email = "N/A", EmployeeID = "9879", Identification = "00107530362", Role = "Consulta", PasswordHash = "0362" }; RegisterUser(user9879);
            User user9904 = new User() { Email = "N/A", EmployeeID = "9904", Identification = "00115108227", Role = "Consulta", PasswordHash = "8227" }; RegisterUser(user9904);
            User user39 = new User() { Email = "N/A", EmployeeID = "39", Identification = "00118532381", Role = "Consulta", PasswordHash = "2381" }; RegisterUser(user39);
            User user470 = new User() { Email = "N/A", EmployeeID = "470", Identification = "00115764623", Role = "Consulta", PasswordHash = "4623" }; RegisterUser(user470);
            User user750 = new User() { Email = "N/A", EmployeeID = "750", Identification = "40226778104", Role = "Consulta", PasswordHash = "8104" }; RegisterUser(user750);
            User user826 = new User() { Email = "N/A", EmployeeID = "826", Identification = "40226173603", Role = "Consulta", PasswordHash = "3603" }; RegisterUser(user826);
            User user3541 = new User() { Email = "N/A", EmployeeID = "3541", Identification = "00115799470", Role = "Consulta", PasswordHash = "9470" }; RegisterUser(user3541);
            User user4888 = new User() { Email = "N/A", EmployeeID = "4888", Identification = "05600727357", Role = "Consulta", PasswordHash = "7357" }; RegisterUser(user4888);
            User user3057 = new User() { Email = "N/A", EmployeeID = "3057", Identification = "00111686150", Role = "Consulta", PasswordHash = "6150" }; RegisterUser(user3057);
            User user3059 = new User() { Email = "N/A", EmployeeID = "3059", Identification = "01100308277", Role = "Consulta", PasswordHash = "8277" }; RegisterUser(user3059);
            User user3652 = new User() { Email = "N/A", EmployeeID = "3652", Identification = "01400203061", Role = "Consulta", PasswordHash = "3061" }; RegisterUser(user3652);
            User user9620 = new User() { Email = "N/A", EmployeeID = "9620", Identification = "10100078145", Role = "Consulta", PasswordHash = "8145" }; RegisterUser(user9620);
            User user3623 = new User() { Email = "abueno@radiocentro.com.do", EmployeeID = "3623", Identification = "00119122307", Role = "Consulta", PasswordHash = "2307" }; RegisterUser(user3623);
            User user3575 = new User() { Email = "N/A", EmployeeID = "3575", Identification = "07300137168", Role = "Consulta", PasswordHash = "7168" }; RegisterUser(user3575);
            User user1338 = new User() { Email = "N/A", EmployeeID = "1338", Identification = "03100180284", Role = "Consulta", PasswordHash = "0284" }; RegisterUser(user1338);
            User user3524 = new User() { Email = "mcabrera@radiocentro.com.do", EmployeeID = "3524", Identification = "03100688880", Role = "Consulta", PasswordHash = "8880" }; RegisterUser(user3524);
            User user480 = new User() { Email = "N/A", EmployeeID = "480", Identification = "03105503852", Role = "Consulta", PasswordHash = "3852" }; RegisterUser(user480);
            User user1004 = new User() { Email = "ybencosme@radiocentro.com.do", EmployeeID = "1004", Identification = "03104804244", Role = "Consulta", PasswordHash = "4244" }; RegisterUser(user1004);
            User user1235 = new User() { Email = "N/A", EmployeeID = "1235", Identification = "03105381895", Role = "Consulta", PasswordHash = "1895" }; RegisterUser(user1235);
            User user3325 = new User() { Email = "N/A", EmployeeID = "3325", Identification = "40228267767", Role = "Consulta", PasswordHash = "7767" }; RegisterUser(user3325);
            User user3587 = new User() { Email = "yjreyes@radiocentro.com.do", EmployeeID = "3587", Identification = "40226092746", Role = "Consulta", PasswordHash = "2746" }; RegisterUser(user3587);
            User user3601 = new User() { Email = "hcepin@radiocentro.com.do", EmployeeID = "3601", Identification = "40226480503", Role = "Consulta", PasswordHash = "0503" }; RegisterUser(user3601);
            User user3615 = new User() { Email = "N/A", EmployeeID = "3615", Identification = "40238389759", Role = "Consulta", PasswordHash = "9759" }; RegisterUser(user3615);
            User user415 = new User() { Email = "N/A", EmployeeID = "415", Identification = "03103107045", Role = "Consulta", PasswordHash = "7045" }; RegisterUser(user415);
            User user3602 = new User() { Email = "N/A", EmployeeID = "3602", Identification = "40225227715", Role = "Consulta", PasswordHash = "7715" }; RegisterUser(user3602);
            User user96 = new User() { Email = "N/A", EmployeeID = "96", Identification = "03105103851", Role = "Consulta", PasswordHash = "3851" }; RegisterUser(user96);
            User user3305 = new User() { Email = "N/A", EmployeeID = "3305", Identification = "40209837034", Role = "Consulta", PasswordHash = "7034" }; RegisterUser(user3305);
            User user3310 = new User() { Email = "N/A", EmployeeID = "3310", Identification = "03104296615", Role = "Consulta", PasswordHash = "6615" }; RegisterUser(user3310);
            User user9365 = new User() { Email = "apichardo@radiocentro.com.do", EmployeeID = "9365", Identification = "03104932144", Role = "Consulta", PasswordHash = "2144" }; RegisterUser(user9365);
            User user9652 = new User() { Email = "N/A", EmployeeID = "9652", Identification = "03103821975", Role = "Consulta", PasswordHash = "1975" }; RegisterUser(user9652);
            User user1000 = new User() { Email = "N/A", EmployeeID = "1000", Identification = "03104465236", Role = "Consulta", PasswordHash = "5236" }; RegisterUser(user1000);
            User user4310 = new User() { Email = "N/A", EmployeeID = "4310", Identification = "03100235393", Role = "Consulta", PasswordHash = "5393" }; RegisterUser(user4310);
            User user9403 = new User() { Email = "avasquez@radiocentro.com.do", EmployeeID = "9403", Identification = "03103300905", Role = "Consulta", PasswordHash = "0905" }; RegisterUser(user9403);
            User user9645 = new User() { Email = "N/A", EmployeeID = "9645", Identification = "03103923623", Role = "Consulta", PasswordHash = "3623" }; RegisterUser(user9645);
            User user3586 = new User() { Email = "N/A", EmployeeID = "3586", Identification = "40213303379", Role = "Consulta", PasswordHash = "3379" }; RegisterUser(user3586);
            User user9079 = new User() { Email = "Dramos@radiocentro.com.do", EmployeeID = "9079", Identification = "03103664888", Role = "Consulta", PasswordHash = "4888" }; RegisterUser(user9079);
            User user61 = new User() { Email = "ralmonte@radiocentro.com.do", EmployeeID = "61", Identification = "11600010596", Role = "Consulta", PasswordHash = "0596" }; RegisterUser(user61);
            User user848 = new User() { Email = "cramirez@radiocentro.com.do", EmployeeID = "848", Identification = "22400088237", Role = "Consulta", PasswordHash = "8237" }; RegisterUser(user848);
            User user1502 = new User() { Email = "orodriguez@radiocentro.com.do", EmployeeID = "1502", Identification = "03100329345", Role = "Consulta", PasswordHash = "9345" }; RegisterUser(user1502);
            User user2407 = new User() { Email = "jsantos@radiocentro.com.do", EmployeeID = "2407", Identification = "05100046472", Role = "Consulta", PasswordHash = "6472" }; RegisterUser(user2407);
            User user3232 = new User() { Email = "ystgo@radiocentro.com.do", EmployeeID = "3232", Identification = "40222406429", Role = "Consulta", PasswordHash = "6429" }; RegisterUser(user3232);
            User user3235 = new User() { Email = "ydiaz@radiocentro.com.do", EmployeeID = "3235", Identification = "40222214906", Role = "Consulta", PasswordHash = "4906" }; RegisterUser(user3235);
            User user3401 = new User() { Email = "rguez@radiocentro.com.do", EmployeeID = "3401", Identification = "03104632843", Role = "Consulta", PasswordHash = "2843" }; RegisterUser(user3401);
            User user3535 = new User() { Email = "pgrullon@radiocentro.com.do", EmployeeID = "3535", Identification = "00109251694", Role = "Consulta", PasswordHash = "1694" }; RegisterUser(user3535);
            User user9897 = new User() { Email = "vgomez@radiocentro.com.do", EmployeeID = "9897", Identification = "05500013775", Role = "Consulta", PasswordHash = "3775" }; RegisterUser(user9897);
            User user343 = new User() { Email = "N/A", EmployeeID = "343", Identification = "03104457688", Role = "Consulta", PasswordHash = "7688" }; RegisterUser(user343);
            User user419 = new User() { Email = "vsantana@radiocentro.com.do", EmployeeID = "419", Identification = "04500235041", Role = "Consulta", PasswordHash = "5041" }; RegisterUser(user419);
            User user532 = new User() { Email = "jduarte@radiocentro.com.do", EmployeeID = "532", Identification = "05601780389", Role = "Consulta", PasswordHash = "0389" }; RegisterUser(user532);
            User user535 = new User() { Email = "pblanco@radiocentro.com.do", EmployeeID = "535", Identification = "03100940414", Role = "Consulta", PasswordHash = "0414" }; RegisterUser(user535);
            User user575 = new User() { Email = "N/A", EmployeeID = "575", Identification = "03105162568", Role = "Consulta", PasswordHash = "2568" }; RegisterUser(user575);
            User user579 = new User() { Email = "N/A", EmployeeID = "579", Identification = "03104880673", Role = "Consulta", PasswordHash = "0673" }; RegisterUser(user579);
            User user686 = new User() { Email = "hbueno@radiocentro.com.do", EmployeeID = "686", Identification = "03105697803", Role = "Consulta", PasswordHash = "7803" }; RegisterUser(user686);
            User user687 = new User() { Email = "N/A", EmployeeID = "687", Identification = "40212822197", Role = "Consulta", PasswordHash = "2197" }; RegisterUser(user687);
            User user932 = new User() { Email = "N/A", EmployeeID = "932", Identification = "40239572882", Role = "Consulta", PasswordHash = "2882" }; RegisterUser(user932);
            User user934 = new User() { Email = "N/A", EmployeeID = "934", Identification = "03105167526", Role = "Consulta", PasswordHash = "7526" }; RegisterUser(user934);
            User user2010 = new User() { Email = "eabreu@radiocentro.com.do", EmployeeID = "2010", Identification = "03102556879", Role = "Consulta", PasswordHash = "6879" }; RegisterUser(user2010);
            User user3214 = new User() { Email = "N/A", EmployeeID = "3214", Identification = "40227516347", Role = "Consulta", PasswordHash = "6347" }; RegisterUser(user3214);
            User user3217 = new User() { Email = "N/A", EmployeeID = "3217", Identification = "40221148527", Role = "Consulta", PasswordHash = "8527" }; RegisterUser(user3217);
            User user3303 = new User() { Email = "N/A", EmployeeID = "3303", Identification = "40211042714", Role = "Consulta", PasswordHash = "2714" }; RegisterUser(user3303);
            User user3307 = new User() { Email = "N/A", EmployeeID = "3307", Identification = "03104797679", Role = "Consulta", PasswordHash = "7679" }; RegisterUser(user3307);
            User user3324 = new User() { Email = "N/A", EmployeeID = "3324", Identification = "40227124399", Role = "Consulta", PasswordHash = "4399" }; RegisterUser(user3324);
            User user3327 = new User() { Email = "N/A", EmployeeID = "3327", Identification = "40222530558", Role = "Consulta", PasswordHash = "0558" }; RegisterUser(user3327);
            User user3330 = new User() { Email = "N/A", EmployeeID = "3330", Identification = "40219564404", Role = "Consulta", PasswordHash = "4404" }; RegisterUser(user3330);
            User user3331 = new User() { Email = "N/A", EmployeeID = "3331", Identification = "03104732940", Role = "Consulta", PasswordHash = "2940" }; RegisterUser(user3331);
            User user3451 = new User() { Email = "N/A", EmployeeID = "3451", Identification = "40226665145", Role = "Consulta", PasswordHash = "5145" }; RegisterUser(user3451);
            User user3452 = new User() { Email = "N/A", EmployeeID = "3452", Identification = "40244773111", Role = "Consulta", PasswordHash = "3111" }; RegisterUser(user3452);
            User user3549 = new User() { Email = "N/A", EmployeeID = "3549", Identification = "03104562982", Role = "Consulta", PasswordHash = "2982" }; RegisterUser(user3549);
            User user3551 = new User() { Email = "N/A", EmployeeID = "3551", Identification = "40215341138", Role = "Consulta", PasswordHash = "1138" }; RegisterUser(user3551);
            User user3552 = new User() { Email = "N/A", EmployeeID = "3552", Identification = "40212793588", Role = "Consulta", PasswordHash = "3588" }; RegisterUser(user3552);
            User user3580 = new User() { Email = "N/A", EmployeeID = "3580", Identification = "40221029123", Role = "Consulta", PasswordHash = "9123" }; RegisterUser(user3580);
            User user3629 = new User() { Email = "N/A", EmployeeID = "3629", Identification = "40223329273", Role = "Consulta", PasswordHash = "9273" }; RegisterUser(user3629);
            User user3630 = new User() { Email = "N/A", EmployeeID = "3630", Identification = "40211461617", Role = "Consulta", PasswordHash = "1617" }; RegisterUser(user3630);
            User user3647 = new User() { Email = "N/A", EmployeeID = "3647", Identification = "03104435619", Role = "Consulta", PasswordHash = "5619" }; RegisterUser(user3647);
            User user3648 = new User() { Email = "N/A", EmployeeID = "3648", Identification = "03200406860", Role = "Consulta", PasswordHash = "6860" }; RegisterUser(user3648);
            User user3649 = new User() { Email = "N/A", EmployeeID = "3649", Identification = "40228501884", Role = "Consulta", PasswordHash = "1884" }; RegisterUser(user3649);
            User user3650 = new User() { Email = "N/A", EmployeeID = "3650", Identification = "40237354606", Role = "Consulta", PasswordHash = "4606" }; RegisterUser(user3650);
            User user3651 = new User() { Email = "N/A", EmployeeID = "3651", Identification = "40251144461", Role = "Consulta", PasswordHash = "4461" }; RegisterUser(user3651);
            User user3662 = new User() { Email = "N/A", EmployeeID = "3662", Identification = "40225007752", Role = "Consulta", PasswordHash = "7752" }; RegisterUser(user3662);
            User user9066 = new User() { Email = "N/A", EmployeeID = "9066", Identification = "05400161583", Role = "Consulta", PasswordHash = "1583" }; RegisterUser(user9066);
            User user9149 = new User() { Email = "N/A", EmployeeID = "9149", Identification = "03600134013", Role = "Consulta", PasswordHash = "4013" }; RegisterUser(user9149);
            User user534 = new User() { Email = "N/A", EmployeeID = "534", Identification = "03104922368", Role = "Consulta", PasswordHash = "2368" }; RegisterUser(user534);
            User user576 = new User() { Email = "N/A", EmployeeID = "576", Identification = "03104571140", Role = "Consulta", PasswordHash = "1140" }; RegisterUser(user576);
            User user577 = new User() { Email = "N/A", EmployeeID = "577", Identification = "04900890841", Role = "Consulta", PasswordHash = "0841" }; RegisterUser(user577);
            User user716 = new User() { Email = "N/A", EmployeeID = "716", Identification = "03104193333", Role = "Consulta", PasswordHash = "3333" }; RegisterUser(user716);
            User user3132 = new User() { Email = "N/A", EmployeeID = "3132", Identification = "04100216607", Role = "Consulta", PasswordHash = "6607" }; RegisterUser(user3132);
            User user3209 = new User() { Email = "N/A", EmployeeID = "3209", Identification = "40231132891", Role = "Consulta", PasswordHash = "2891" }; RegisterUser(user3209);
            User user3210 = new User() { Email = "N/A", EmployeeID = "3210", Identification = "10100118388", Role = "Consulta", PasswordHash = "8388" }; RegisterUser(user3210);
            User user3332 = new User() { Email = "N/A", EmployeeID = "3332", Identification = "06100209086", Role = "Consulta", PasswordHash = "9086" }; RegisterUser(user3332);
            User user3447 = new User() { Email = "N/A", EmployeeID = "3447", Identification = "03105601136", Role = "Consulta", PasswordHash = "1136" }; RegisterUser(user3447);
            User user3448 = new User() { Email = "N/A", EmployeeID = "3448", Identification = "40210267221", Role = "Consulta", PasswordHash = "7221" }; RegisterUser(user3448);
            User user3515 = new User() { Email = "N/A", EmployeeID = "3515", Identification = "40211619685", Role = "Consulta", PasswordHash = "9685" }; RegisterUser(user3515);
            User user3566 = new User() { Email = "N/A", EmployeeID = "3566", Identification = "05300263000", Role = "Consulta", PasswordHash = "3000" }; RegisterUser(user3566);
            User user3567 = new User() { Email = "N/A", EmployeeID = "3567", Identification = "03104180702", Role = "Consulta", PasswordHash = "0702" }; RegisterUser(user3567);
            User user3568 = new User() { Email = "jtejada@radiocentro.com.do", EmployeeID = "3568", Identification = "40237272386", Role = "Consulta", PasswordHash = "2386" }; RegisterUser(user3568);
            User user3622 = new User() { Email = "N/A", EmployeeID = "3622", Identification = "10200125143", Role = "Consulta", PasswordHash = "5143" }; RegisterUser(user3622);
            User user3646 = new User() { Email = "N/A", EmployeeID = "3646", Identification = "03105776698", Role = "Consulta", PasswordHash = "6698" }; RegisterUser(user3646);
            User user3663 = new User() { Email = "N/A", EmployeeID = "3663", Identification = "03105714244", Role = "Consulta", PasswordHash = "4244" }; RegisterUser(user3663);
            User user4882 = new User() { Email = "N/A", EmployeeID = "4882", Identification = "03100151392", Role = "Consulta", PasswordHash = "1392" }; RegisterUser(user4882);
            User user5246 = new User() { Email = "N/A", EmployeeID = "5246", Identification = "03100675846", Role = "Consulta", PasswordHash = "5846" }; RegisterUser(user5246);
            User user9429 = new User() { Email = "N/A", EmployeeID = "9429", Identification = "03500022672", Role = "Consulta", PasswordHash = "2672" }; RegisterUser(user9429);
            User user813 = new User() { Email = "yvalerio@radiocentro.com.do", EmployeeID = "813", Identification = "40224355038", Role = "Consulta", PasswordHash = "5038" }; RegisterUser(user813);
            User user2692 = new User() { Email = "zcollado@radiocentro.com.do", EmployeeID = "2692", Identification = "03200113938", Role = "Consulta", PasswordHash = "3938" }; RegisterUser(user2692);
            User user3095 = new User() { Email = "lgarcia@radiocentro.com.do", EmployeeID = "3095", Identification = "03200358061", Role = "Consulta", PasswordHash = "8061" }; RegisterUser(user3095);
            User user3302 = new User() { Email = "cenedina@radiocentro.com.do", EmployeeID = "3302", Identification = "06800351576", Role = "Consulta", PasswordHash = "1576" }; RegisterUser(user3302);
            User user3484 = new User() { Email = "spimentel@radiocentro.com.do", EmployeeID = "3484", Identification = "40220263335", Role = "Consulta", PasswordHash = "3335" }; RegisterUser(user3484);
            User user3562 = new User() { Email = "ataveras@radiocentro.com.do", EmployeeID = "3562", Identification = "03104087790", Role = "Consulta", PasswordHash = "7790" }; RegisterUser(user3562);
            User user221 = new User() { Email = "jnovas@radiocentro.com.do", EmployeeID = "221", Identification = "09300773554", Role = "Consulta", PasswordHash = "3554" }; RegisterUser(user221);
            User user794 = new User() { Email = "acruz@radiocentro.com.do", EmployeeID = "794", Identification = "40238649343", Role = "Consulta", PasswordHash = "9343" }; RegisterUser(user794);
            User user875 = new User() { Email = "N/A", EmployeeID = "875", Identification = "00107010456", Role = "Consulta", PasswordHash = "0456" }; RegisterUser(user875);
            User user3079 = new User() { Email = "N/A", EmployeeID = "3079", Identification = "22900255922", Role = "Consulta", PasswordHash = "5922" }; RegisterUser(user3079);
            User user3118 = new User() { Email = "N/A", EmployeeID = "3118", Identification = "00119416477", Role = "Consulta", PasswordHash = "6477" }; RegisterUser(user3118);
            User user3137 = new User() { Email = "N/A", EmployeeID = "3137", Identification = "40240293353", Role = "Consulta", PasswordHash = "3353" }; RegisterUser(user3137);
            User user3139 = new User() { Email = "N/A", EmployeeID = "3139", Identification = "00118417625", Role = "Consulta", PasswordHash = "7625" }; RegisterUser(user3139);
            User user3294 = new User() { Email = "N/A", EmployeeID = "3294", Identification = "01100371317", Role = "Consulta", PasswordHash = "1317" }; RegisterUser(user3294);
            User user3295 = new User() { Email = "N/A", EmployeeID = "3295", Identification = "01300509096", Role = "Consulta", PasswordHash = "9096" }; RegisterUser(user3295);
            User user3297 = new User() { Email = "N/A", EmployeeID = "3297", Identification = "40229417627", Role = "Consulta", PasswordHash = "7627" }; RegisterUser(user3297);
            User user3298 = new User() { Email = "N/A", EmployeeID = "3298", Identification = "22900124755", Role = "Consulta", PasswordHash = "4755" }; RegisterUser(user3298);
            User user3345 = new User() { Email = "N/A", EmployeeID = "3345", Identification = "40224579207", Role = "Consulta", PasswordHash = "9207" }; RegisterUser(user3345);
            User user3347 = new User() { Email = "N/A", EmployeeID = "3347", Identification = "40209463138", Role = "Consulta", PasswordHash = "3138" }; RegisterUser(user3347);
            User user3406 = new User() { Email = "N/A", EmployeeID = "3406", Identification = "40227155252", Role = "Consulta", PasswordHash = "5252" }; RegisterUser(user3406);
            User user3435 = new User() { Email = "cmendoza@radiocentro.com.do", EmployeeID = "3435", Identification = "00112931589", Role = "Consulta", PasswordHash = "1589" }; RegisterUser(user3435);
            User user3471 = new User() { Email = "asoriano@radiocentro.com.do", EmployeeID = "3471", Identification = "01001205804", Role = "Consulta", PasswordHash = "5804" }; RegisterUser(user3471);
            User user3472 = new User() { Email = "jaquino@radiocentro.com.do", EmployeeID = "3472", Identification = "00115038002", Role = "Consulta", PasswordHash = "8002" }; RegisterUser(user3472);
            User user3478 = new User() { Email = "N/A", EmployeeID = "3478", Identification = "40229487851", Role = "Consulta", PasswordHash = "7851" }; RegisterUser(user3478);
            User user3480 = new User() { Email = "N/A", EmployeeID = "3480", Identification = "40240890851", Role = "Consulta", PasswordHash = "0851" }; RegisterUser(user3480);
            User user3482 = new User() { Email = "N/A", EmployeeID = "3482", Identification = "40227464779", Role = "Consulta", PasswordHash = "4779" }; RegisterUser(user3482);
            User user3510 = new User() { Email = "N/A", EmployeeID = "3510", Identification = "12800009628", Role = "Consulta", PasswordHash = "9628" }; RegisterUser(user3510);
            User user3512 = new User() { Email = "N/A", EmployeeID = "3512", Identification = "40230961886", Role = "Consulta", PasswordHash = "1886" }; RegisterUser(user3512);
            User user3514 = new User() { Email = "N/A", EmployeeID = "3514", Identification = "40239549831", Role = "Consulta", PasswordHash = "9831" }; RegisterUser(user3514);
            User user3561 = new User() { Email = "N/A", EmployeeID = "3561", Identification = "40244828501", Role = "Consulta", PasswordHash = "8501" }; RegisterUser(user3561);
            User user3604 = new User() { Email = "N/A", EmployeeID = "3604", Identification = "01001189701", Role = "Consulta", PasswordHash = "9701" }; RegisterUser(user3604);
            User user9808 = new User() { Email = "N/A", EmployeeID = "9808", Identification = "01000281012", Role = "Consulta", PasswordHash = "1012" }; RegisterUser(user9808);
            User user41 = new User() { Email = "N/A", EmployeeID = "41", Identification = "14000020728", Role = "Consulta", PasswordHash = "0728" }; RegisterUser(user41);
            User user209 = new User() { Email = "N/A", EmployeeID = "209", Identification = "01100384914", Role = "Consulta", PasswordHash = "4914" }; RegisterUser(user209);
            User user219 = new User() { Email = "N/A", EmployeeID = "219", Identification = "09300684124", Role = "Consulta", PasswordHash = "4124" }; RegisterUser(user219);
            User user289 = new User() { Email = "N/A", EmployeeID = "289", Identification = "09300728509", Role = "Consulta", PasswordHash = "8509" }; RegisterUser(user289);
            User user359 = new User() { Email = "N/A", EmployeeID = "359", Identification = "09300319705", Role = "Consulta", PasswordHash = "9705" }; RegisterUser(user359);
            User user360 = new User() { Email = "N/A", EmployeeID = "360", Identification = "01100286655", Role = "Consulta", PasswordHash = "6655" }; RegisterUser(user360);
            User user469 = new User() { Email = "N/A", EmployeeID = "469", Identification = "00201165644", Role = "Consulta", PasswordHash = "5644" }; RegisterUser(user469);
            User user637 = new User() { Email = "N/A", EmployeeID = "637", Identification = "00201552734", Role = "Consulta", PasswordHash = "2734" }; RegisterUser(user637);
            User user724 = new User() { Email = "N/A", EmployeeID = "724", Identification = "09300221497", Role = "Consulta", PasswordHash = "1497" }; RegisterUser(user724);
            User user970 = new User() { Email = "N/A", EmployeeID = "970", Identification = "09300624633", Role = "Consulta", PasswordHash = "4633" }; RegisterUser(user970);
            User user1031 = new User() { Email = "N/A", EmployeeID = "1031", Identification = "01100388816", Role = "Consulta", PasswordHash = "8816" }; RegisterUser(user1031);
            User user3074 = new User() { Email = "N/A", EmployeeID = "3074", Identification = "14000020439", Role = "Consulta", PasswordHash = "0439" }; RegisterUser(user3074);
            User user3075 = new User() { Email = "N/A", EmployeeID = "3075", Identification = "01100272101", Role = "Consulta", PasswordHash = "2101" }; RegisterUser(user3075);
            User user3120 = new User() { Email = "N/A", EmployeeID = "3120", Identification = "40211914862", Role = "Consulta", PasswordHash = "4862" }; RegisterUser(user3120);
            User user3124 = new User() { Email = "N/A", EmployeeID = "3124", Identification = "40226576946", Role = "Consulta", PasswordHash = "6946" }; RegisterUser(user3124);
            User user3146 = new User() { Email = "N/A", EmployeeID = "3146", Identification = "01201262217", Role = "Consulta", PasswordHash = "2217" }; RegisterUser(user3146);
            User user3239 = new User() { Email = "N/A", EmployeeID = "3239", Identification = "09300369239", Role = "Consulta", PasswordHash = "9239" }; RegisterUser(user3239);
            User user3240 = new User() { Email = "N/A", EmployeeID = "3240", Identification = "00118145259", Role = "Consulta", PasswordHash = "5259" }; RegisterUser(user3240);
            User user3255 = new User() { Email = "N/A", EmployeeID = "3255", Identification = "01300493911", Role = "Consulta", PasswordHash = "3911" }; RegisterUser(user3255);
            User user3343 = new User() { Email = "N/A", EmployeeID = "3343", Identification = "00201322112", Role = "Consulta", PasswordHash = "2112" }; RegisterUser(user3343);
            User user3344 = new User() { Email = "N/A", EmployeeID = "3344", Identification = "00201189040", Role = "Consulta", PasswordHash = "9040" }; RegisterUser(user3344);
            User user3528 = new User() { Email = "lvasquez@radiocentro.com.do", EmployeeID = "3528", Identification = "40226083109", Role = "Consulta", PasswordHash = "3109" }; RegisterUser(user3528);
            User user3594 = new User() { Email = "N/A", EmployeeID = "3594", Identification = "40220464057", Role = "Consulta", PasswordHash = "4057" }; RegisterUser(user3594);
            User user3595 = new User() { Email = "N/A", EmployeeID = "3595", Identification = "09300768554", Role = "Consulta", PasswordHash = "8554" }; RegisterUser(user3595);
            User user3596 = new User() { Email = "N/A", EmployeeID = "3596", Identification = "40241030853", Role = "Consulta", PasswordHash = "0853" }; RegisterUser(user3596);
            User user3597 = new User() { Email = "N/A", EmployeeID = "3597", Identification = "00201048519", Role = "Consulta", PasswordHash = "8519" }; RegisterUser(user3597);
            User user3598 = new User() { Email = "N/A", EmployeeID = "3598", Identification = "40237263666", Role = "Consulta", PasswordHash = "3666" }; RegisterUser(user3598);
            User user3599 = new User() { Email = "N/A", EmployeeID = "3599", Identification = "09300767242", Role = "Consulta", PasswordHash = "7242" }; RegisterUser(user3599);
            User user3608 = new User() { Email = "jferreras@radiocentro.com.do", EmployeeID = "3608", Identification = "22301579276", Role = "Consulta", PasswordHash = "9276" }; RegisterUser(user3608);
            User user3632 = new User() { Email = "N/A", EmployeeID = "3632", Identification = "40240261004", Role = "Consulta", PasswordHash = "1004" }; RegisterUser(user3632);
            User user3633 = new User() { Email = "N/A", EmployeeID = "3633", Identification = "40232396651", Role = "Consulta", PasswordHash = "6651" }; RegisterUser(user3633);
            User user4369 = new User() { Email = "N/A", EmployeeID = "4369", Identification = "09300116192", Role = "Consulta", PasswordHash = "6192" }; RegisterUser(user4369);
            User user5244 = new User() { Email = "N/A", EmployeeID = "5244", Identification = "00103889218", Role = "Consulta", PasswordHash = "9218" }; RegisterUser(user5244);
            User user8649 = new User() { Email = "N/A", EmployeeID = "8649", Identification = "09300116515", Role = "Consulta", PasswordHash = "6515" }; RegisterUser(user8649);
            User user9031 = new User() { Email = "N/A", EmployeeID = "9031", Identification = "02300159346", Role = "Consulta", PasswordHash = "9346" }; RegisterUser(user9031);
            User user9267 = new User() { Email = "N/A", EmployeeID = "9267", Identification = "09300122117", Role = "Consulta", PasswordHash = "2117" }; RegisterUser(user9267);
            User user9423 = new User() { Email = "N/A", EmployeeID = "9423", Identification = "00200717106", Role = "Consulta", PasswordHash = "7106" }; RegisterUser(user9423);
            User user9454 = new User() { Email = "N/A", EmployeeID = "9454", Identification = "09300600385", Role = "Consulta", PasswordHash = "0385" }; RegisterUser(user9454);
            User user9529 = new User() { Email = "N/A", EmployeeID = "9529", Identification = "01100174349", Role = "Consulta", PasswordHash = "4349" }; RegisterUser(user9529);
            User user9541 = new User() { Email = "N/A", EmployeeID = "9541", Identification = "10400167622", Role = "Consulta", PasswordHash = "7622" }; RegisterUser(user9541);
            User user9810 = new User() { Email = "N/A", EmployeeID = "9810", Identification = "09300166007", Role = "Consulta", PasswordHash = "6007" }; RegisterUser(user9810);

            return Content("Process completed");
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