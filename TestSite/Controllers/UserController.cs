using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.WebPages;
using TestSite.Infrastructure;
using TestSite.Models;
using TestSite.Persistence;

namespace TestSite.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index(string form = "")
        {
            ViewData["RegisterTest"] = form;
            return View();
        }

        //GET: User/Welcome

        public ActionResult Welcome(User user)
        {
            string form = string.Empty;
            if (user.Firstname == null)
            {
                form += "F";
            }
            if (user.Lastname == null)
            {
                form += "L";
            }
            if (user.Mail == null)
            {
                form += "M";
            }
            if (user.Lastname == null)
            {
                form += "P";
            }
            if (form != string.Empty)
            {
                return Redirect("/User/Index?form="+form);
            }
            byte[] data = Encoding.ASCII.GetBytes(user.Password);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] md5Data = md5.ComputeHash(data);
            user.Password = Encoding.ASCII.GetString(md5Data);
            user.Roles = "User";
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            User result = unit.Users.FindMail(user.Mail);
            if (result == null)
            {
                Session["User"] = user;
                unit.Users.Add(user);
                unit.Complete();
            }
            else
            {
                return View("ERROR");
            }

            return View(user);
        }


        //GET: User/Login
        public ActionResult Login(bool loginTest = false)
        {

            if (loginTest)
            {
                ViewData["LoginTest"] = true;
            }
            else
            {
                ViewData["LoginTest"] = false;
            }
            
            return View();
        }

        //GET: User/Loginfail
        public ActionResult Loginfail(string mail, string password)
        {
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            byte[] data = Encoding.ASCII.GetBytes(password);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] md5Data = md5.ComputeHash(data);
            password = Encoding.ASCII.GetString(md5Data);
            User result = unit.Users.GetUser(mail, password);
            if (result == null)
            {
                
                return Redirect("/User/Login?loginTest=true");
            }
            Session["User"] = result;
            return Redirect("/Home/Index");
        }

        public ActionResult RoleManager()
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User) Session["User"];
            }
            if (user.Roles == "Admin")
            {
                return View();
            }
            return Redirect("/User/Welcome");
        }

        public ActionResult LogOut()
        {
            Session["User"] = null;
            return Redirect("/User/Login");
        }

        public ActionResult Profiles(string id, string act = "0")
        {
            if (act != "0")
            {
                User user = new User();
                if (Session["User"] != null)
                {
                    user = (User)Session["User"];
                }
                if (user.Mail != null)
                {
                    ViewData["UserId"] = user.Id;
                    ViewData["UserListOption"] = act;
                    return View();
                }
                else
                {
                    return View("UserList");
                }
            }
            else
            {
                User user = new User();
                if (Session["User"] != null)
                {
                    user = (User) Session["User"];
                }
                if (id == null)
                {
                    if (user.Id != null)
                    {
                        ViewData["UserId"] = user.Id;
                        return View();
                    }
                    else
                    {
                        return View("UserList");
                    }
                }
                UnitOfWork unit = new UnitOfWork(new PlutoContext());

                if (unit.Users.Get(id.AsInt()) == null)
                {
                    return View("UserList");
                }
                ViewData["UserId"] = id.AsInt();
                return View();
            }
        }

        public ActionResult ChangePassword(string id, string passwordNew, string passwordRepeat)
        {

            if (passwordNew != passwordRepeat)
            {
                return Redirect("/User/Profiles?id="+id+"&act=1.5");
            }
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            User user = unit.Users.Get(id.AsInt());
            byte[] data = Encoding.ASCII.GetBytes(passwordNew);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] md5Data = md5.ComputeHash(data);
            user.Password = Encoding.ASCII.GetString(md5Data);
            unit.Users.Add(user);
            unit.Complete();
            return Redirect("/User/Profiles?id="+id+"&act=1.7");
        }

        public ActionResult DeleteUser(string mail)
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User) Session["User"];

            }
            if (user.Mail == mail || user.Roles == "Admin")
            {
                UnitOfWork unit = new UnitOfWork(new PlutoContext());
                User result = unit.Users.FindMail(mail);
                result.Mail = "-----------";
                result.Phonenumber = "";
                result.Firstname = "Gelöschter";
                result.Lastname = "Nutzer";
                result.Salutation = "------";
                result.Password = "jfjaejkakfnaejfkaefluahzlenafewe";
                unit.Users.Add(result);
                unit.Complete();
                if (user.Mail == mail)
                {
                    return Redirect("/User/LogOut");
                }
            }
            return Redirect("/Forum/Index");
        }

        public ActionResult UserList()
        {
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            IEnumerable<User> user = unit.Users.GetAll();
            unit.Complete();
            Session["UserList"] = user;

            return View();
        }

        public ActionResult UserRank(string id, string funk)
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User)Session["User"];
            }
            if (user.Roles == "Admin")
            {
                UnitOfWork unit = new UnitOfWork(new PlutoContext());
                unit.Users.UserRank(id, funk);
                unit.Complete();

                return Redirect("UserList");
            }
            return Redirect("UserList");

        }
    }
}