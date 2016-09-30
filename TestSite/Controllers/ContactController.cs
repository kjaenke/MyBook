using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.WebPages;
using Hangfire;
using TestSite.Infrastructure;
using TestSite.Models;
using TestSite.Persistence;
using TestSite.Scripts;

namespace TestSite.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
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
            IEnumerable<Contact> contactList = unit.Contacts.GetAll();
            Session["Contacts"] = contactList;
            return View();
        }

        public ActionResult Funktion(int id, string funktion)
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User)Session["User"];
            }
            if (user != null && user.Mail == null)
            {
                return Redirect("/User/Login");
            }
            SystemMessage message = new SystemMessage();
            switch (funktion)
            {
                case "Melden":
                {
                    UnitOfWork unit = new UnitOfWork(new PlutoContext());
                    BackgroundJob.Enqueue(() => message.NewAction("KONTAKT GEMELDET",
                        "Der Kontakt mit der ID: " + id +
                        ", wurde soeben gemeldet. Nach Bearbeitung muss diese Mail gelöscht werden." + DateTime.Now.ToString()));

                    unit.Contacts.ChangeStatus(id, 2);
                    return Redirect("/Contact/Index");
                }
                case "Sperren":
                {
                    if (user.Roles != "Admin" && user.Roles != "Support") return Redirect("/Contact/Index");
                    UnitOfWork unit = new UnitOfWork(new PlutoContext());
                    unit.Contacts.ChangeStatus(id, 1);
                    return Redirect("/Contact/Index");
                }
                case "Entsperren":
                {
                    if (user.Roles != "Admin") return Redirect("/Contact/Index");
                    UnitOfWork unit = new UnitOfWork(new PlutoContext());
                    unit.Contacts.ChangeStatus(id, 0);
                    unit.Complete();
                    return Redirect("/Contact/Index");
                }
                case "Löschen":
                {
                    if (user.Roles != "Admin") return Redirect("/Contact/Index");
                    UnitOfWork unit = new UnitOfWork(new PlutoContext());
                    unit.Contacts.ChangeStatus(id, 3);
                    return Redirect("/Contact/Index");
                }
                case "Reaktivieren":
                {
                    if (user.Roles != "Admin") return Redirect("/Contact/Index");
                    UnitOfWork unit = new UnitOfWork(new PlutoContext());
                    unit.Contacts.ChangeStatus(id, 0);
                    return Redirect("/Contact/Index");
                }
            }
            if (funktion != "Bearbeiten") return Redirect("/Contact/Index");
            {
                UnitOfWork unit = new UnitOfWork(new PlutoContext());
                Contact contact = unit.Contacts.Get(id);
                if (contact.CreateById == user.Id || user.Roles == "Admin" || user.Roles == "Support")
                {
                    return Redirect("/Contact/EditContact?Id=" + id);
                }
            }
            return Redirect("/Contact/Index");
        }

        public ActionResult EditContact(string id)
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
            if (id == null)
            {
                return Redirect("/Contact/Index");
            }
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            Contact contact = unit.Contacts.Get(id.AsInt());
            if (contact.CreateById != user.Id)
            {
                return Redirect("/User/Login");
            }
            ViewData["EditContact"] = contact;
            return View();
        }

        public ActionResult SaveContact(Contact contact)
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User)Session["User"];
            }
            if (user != null && user.Mail == null)
            {
                return Redirect("/User/Login");
            }
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            unit.Contacts.Add(contact);
            unit.Complete();
            return Redirect("/Contact/Index");
        }

        public ActionResult Archiv()
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User) Session["User"];
            }
            if (user != null && user.Mail == null)
            {
                return Redirect("/User/Login");
            }
            if (user != null && user.Roles == "User")
            {
                return Redirect("/Contact/Index");
            }

            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            IEnumerable<Contact> contactList = unit.Contacts.GetAll();
            Session["ContactsArchiv"] = contactList;

            return View("Archiv");
        }

        public ActionResult NewContact()
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User)Session["User"];
            }
            if (user != null && user.Mail == null)
            {
                return Redirect("/User/Login");
            }
            return View();
        }

        public ActionResult SaveNewContact(Contact contact)
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User)Session["User"];
            }
            if (user != null && user.Mail == null)
            {
                return Redirect("/User/Login");
            }
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            contact.Status = 0;
            unit.Contacts.Add(contact);
            unit.Complete();
            return Redirect("/Contact/Index");
        }
    }
}