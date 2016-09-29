using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using TestSite.Infrastructure;
using TestSite.Models;
using TestSite.Persistence;

namespace TestSite.Scripts
{
    public class Counter
    {
        public int User()
        {
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            IEnumerable<User> user = unit.Users.GetAll();
            int counter = 0;
            foreach (User u in user)
            {
                counter++;
            }
            return counter;
        }


        public int Topic()
        {
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            IEnumerable<Topic> topic = unit.Topics.GetAll();
            int counter = 0;
            foreach (Topic t in topic)
            {
                if (t.Delete == "false")
                {
                    counter++;
                }
            }
            return counter;


        }

        public int Article()
        {
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            IEnumerable<Article> article = unit.Articles.GetAll();
            int counter = 0;

            foreach (Article a in article)
            {
                if (a.Delete == "false" || a.Delete == null)
                {
                    counter++;
                }
            }
            return counter;
        }
    }
}