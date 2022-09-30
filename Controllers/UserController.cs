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
            //User user1002 = new User() { Email = "N/A", EmployeeID = "1002", Identification = "00107911281", Role = "Consulta", PasswordHash = "1281" }; RegisterUser(user1002);
            //User user1364 = new User() { Email = "Radiocentro@claro.net.do", EmployeeID = "1364", Identification = "00109211870", Role = "Consulta", PasswordHash = "1870" }; RegisterUser(user1364);
            //User user1673 = new User() { Email = "N/A", EmployeeID = "1673", Identification = "00104125117", Role = "Consulta", PasswordHash = "5117" }; RegisterUser(user1673);
            //User user1841 = new User() { Email = "N/A", EmployeeID = "1841", Identification = "00100679836", Role = "Consulta", PasswordHash = "9836" }; RegisterUser(user1841);
            //User user9432 = new User() { Email = "N/A", EmployeeID = "9432", Identification = "00108194283", Role = "Consulta", PasswordHash = "4283" }; RegisterUser(user9432);
            //User user3463 = new User() { Email = "lguerra@radiocentro.com.do", EmployeeID = "3463", Identification = "40223264439", Role = "Consulta", PasswordHash = "4439" }; RegisterUser(user3463);
            //User user2666 = new User() { Email = "miryamdr@radiocentro.com.do", EmployeeID = "2666", Identification = "00101221745", Role = "Consulta", PasswordHash = "1745" }; RegisterUser(user2666);
            //User user3383 = new User() { Email = "maguzman@radiocentro.com.do", EmployeeID = "3383", Identification = "22300116377", Role = "Consulta", PasswordHash = "6377" }; RegisterUser(user3383);
            //User user3385 = new User() { Email = "ltroncoso@radiocentro.com.do", EmployeeID = "3385", Identification = "40226059026", Role = "Consulta", PasswordHash = "9026" }; RegisterUser(user3385);
            //User user3584 = new User() { Email = "adelarosa@radiocentro.com.do", EmployeeID = "3584", Identification = "01600170367", Role = "Consulta", PasswordHash = "0367" }; RegisterUser(user3584);
            //User user4763 = new User() { Email = "jbrito@radiocentro.com.do", EmployeeID = "4763", Identification = "04900608490", Role = "Consulta", PasswordHash = "8490" }; RegisterUser(user4763);
            //User user4846 = new User() { Email = "staveras@radiocentro.com.do", EmployeeID = "4846", Identification = "00116160490", Role = "Consulta", PasswordHash = "0490" }; RegisterUser(user4846);
            //User user3631 = new User() { Email = "rbaez@radiocentro.com.do", EmployeeID = "3631", Identification = "00116209958", Role = "Consulta", PasswordHash = "9958" }; RegisterUser(user3631);
            //User user525 = new User() { Email = "svicente@radiocentro.com.do", EmployeeID = "525", Identification = "40224290334", Role = "Consulta", PasswordHash = "0334" }; RegisterUser(user525);
            //User user601 = new User() { Email = "cpinales@radiocentro.com.do", EmployeeID = "601", Identification = "01300368220", Role = "Consulta", PasswordHash = "8220" }; RegisterUser(user601);
            //User user781 = new User() { Email = "dlacruz@radiocentro.com.do", EmployeeID = "781", Identification = "00101022671", Role = "Consulta", PasswordHash = "2671" }; RegisterUser(user781);
            //User user1350 = new User() { Email = "ocruz@radiocentro.com.do", EmployeeID = "1350", Identification = "00101338598", Role = "Consulta", PasswordHash = "8598" }; RegisterUser(user1350);
            //User user1383 = new User() { Email = "afernandez@radiocentro.com.do", EmployeeID = "1383", Identification = "00107591836", Role = "Consulta", PasswordHash = "1836" }; RegisterUser(user1383);
            //User user1567 = new User() { Email = "fgarcia@radiocentro.com.do", EmployeeID = "1567", Identification = "00109643478", Role = "Consulta", PasswordHash = "3478" }; RegisterUser(user1567);

            ChangePasswordMassive("1002", "11281");
            ChangePasswordMassive("1364", "11870");
            ChangePasswordMassive("1673", "25117");
            ChangePasswordMassive("1841", "79836");
            ChangePasswordMassive("9432", "94283");
            ChangePasswordMassive("3463", "64439");
            ChangePasswordMassive("2666", "21745");
            ChangePasswordMassive("3383", "16377");
            ChangePasswordMassive("3385", "59026");
            ChangePasswordMassive("3584", "70367");
            ChangePasswordMassive("3670", "92158");
            ChangePasswordMassive("4763", "08490");
            ChangePasswordMassive("4846", "60490");
            ChangePasswordMassive("3631", "09958");
            ChangePasswordMassive("525", "90334");
            ChangePasswordMassive("601", "68220");
            ChangePasswordMassive("781", "22671");
            ChangePasswordMassive("1350", "38598");
            ChangePasswordMassive("1383", "91836");
            ChangePasswordMassive("1567", "43478");
            ChangePasswordMassive("2769", "86281");
            ChangePasswordMassive("3281", "35089");
            ChangePasswordMassive("3282", "80630");
            ChangePasswordMassive("3417", "28877");
            ChangePasswordMassive("3522", "51711");
            ChangePasswordMassive("3585", "71651");
            ChangePasswordMassive("5250", "01665");
            ChangePasswordMassive("5280", "73959");
            ChangePasswordMassive("5529", "77943");
            ChangePasswordMassive("8622", "46860");
            ChangePasswordMassive("9138", "37937");
            ChangePasswordMassive("9343", "51122");
            ChangePasswordMassive("9982", "53868");
            ChangePasswordMassive("161", "07342");
            ChangePasswordMassive("240", "09099");
            ChangePasswordMassive("241", "93152");
            ChangePasswordMassive("466", "77660");
            ChangePasswordMassive("590", "85761");
            ChangePasswordMassive("618", "65666");
            ChangePasswordMassive("621", "16295");
            ChangePasswordMassive("3181", "25907");
            ChangePasswordMassive("3256", "35978");
            ChangePasswordMassive("3268", "13591");
            ChangePasswordMassive("3269", "96336");
            ChangePasswordMassive("3364", "94815");
            ChangePasswordMassive("3620", "32561");
            ChangePasswordMassive("3655", "75598");
            ChangePasswordMassive("3656", "02927");
            ChangePasswordMassive("4913", "66986");
            ChangePasswordMassive("9800", "44987");
            ChangePasswordMassive("1804", "51064");
            ChangePasswordMassive("3473", "19144");
            ChangePasswordMassive("3588", "32642");
            ChangePasswordMassive("8659", "04649");
            ChangePasswordMassive("1307", "53144");
            ChangePasswordMassive("3519", "14602");
            ChangePasswordMassive("3603", "31040");
            ChangePasswordMassive("1744", "40018");
            ChangePasswordMassive("2168", "15874");
            ChangePasswordMassive("2821", "97899");
            ChangePasswordMassive("3611", "91423");
            ChangePasswordMassive("1017", "45724");
            ChangePasswordMassive("872", "97984");
            ChangePasswordMassive("1146", "50967");
            ChangePasswordMassive("1548", "66685");
            ChangePasswordMassive("1663", "66521");
            ChangePasswordMassive("2871", "12145");
            ChangePasswordMassive("3224", "00413");
            ChangePasswordMassive("3318", "49825");
            ChangePasswordMassive("3453", "05141");
            ChangePasswordMassive("3609", "10698");
            ChangePasswordMassive("3665", "82654");
            ChangePasswordMassive("3666", "92976");
            ChangePasswordMassive("8681", "63181");
            ChangePasswordMassive("8715", "23482");
            ChangePasswordMassive("8913", "36928");
            ChangePasswordMassive("9670", "65341");
            ChangePasswordMassive("778", "39468");
            ChangePasswordMassive("1522", "46523");
            ChangePasswordMassive("3104", "18552");
            ChangePasswordMassive("3624", "44868");
            ChangePasswordMassive("5199", "65848");
            ChangePasswordMassive("9420", "51199");
            ChangePasswordMassive("2315", "92040");
            ChangePasswordMassive("4263", "62673");
            ChangePasswordMassive("67", "03822");
            ChangePasswordMassive("1211", "09239");
            ChangePasswordMassive("4927", "11797");
            ChangePasswordMassive("5497", "95364");
            ChangePasswordMassive("8311", "52582");
            ChangePasswordMassive("9164", "97134");
            ChangePasswordMassive("9818", "39601");
            ChangePasswordMassive("9954", "41508");
            ChangePasswordMassive("153", "57619");
            ChangePasswordMassive("745", "29683");
            ChangePasswordMassive("3454", "47400");
            ChangePasswordMassive("1217", "36720");
            ChangePasswordMassive("3105", "22955");
            ChangePasswordMassive("3389", "30565");
            ChangePasswordMassive("9060", "92475");
            ChangePasswordMassive("3393", "80313");
            ChangePasswordMassive("405", "30240");
            ChangePasswordMassive("620", "16959");
            ChangePasswordMassive("633", "86116");
            ChangePasswordMassive("980", "57153");
            ChangePasswordMassive("998", "78118");
            ChangePasswordMassive("1011", "07644");
            ChangePasswordMassive("1859", "62745");
            ChangePasswordMassive("2292", "46753");
            ChangePasswordMassive("3010", "50584");
            ChangePasswordMassive("3102", "71403");
            ChangePasswordMassive("3118", "16477");
            ChangePasswordMassive("3226", "30698");
            ChangePasswordMassive("3251", "01713");
            ChangePasswordMassive("3291", "40720");
            ChangePasswordMassive("3300", "04340");
            ChangePasswordMassive("3336", "14328");
            ChangePasswordMassive("3338", "48154");
            ChangePasswordMassive("3341", "85695");
            ChangePasswordMassive("3350", "43002");
            ChangePasswordMassive("3352", "63594");
            ChangePasswordMassive("3375", "90384");
            ChangePasswordMassive("3405", "84043");
            ChangePasswordMassive("3410", "56132");
            ChangePasswordMassive("3421", "91695");
            ChangePasswordMassive("3430", "89067");
            ChangePasswordMassive("3431", "41158");
            ChangePasswordMassive("3459", "13749");
            ChangePasswordMassive("3487", "45007");
            ChangePasswordMassive("3498", "20029");
            ChangePasswordMassive("3499", "76002");
            ChangePasswordMassive("3502", "76988");
            ChangePasswordMassive("3510", "09628");
            ChangePasswordMassive("3571", "73643");
            ChangePasswordMassive("3572", "86415");
            ChangePasswordMassive("3582", "27923");
            ChangePasswordMassive("3583", "70821");
            ChangePasswordMassive("3605", "57295");
            ChangePasswordMassive("3626", "03028");
            ChangePasswordMassive("3634", "87345");
            ChangePasswordMassive("3635", "63887");
            ChangePasswordMassive("3636", "07351");
            ChangePasswordMassive("3637", "68455");
            ChangePasswordMassive("3639", "75572");
            ChangePasswordMassive("3659", "41107");
            ChangePasswordMassive("3671", "54497");
            ChangePasswordMassive("3672", "42136");
            ChangePasswordMassive("3674", "17590");
            ChangePasswordMassive("3699", "93912");
            ChangePasswordMassive("3700", "47246");
            ChangePasswordMassive("3701", "31942");
            ChangePasswordMassive("3704", "70089");
            ChangePasswordMassive("4277", "94728");
            ChangePasswordMassive("4757", "30805");
            ChangePasswordMassive("231", "09874");
            ChangePasswordMassive("303", "02994");
            ChangePasswordMassive("485", "11388");
            ChangePasswordMassive("625", "88445");
            ChangePasswordMassive("665", "38162");
            ChangePasswordMassive("839", "16364");
            ChangePasswordMassive("923", "86016");
            ChangePasswordMassive("994", "30408");
            ChangePasswordMassive("3020", "25756");
            ChangePasswordMassive("3184", "51926");
            ChangePasswordMassive("3252", "19130");
            ChangePasswordMassive("3358", "76306");
            ChangePasswordMassive("3398", "64803");
            ChangePasswordMassive("3399", "37039");
            ChangePasswordMassive("3407", "03546");
            ChangePasswordMassive("3412", "70372");
            ChangePasswordMassive("3434", "83455");
            ChangePasswordMassive("3441", "75683");
            ChangePasswordMassive("3507", "77988");
            ChangePasswordMassive("3530", "66599");
            ChangePasswordMassive("3590", "57773");
            ChangePasswordMassive("3592", "44572");
            ChangePasswordMassive("3606", "33314");
            ChangePasswordMassive("3607", "69942");
            ChangePasswordMassive("3653", "70935");
            ChangePasswordMassive("3657", "20029");
            ChangePasswordMassive("3660", "47389");
            ChangePasswordMassive("3661", "14441");
            ChangePasswordMassive("3676", "06727");
            ChangePasswordMassive("3677", "11040");
            ChangePasswordMassive("3678", "33696");
            ChangePasswordMassive("479", "05487");
            ChangePasswordMassive("615", "99209");
            ChangePasswordMassive("999", "50611");
            ChangePasswordMassive("3290", "40581");
            ChangePasswordMassive("3357", "57929");
            ChangePasswordMassive("3390", "98075");
            ChangePasswordMassive("3391", "05108");
            ChangePasswordMassive("3392", "44595");
            ChangePasswordMassive("3414", "74132");
            ChangePasswordMassive("3464", "18998");
            ChangePasswordMassive("3558", "42123");
            ChangePasswordMassive("3618", "26520");
            ChangePasswordMassive("3642", "57000");
            ChangePasswordMassive("3703", "48299");
            ChangePasswordMassive("3706", "64564");
            ChangePasswordMassive("3710", "01117");
            ChangePasswordMassive("3711", "09486");
            ChangePasswordMassive("4415", "20295");
            ChangePasswordMassive("4858", "40448");
            ChangePasswordMassive("9318", "75188");
            ChangePasswordMassive("9427", "87714");
            ChangePasswordMassive("301", "57162");
            ChangePasswordMassive("478", "49107");
            ChangePasswordMassive("1015", "14975");
            ChangePasswordMassive("1086", "07364");
            ChangePasswordMassive("1119", "98709");
            ChangePasswordMassive("1680", "17603");
            ChangePasswordMassive("1692", "45541");
            ChangePasswordMassive("2082", "88426");
            ChangePasswordMassive("2617", "09448");
            ChangePasswordMassive("3129", "26103");
            ChangePasswordMassive("3183", "57485");
            ChangePasswordMassive("3319", "18484");
            ChangePasswordMassive("3381", "88816");
            ChangePasswordMassive("3511", "08651");
            ChangePasswordMassive("3518", "48490");
            ChangePasswordMassive("3531", "69040");
            ChangePasswordMassive("3565", "82564");
            ChangePasswordMassive("3569", "29568");
            ChangePasswordMassive("3612", "03686");
            ChangePasswordMassive("3667", "01360");
            ChangePasswordMassive("8302", "03407");
            ChangePasswordMassive("9535", "72499");
            ChangePasswordMassive("9960", "25610");
            ChangePasswordMassive("1215", "82235");
            ChangePasswordMassive("1569", "18219");
            ChangePasswordMassive("3408", "32536");
            ChangePasswordMassive("3664", "52555");
            ChangePasswordMassive("4424", "33443");
            ChangePasswordMassive("9534", "97649");
            ChangePasswordMassive("3286", "54867");
            ChangePasswordMassive("3490", "47689");
            ChangePasswordMassive("3523", "13570");
            ChangePasswordMassive("3529", "47838");
            ChangePasswordMassive("3654", "00716");
            ChangePasswordMassive("3698", "63490");
            ChangePasswordMassive("4118", "81960");
            ChangePasswordMassive("4381", "90384");
            ChangePasswordMassive("5632", "99177");
            ChangePasswordMassive("9682", "44979");
            ChangePasswordMassive("287", "21937");
            ChangePasswordMassive("843", "51913");
            ChangePasswordMassive("926", "59567");
            ChangePasswordMassive("1094", "04729");
            ChangePasswordMassive("1913", "91770");
            ChangePasswordMassive("3241", "51185");
            ChangePasswordMassive("3322", "25793");
            ChangePasswordMassive("3415", "53210");
            ChangePasswordMassive("3416", "53988");
            ChangePasswordMassive("3557", "11977");
            ChangePasswordMassive("3564", "34763");
            ChangePasswordMassive("3644", "97285");
            ChangePasswordMassive("3645", "49210");
            ChangePasswordMassive("3682", "07047");
            ChangePasswordMassive("3697", "55142");
            ChangePasswordMassive("5998", "08062");
            ChangePasswordMassive("8368", "67788");
            ChangePasswordMassive("8682", "54157");
            ChangePasswordMassive("9043", "71681");
            ChangePasswordMassive("9255", "80001");
            ChangePasswordMassive("489", "20120");
            ChangePasswordMassive("3173", "97731");
            ChangePasswordMassive("3205", "34426");
            ChangePasswordMassive("4262", "66575");
            ChangePasswordMassive("213", "31199");
            ChangePasswordMassive("302", "81121");
            ChangePasswordMassive("3119", "69531");
            ChangePasswordMassive("3679", "44636");
            ChangePasswordMassive("3683", "08514");
            ChangePasswordMassive("9407", "72893");
            ChangePasswordMassive("9621", "85381");
            ChangePasswordMassive("9879", "30362");
            ChangePasswordMassive("9904", "08227");
            ChangePasswordMassive("39", "32381");
            ChangePasswordMassive("470", "64623");
            ChangePasswordMassive("750", "78104");
            ChangePasswordMassive("826", "73603");
            ChangePasswordMassive("3541", "99470");
            ChangePasswordMassive("4888", "27357");
            ChangePasswordMassive("3057", "86150");
            ChangePasswordMassive("3059", "08277");
            ChangePasswordMassive("3652", "03061");
            ChangePasswordMassive("9620", "78145");
            ChangePasswordMassive("3623", "22307");
            ChangePasswordMassive("3575", "37168");
            ChangePasswordMassive("3707", "71721");
            ChangePasswordMassive("1338", "80284");
            ChangePasswordMassive("3524", "88880");
            ChangePasswordMassive("480", "03852");
            ChangePasswordMassive("1004", "04244");
            ChangePasswordMassive("1235", "81895");
            ChangePasswordMassive("3325", "67767");
            ChangePasswordMassive("3331", "32940");
            ChangePasswordMassive("3587", "92746");
            ChangePasswordMassive("3601", "80503");
            ChangePasswordMassive("3615", "89759");
            ChangePasswordMassive("415", "07045");
            ChangePasswordMassive("3602", "27715");
            ChangePasswordMassive("96", "03851");
            ChangePasswordMassive("3305", "37034");
            ChangePasswordMassive("3310", "96615");
            ChangePasswordMassive("9365", "32144");
            ChangePasswordMassive("9652", "21975");
            ChangePasswordMassive("1000", "65236");
            ChangePasswordMassive("4310", "35393");
            ChangePasswordMassive("9403", "00905");
            ChangePasswordMassive("9645", "23623");
            ChangePasswordMassive("3586", "03379");
            ChangePasswordMassive("9079", "64888");
            ChangePasswordMassive("61", "10596");
            ChangePasswordMassive("1502", "29345");
            ChangePasswordMassive("2407", "46472");
            ChangePasswordMassive("3232", "06429");
            ChangePasswordMassive("3235", "14906");
            ChangePasswordMassive("3401", "32843");
            ChangePasswordMassive("3535", "51694");
            ChangePasswordMassive("9897", "13775");
            ChangePasswordMassive("343", "57688");
            ChangePasswordMassive("419", "35041");
            ChangePasswordMassive("532", "80389");
            ChangePasswordMassive("535", "40414");
            ChangePasswordMassive("579", "80673");
            ChangePasswordMassive("686", "97803");
            ChangePasswordMassive("932", "72882");
            ChangePasswordMassive("934", "67526");
            ChangePasswordMassive("2010", "56879");
            ChangePasswordMassive("3214", "16347");
            ChangePasswordMassive("3217", "48527");
            ChangePasswordMassive("3303", "42714");
            ChangePasswordMassive("3307", "97679");
            ChangePasswordMassive("3324", "24399");
            ChangePasswordMassive("3451", "65145");
            ChangePasswordMassive("3452", "73111");
            ChangePasswordMassive("3549", "62982");
            ChangePasswordMassive("3551", "41138");
            ChangePasswordMassive("3552", "93588");
            ChangePasswordMassive("3630", "61617");
            ChangePasswordMassive("3647", "35619");
            ChangePasswordMassive("3648", "06860");
            ChangePasswordMassive("3649", "01884");
            ChangePasswordMassive("3662", "07752");
            ChangePasswordMassive("3693", "97483");
            ChangePasswordMassive("3695", "36254");
            ChangePasswordMassive("9066", "61583");
            ChangePasswordMassive("9149", "34013");
            ChangePasswordMassive("534", "22368");
            ChangePasswordMassive("576", "71140");
            ChangePasswordMassive("577", "90841");
            ChangePasswordMassive("687", "22197");
            ChangePasswordMassive("716", "93333");
            ChangePasswordMassive("3209", "32891");
            ChangePasswordMassive("3332", "09086");
            ChangePasswordMassive("3447", "01136");
            ChangePasswordMassive("3448", "67221");
            ChangePasswordMassive("3515", "19685");
            ChangePasswordMassive("3566", "63000");
            ChangePasswordMassive("3567", "80702");
            ChangePasswordMassive("3568", "72386");
            ChangePasswordMassive("3646", "76698");
            ChangePasswordMassive("3692", "60373");
            ChangePasswordMassive("3696", "79949");
            ChangePasswordMassive("4882", "51392");
            ChangePasswordMassive("9429", "22672");
            ChangePasswordMassive("813", "55038");
            ChangePasswordMassive("2692", "13938");
            ChangePasswordMassive("3095", "58061");
            ChangePasswordMassive("3302", "51576");
            ChangePasswordMassive("3484", "63335");
            ChangePasswordMassive("3562", "87790");
            ChangePasswordMassive("221", "73554");
            ChangePasswordMassive("794", "49343");
            ChangePasswordMassive("875", "10456");
            ChangePasswordMassive("3079", "55922");
            ChangePasswordMassive("3137", "93353");
            ChangePasswordMassive("3139", "17625");
            ChangePasswordMassive("3297", "17627");
            ChangePasswordMassive("3298", "24755");
            ChangePasswordMassive("3347", "63138");
            ChangePasswordMassive("3406", "55252");
            ChangePasswordMassive("3435", "31589");
            ChangePasswordMassive("3471", "05804");
            ChangePasswordMassive("3472", "38002");
            ChangePasswordMassive("3480", "90851");
            ChangePasswordMassive("3514", "49831");
            ChangePasswordMassive("3561", "28501");
            ChangePasswordMassive("3604", "89701");
            ChangePasswordMassive("3685", "84176");
            ChangePasswordMassive("3686", "39429");
            ChangePasswordMassive("3687", "00534");
            ChangePasswordMassive("3688", "31732");
            ChangePasswordMassive("3689", "22830");
            ChangePasswordMassive("3690", "57459");
            ChangePasswordMassive("3691", "25859");
            ChangePasswordMassive("9808", "81012");
            ChangePasswordMassive("41", "20728");
            ChangePasswordMassive("209", "84914");
            ChangePasswordMassive("289", "28509");
            ChangePasswordMassive("359", "19705");
            ChangePasswordMassive("360", "86655");
            ChangePasswordMassive("469", "65644");
            ChangePasswordMassive("637", "52734");
            ChangePasswordMassive("724", "21497");
            ChangePasswordMassive("970", "24633");
            ChangePasswordMassive("1031", "88816");
            ChangePasswordMassive("3074", "20439");
            ChangePasswordMassive("3075", "72101");
            ChangePasswordMassive("3120", "14862");
            ChangePasswordMassive("3124", "76946");
            ChangePasswordMassive("3146", "62217");
            ChangePasswordMassive("3239", "69239");
            ChangePasswordMassive("3240", "45259");
            ChangePasswordMassive("3255", "93911");
            ChangePasswordMassive("3344", "89040");
            ChangePasswordMassive("3595", "68554");
            ChangePasswordMassive("3596", "30853");
            ChangePasswordMassive("3598", "63666");
            ChangePasswordMassive("3599", "67242");
            ChangePasswordMassive("3608", "79276");
            ChangePasswordMassive("3633", "96651");
            ChangePasswordMassive("3668", "34869");
            ChangePasswordMassive("3669", "63602");
            ChangePasswordMassive("3702", "60414");
            ChangePasswordMassive("3708", "49503");
            ChangePasswordMassive("3709", "30200");
            ChangePasswordMassive("3712", "11574");
            ChangePasswordMassive("4369", "16192");
            ChangePasswordMassive("5244", "89218");
            ChangePasswordMassive("8649", "16515");
            ChangePasswordMassive("9031", "59346");
            ChangePasswordMassive("9267", "22117");
            ChangePasswordMassive("9423", "17106");
            ChangePasswordMassive("9454", "00385");
            ChangePasswordMassive("9529", "74349");
            ChangePasswordMassive("9541", "67622");
            ChangePasswordMassive("9810", "66007");


            return Content("Process completed");
        }

        private void ChangePasswordMassive(string employeeId, string newPassword)
        {
            try
            {
                using (var db = new VolanteNominaEntities())
                {
                    string password = AjaxController.SHA256(newPassword);

                    var currentUser = db.Users.FirstOrDefault(u => u.EmployeeID == employeeId);

                    if (currentUser != null)
                    {
                        currentUser.PasswordHash = AjaxController.SHA256(newPassword);
                        db.SaveChanges();
                    }
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
            }
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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

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
                    AjaxController.SendRawEmail("rafaelmersant@sagaracorp.com", "Exception in RecoverPassword: " + employeeId, ex.ToString());
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