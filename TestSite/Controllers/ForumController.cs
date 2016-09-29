using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using System.Web.WebPages;
using TestSite.Infrastructure;
using TestSite.Models;
using TestSite.Persistence;

namespace TestSite.Controllers
{
    public class ForumController : Controller
    {
        // GET: Forum
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowTopics(string forumId)
        {
            ViewData["ForumId"] = forumId;
            return View();
        }

        public ActionResult CreateForum()
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
            return Redirect("/Forum/Index");
        }

        public ActionResult SaveForum(Forum forum)
        {
            string time = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            forum.Erstelldatum = time;
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            forum.Delete = "false";
            unit.Forums.Add(forum);
            unit.Complete();
            return Redirect("/Forum/Index");
        }

        public ActionResult CreateTopic(string forumId)
        {
            if (forumId == null)
            {
                return Redirect("/Forum/Index");
            }
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User) Session["User"];
            }
            if (user.Mail == null)
            {
                return Redirect("/Forum/ShowTopics?ForumId=" + forumId);
            }
            ViewData["ForumId"] = forumId;
            return View();
        }

        public ActionResult SaveTopic(Topic topic)
        {
            UnitOfWork unit = new UnitOfWork(new PlutoContext());

            string timer = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            topic.CreateDate = timer;
            topic.Delete = "false";
            unit.Topics.Add(topic);
            unit.Complete();
            return Redirect("/Forum/ShowArticle?Id=" + topic.TopicId);
        }

        public ActionResult ShowArticle(string id)
        {
            if (id == null)
            {
                return Redirect("/Forum/Index");
            }
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            ViewData["Topic"] = unit.Topics.Get(id.AsInt());
            return View();
        }

        public ActionResult SaveArticle(Article article)
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User)Session["User"];
            }
            if (user != null && user.Mail == null)
            {
                return Redirect("/Forum/ShowArticle?Id="+article.TopicId);
            }
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            string time = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            article.CreateDate = time;
            unit.Articles.Add(article);
            unit.Complete();
            return Redirect("/Forum/ShowArticle?Id=" + article.TopicId);
        }

        public ActionResult EditArticle(string articleId)
        {
            if (articleId == null)
            {
                return Redirect("/Forum/Index");
            }
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            Article article = unit.Articles.Get(articleId.AsInt());
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User)Session["User"];
            }
            if (user != null && user.Mail == null)
            {
                return Redirect("/Forum/Index");
            }
            if (user != null && (user.Roles == "Admin" || user.Roles == "Support" || user.Id.ToString() == article.CreateId))
            {
                ViewData["EditArticle"] = article;
                return View();
            }
            return Redirect("/Forum/ShowArticle?Id=" + article.TopicId);
        }

        public ActionResult SaveEditArticle(Article article)
        {
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            article.EditCount += 1;
            article.EditDate = DateTime.Now.ToString(CultureInfo.InvariantCulture);

            Article article2 = unit.Articles.Get(article.ArticleId);
            article2.Content = article.Content;
            article2.EditCount = article.EditCount;
            article2.EditDate = article.EditDate;
            article2.LastEditId = article.LastEditId;


            unit.Articles.Add(article2);
            unit.Complete();
            return Redirect("/Forum/ShowArticle?Id=" + article2.TopicId);
        }

        public ActionResult DeleteArticle(string articleId)
        {
            UnitOfWork unit = new UnitOfWork(new PlutoContext());

            Article article = unit.Articles.Get(articleId.AsInt());

            User user = new User();
            if (Session["User"] != null)
            {
                user = (User) Session["User"];
            }
            if (user != null && user.Mail == null)
            {
                return Redirect("/Forum/Index");
            }
            if (user == null ||
                (article.CreateId != user.Id.ToString() && user.Roles != "Admin" && user.Roles != "Support"))
                return Redirect("/Forum/ShowArticle?Id=" + article.TopicId);
            article.Delete = "true";
            unit.Articles.Add(article);
            unit.Complete();
            return Redirect("/Forum/ShowArticle?Id=" + article.TopicId);
        }

        public ActionResult DeleteForum(string Id)
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User)Session["User"];
            }
            if (user.Roles != "Admin")
            {
                return Redirect("/Forum/Index");
            }
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            Forum forum = unit.Forums.Get(Id.AsInt());
            forum.Delete = "true";
            unit.Forums.Add(forum);
            unit.Complete();
            return Redirect("/Forum/Index");
        }

        public ActionResult EditForum(string id)
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User) Session["User"];
            }
            if (user.Roles != "Admin")
            {
                return Redirect("/Forum/Index");
            }

            ViewData["ThisForum"] = id;
            return View();
        }

        public ActionResult SaveEditForum(string id,string forumName,string comment)
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User)Session["User"];
            }
            if (user.Roles != "Admin")
            {
                return Redirect("/Forum/Index");
            }
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            Forum forum = unit.Forums.Get(id.AsInt());
            forum.Comment = comment;
            forum.ForumName = forumName;

            unit.Forums.Add(forum);
            unit.Complete();
            return Redirect("/Forum/Index");

        }

        public ActionResult DeleteTopic(string id)
        {
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            Topic topic = unit.Topics.Get(id.AsInt());
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User) Session["User"];
            }
            if (user.Roles == "Admin" || user.Roles == "Support" || user.Id == topic.CreateId.AsInt())
            {
                topic.Delete = "true";
                unit.Topics.Add(topic);
                unit.Complete();
            }
            return Redirect("/Forum/ShowTopics?forumId=" + topic.ForumId);

        }

        public ActionResult EditTopic(string id)
        {
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            Topic topic = unit.Topics.Get(id.AsInt());
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User) Session["User"];
            }
            if (user.Roles == "Admin" || user.Roles == "Support" || user.Id == topic.CreateId.AsInt())
            {
                ViewData["EditTopic"] = topic;
                return View();
            }
            return Redirect("/Forum/Index");

        }

        public ActionResult SaveEditTopic(Topic topic)
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User)Session["User"];
            }
            if (user.Roles == "Admin" || user.Roles == "Support" || user.Id == topic.CreateId.AsInt())
            {
                UnitOfWork unit = new UnitOfWork(new PlutoContext());
                Topic top = unit.Topics.Get(topic.TopicId);
                top.Name = topic.Name;
                top.Content = topic.Content;
                unit.Topics.Add(top);
                unit.Complete();
                return Redirect("/Forum/ShowArticle?id=" + top.TopicId);
            }
            return Redirect("/Forum/Index");

        }

        public ActionResult MoveTopic(string id, string where)
        {
            User user = new User();
            if (Session["User"] != null)
            {
                user = (User) Session["User"];
            }
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            IEnumerable<Forum> forums = unit.Forums.GetAll();
            Topic topic = unit.Topics.Get(id.AsInt());
            if (user.Roles == "Admin" || user.Roles == "Support")
            {
                

                foreach (Forum f in forums)
                {
                    if (f.ForumName == where)
                    {
                        
                        topic.ForumId = f.ForumId;
                        unit.Topics.Add(topic);
                        unit.Complete();
                        return Redirect("/Forum/ShowArticle?id="+topic.TopicId);
                    }
                }
            }
            return Redirect("/Forum/ShowArticle?id="+topic.TopicId);
        }

    }
}