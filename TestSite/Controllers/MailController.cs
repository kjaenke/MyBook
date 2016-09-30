using System;
using System.Globalization;
using System.Web.Mvc;
using System.Web.WebPages;
using TestSite.Infrastructure;
using TestSite.Models;
using TestSite.Persistence;

namespace TestSite.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User) Session["User"];
            }
            if (user.Mail == null)
            {
                return Redirect("/User/Login");
            }
            return View();
        }

        public ActionResult NewMail()
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User)Session["User"];
            }
            if (user.Mail == null)
            {
                return Redirect("/User/Login");
            }
            return View();
        }

        public ActionResult SaveMail(Mail mail)
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User)Session["User"];
            }
            if (user.Mail == null)
            {
                return Redirect("/User/Login");
            }
            mail.Date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            mail.Delete = "false";
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            try
            {
                user = unit.Users.FindMail(mail.ToId);
                mail.ToId = user.Id.ToString();
            }
            catch (Exception)
            {
                // ignored
            }
            unit.Mails.Add(mail);
            unit.Complete();
            return Redirect("Index");
        }

        public ActionResult ReadMail(string id)
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User)Session["User"];
            }
            if (user.Mail == null)
            {
                return Redirect("/User/Login");
            }
            if (id == null)
            {
                id = (string) Session["ThisMailId"];
                Session["ThisMailId"] = null;
            }
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            Mail mail = unit.Mails.Get(id.AsInt());
            mail.IsRead = "true";
            unit.Complete();
            unit.Mails.Add(mail);
            unit.Complete();
            Session["actuallMail"] = mail;
            return View();
        }

        public ActionResult SaveAnswerMail(Mail mail)
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User)Session["User"];
            }
            if (user.Mail == null)
            {
                return Redirect("/User/Login");
            }
            mail.Delete = "false";
            mail.Date = DateTime.Now.ToString(CultureInfo.InvariantCulture);

            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            unit.Mails.Add(mail);
            unit.Complete();

            Session["ThisMailId"] = mail.TopId;
            return Redirect("/Mail/ReadMail");
        }

        public ActionResult DeleteMail(string id)
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User)Session["User"];
            }
            if (user.Mail == null)
            {
                return Redirect("/User/Login");
            }
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            Mail mail = unit.Mails.Get(id.AsInt());

            mail.Delete = "true";
            unit.Mails.Add(mail);
            unit.Complete();

            return Redirect("/Mail/Index");
        }

        public ActionResult Intern()
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User)Session["User"];
            }
            if (user.Mail == null)
            {
                return Redirect("/User/Login");
            }
            if (user.Roles == "Admin" || user.Roles == "Support")
            {
                return View();
            }
            return Redirect("/Mail/Index");
        }

        public ActionResult Ticket()
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User) Session["User"];
            }
            if (user.Mail == null)
            {
                return Redirect("/Forum/Index");
            }
            return View();
        }
    }
}