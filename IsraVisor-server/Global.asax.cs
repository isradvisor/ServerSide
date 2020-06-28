using IsraVisor_server.Controllers;
using IsraVisor_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IsraVisor_server
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            // Dynamically create new timer
            System.Timers.Timer timScheduledTask = new System.Timers.Timer();
            System.Timers.Timer timer24Hours = new System.Timers.Timer();
            // Timer interval is set in miliseconds,
            // In this case, we'll run a task every minute
            timScheduledTask.Interval = 5 * 1000;
            timer24Hours.Interval = 86400000;
            timer24Hours.Enabled = true;
            timScheduledTask.Enabled = true;
            timer24Hours.Elapsed += new System.Timers.ElapsedEventHandler(timer24Hours_Elpsed);
            // Add handler for Elapsed event
            timScheduledTask.Elapsed +=
            new System.Timers.ElapsedEventHandler(timScheduledTask_Elapsed);
        }
        void timer24Hours_Elpsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //TaskPer24Hours();
        }
        // Code that runs on application startup
        //void Application_Start(object sender, EventArgs e)
        //{
        //}

        void timScheduledTask_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Execute some task
            //FirstTask();
        }

        void FirstTask()
        {
            // Here is the code we need to execute periodically
            DBservices db = new DBservices();
            List<Guide_Tourist> listTokens = db.GetTokensOfUsersInChat();
            for (int i = 0; i < listTokens.Count; i++)
            {
                if (listTokens[i].Token != null)
                {
                    sendMessage(listTokens[i]);
                }
            }
        }
       void sendMessage(Guide_Tourist gt)
        {
            TouristController c = new TouristController();
            PushNotData p = new PushNotData();
            p.to = gt.Token;
            p.body = gt.GuideEmail + "As Accept your request";
            p.title = "Accept Request";
            c.PostNotif(p);
        }


    }
}
