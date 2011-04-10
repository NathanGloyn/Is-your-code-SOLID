using System;
using System.Web;
using System.Web.UI;
using Microsoft.Practices.Unity;
using BoxInformation.UnityConfiguration;

namespace BoxInformation
{
    public class Global : System.Web.HttpApplication
    {
        private const string AppContainerKey = "application container";

        private const string SessionContainerKey = "session container";

        private IUnityContainer ApplicationContainer
        {
            get
            {
                return (IUnityContainer)Application[AppContainerKey];
            }
            set
            {
                Application[AppContainerKey] = value;
            }
        }

        private IUnityContainer SessionContainer
        {
            get
            {
                return (IUnityContainer)Session[SessionContainerKey];
            }
            set
            {
                Session[SessionContainerKey] = value;
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            IUnityContainer applicationContainer = new UnityContainer();
            ConfigureContainer.Application(applicationContainer);

            ApplicationContainer = applicationContainer;
        }

        protected void Application_End(object sender, EventArgs e)
        {
            IUnityContainer applicationContainer = ApplicationContainer;

            if (applicationContainer != null)
            {
                applicationContainer.Dispose();

                ApplicationContainer = null;
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
            IUnityContainer applicationContainer = ApplicationContainer;

            if (applicationContainer != null)
            {
                IUnityContainer sessionContainer
                    = applicationContainer.CreateChildContainer();
                ConfigureContainer.Session(sessionContainer);

                SessionContainer = sessionContainer;
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {
            IUnityContainer sessionContainer = SessionContainer;
            if (sessionContainer != null)
            {
                sessionContainer.Dispose();

                SessionContainer = null;

            }
        }

    }
}