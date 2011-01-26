using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BoxInformation.Interfaces;

namespace BoxInformation
{
    public partial class Search : Page,  IView
    {
        Presenter.BoxDetailsPresenter _mBoxDetailsPresenter;
        DataSet _searchResults;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["BoxInformation"] = null;
            if (_mBoxDetailsPresenter == null) _mBoxDetailsPresenter = new Presenter.BoxDetailsPresenter(this);
        }

        /// <summary>
        /// Handles the Click event of the Search control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void Search_Click(object sender, EventArgs e)
        {
            _mBoxDetailsPresenter.GetSearchResults();
            dtlSearchResults.DataSource = _searchResults;
            dtlSearchResults.DataBind();
        }

        /// <summary>
        /// Gets or sets the search results.
        /// </summary>
        /// <value>The search results.</value>
        public DataSet searchResults
        {
            get { return _searchResults; }
            set { _searchResults = value; }
        }

        /// <summary>
        /// Gets the Client number.
        /// </summary>
        /// <value>The Client number.</value>
        public string ClientNumber
        {
            get
            {
                if (txtClientNumber.Text.Length > 0)
                {
                    return txtClientNumber.Text;
                }
                else
                {
                    return null;
                }
            }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the name of the Client.
        /// </summary>
        /// <value>The name of the Client.</value>
        public string ClientName
        {
            get
            {
                if (txtClientName.Text.Length > 0)
                {
                    return txtClientName.Text;
                }
                else
                {
                    return null;
                }
            }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the Client principal.
        /// </summary>
        /// <value>The Client principal.</value>
        public string ClientPrincipal
        {
            get
            {
                if (txtClientContact.Text.Length > 0)
                {
                    return txtClientContact.Text;
                }
                else
                {
                    return null;
                }
            }
            set { throw new NotImplementedException(); }
        }


        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>The location.</value>
        public string location
        {
            get
            {
                if (ddlLocation.SelectedValue != "0")
                {
                    return ddlLocation.SelectedValue;
                }
                else
                {
                    return null;
                }
            }

        }

        /// <summary>
        /// Gets the completion date.
        /// </summary>
        /// <value>The completion date.</value>
        public DateTime? reviewDate
        {
            get
            {
                if (txtReviewDate.Text.Length > 0)
                {
                    DateTime? _date = new DateTime?();
                    DateTime _date2;

                    if (DateTime.TryParse(txtReviewDate.Text, out _date2))
                    {
                        _date = new DateTime?(_date2);
                    }
                    else
                    {
                        return null;
                    }
                    return _date;
                }
                else
                {
                    return null;
                }
                    

            }
            set { throw new NotImplementedException(); }
        }

        DateTime? IView.reviewDate { get; set; }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public string comments
        {
            get {
                if (txtComments.Text.Length > 0)
                {
                    return txtComments.Text;
                }
                else
                {
                    return null;
                }
            }
            set { throw new NotImplementedException(); }
        }

        public HttpPostedFile file
        {
            get { throw new NotImplementedException(); }
        }

        public HttpPostedFile file2
        {
            get { throw new NotImplementedException(); }
        }

        public string fileName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string fileName2
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Id
        {
            get { throw new NotImplementedException(); }
        }

        public bool SecureStorage
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public List<KeyValuePair<string, int>> boxDetails
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void AddRecord_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
