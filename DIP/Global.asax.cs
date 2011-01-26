using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using Microsoft.Practices.Unity;
using BoxInformation.Presenter;
using BoxInformation.Interfaces;
using BoxInformation.Model;


namespace BoxInformation
{
    public class Global : System.Web.HttpApplication
    {
        private const string AppContainerKey = "application container";
        private const string SessionContainerKey = "session container";

        protected void Application_Start(object sender, EventArgs e)
        {
            IUnityContainer applicationContainer = new UnityContainer();

            ConfigureContainer(applicationContainer, "application");
                

            ApplicationContainer = applicationContainer;
        }

        protected void Application_End(object sender, EventArgs e)
        {
            IUnityContainer applicationContainer = this.ApplicationContainer;

            if (applicationContainer != null)
            {
                applicationContainer.Dispose();

                this.ApplicationContainer = null;
            }
        }

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            Page handler = HttpContext.Current.Handler as Page;

            if (handler != null)
            {
                IUnityContainer container = SessionContainer;

                if (container != null)
                {
                    container.BuildUp(handler.GetType(), handler);
                }
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            IUnityContainer applicationContainer = this.ApplicationContainer;

            if (applicationContainer != null)
            {
                IUnityContainer sessionContainer
                    = applicationContainer.CreateChildContainer();
                ConfigureContainer(sessionContainer, "session");

                this.SessionContainer = sessionContainer;
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {
            IUnityContainer sessionContainer = this.SessionContainer;
            if (sessionContainer != null)
            {
                sessionContainer.Dispose();

                this.SessionContainer = null;
            }
        }

        private IUnityContainer ApplicationContainer
        {
            get
            {
                return (IUnityContainer)this.Application[AppContainerKey];
            }
            set
            {
                this.Application[AppContainerKey] = value;
            }
        }

        private IUnityContainer SessionContainer
        {
            get
            {
                return (IUnityContainer)this.Session[SessionContainerKey];
            }
            set
            {
                this.Session[SessionContainerKey] = value;
            }
        }

        private static void ConfigureContainer(
            IUnityContainer container,string containerName)
        {
            if (containerName == "application")
            {
                container.RegisterType<IDataAccess, SqlDataAccess>(
                    new InjectionConstructor(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
                    .RegisterType<SearchPresenter, SearchPresenter>("Search")
                    .RegisterType<ISearchView, Search>()
                    .RegisterType<IBoxEntry, BoxEntry>()
                    .RegisterType<RecordPresenter, RecordPresenter>()
                    .RegisterType<IRecordView, ViewRecord>();

            }

        }
    }
}