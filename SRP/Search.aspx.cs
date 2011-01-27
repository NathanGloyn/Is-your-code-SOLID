using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using BoxInformation.Interfaces;
using BoxInformation.Presenter;

namespace BoxInformation
{
    public partial class Search : Page,  IView
    {
        BoxDetailsPresenter presenter;
        DataSet _searchResults;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["BoxInformation"] = null;
            if (presenter == null) presenter = new Presenter.BoxDetailsPresenter(this);
        }

        public void Search_Click(object sender, EventArgs e)
        {
            presenter.GetSearchResults();
            dtlSearchResults.DataSource = _searchResults;
            dtlSearchResults.DataBind();
        }

        public DataSet searchResults
        {
            get { return _searchResults; }
            set { _searchResults = value; }
        }

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
