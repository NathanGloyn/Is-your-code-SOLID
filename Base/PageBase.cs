using System;
using System.Collections.Generic;
using System.Text;
using System.Web.SessionState;
using System.Web.UI;
using System.Configuration;
using System.Web.Security;
using System.Web;
using System.Threading;
using System.Globalization;

namespace BoxDetails.Helper
{
    public class PageBase : Page
        {
        private string _errorcode;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePage"/> class.
        /// </summary>
        public PageBase()
        {
            this.PreInit += new EventHandler(BasePage_PreInit);
        }

        /// <summary>
        /// Handles the PreInit event of the BasePage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void BasePage_PreInit(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <value>The session.</value>
        public HttpSessionState session
        {
            get { return Session; }
        }

        /// <summary>
        /// Gets a value indicating whether the page is being requested for the first time.
        /// </summary>
        /// <value><c>true</c> if isinit; otherwise, <c>false</c>.</value>
        public bool isinit
        {
            get { return (!Page.IsPostBack && !Page.IsCallback); }
        }

        /// <summary>
        /// Gets the requesturl.
        /// </summary>
        /// <value>The requesturl.</value>
        public string requesturl
        {
            get { return Request.Url.OriginalString; }
        }

        /// <summary>
        /// Gets the applicationpath.
        /// </summary>
        /// <value>The applicationpath.</value>
        public string applicationpath
        {
            get { return Request.AppRelativeCurrentExecutionFilePath; }
        }

        /// <summary>
        /// Gets the physicalpath.
        /// </summary>
        /// <value>The physicalpath.</value>
        public string physicalpath
        {
            get { return Request.PhysicalApplicationPath; }
        }

        /// <summary>
        /// Sets the error message for the page and displays it to the user.
        /// </summary>
        /// <value>The error.</value>
        public string error
        {
            set
            {
                value = value.Replace("'", "\\'");
                value = value.Replace("\r\n", "\\n");
                String scriptString = "try{window.alert('" + value + ", if the error presists please contact your System Administrator.');}catch(e){window.alert('" + genericError + ", if the error presists please contact your System Administrator.');}";

                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;

                // Check to see if the startup script is already registered.
                if (!cs.IsStartupScriptRegistered(GetType(), "Error"))
                {
                    cs.RegisterStartupScript(GetType(), "Error", scriptString, true);
                }
            }
        }

        /// <summary>
        /// Gets the generic error message.
        /// </summary>
        /// <value>The generic error.</value>
        public string genericError
        {
            get { return "An Unexpected error occurred [" + errorCode + "] "; }
        }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public string errorCode
        {
            get { return _errorcode; }
            set
            {
                try
                {
                    _errorcode = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() +
                                 DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() +
                                 DateTime.Now.Minute.ToString() + value.Remove(4);
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// Generates javascript code block from code provided.
        /// </summary>
        /// <value>The generate javascript.</value>
        public string generateJavascript
        {
            set
            {
                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;

                // Check to see if the startup script is already registered.
                if (!cs.IsStartupScriptRegistered(GetType(), "Script"))
                {
                    cs.RegisterStartupScript(GetType(), "Script", "Javascript:" + value, true);
                }
            }
        }

        /// <summary>
        /// Displays message in a MSG box.
        /// </summary>
        /// <value>The display MSG box.</value>
        public string displayMsgBox
        {
            set
            {
                value = value.Replace("'", "\\'");
                value = value.Replace("\r\n", "\\n");
                String scriptString = "try{window.alert('" + value + "');}catch(e){window.alert('" + genericError + ", if the error presists please contact your System Administrator.');}";

                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;

                // Check to see if the startup script is already registered.
                if (!cs.IsStartupScriptRegistered(GetType(), "Error"))
                {
                    cs.RegisterStartupScript(GetType(), "Error", scriptString, true);
                }
            }
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public virtual void close()
        {
            Response.Redirect(ConfigurationManager.AppSettings["homeurl"].ToString());
        }

        /// <summary>
        /// Sets a value indicating whether this is an invalidrequest.
        /// </summary>
        /// <value><c>true</c> if invalidrequest; otherwise, <c>false</c>.</value>
        public virtual bool invalidrequest
        {
            set
            {
                if (value)
                {
                    close();
                }
            }
        }

        /// <summary>
        /// Gets the username of the authenticated user.
        /// </summary>
        /// <value>The username.</value>
        public string username
        {
            get
            {
                try
                {
                    return this.User.Identity.Name;
                }
                catch (IndexOutOfRangeException)
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// Sets the securitycookie.
        /// </summary>
        /// <value>The securitycookie.</value>
        public System.Web.HttpCookie securitycookie
        {
            set { Response.Cookies.Add(value); }
        }

        /// <summary>
        /// Gets the ticket of the authenticated user.
        /// </summary>
        /// <value>The ticket.</value>
        public System.Web.Security.FormsAuthenticationTicket ticket
        {
            get { return ((FormsIdentity)HttpContext.Current.User.Identity).Ticket; }
        }

        /// <summary>
        /// Sets the redirect page to enable the site to navigate away from the current page.
        /// </summary>
        /// <value>The redirect url.</value>
        public string redirect
        {
            set { Response.Redirect(value); }
        }  
    }
}
