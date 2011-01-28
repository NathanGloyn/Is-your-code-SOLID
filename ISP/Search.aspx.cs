using System;
using System.Data;
using System.Web.UI;
using BoxInformation.Interfaces;
using BoxInformation.Presenter;

namespace BoxInformation
{
    public partial class Search : Page, ISearchView
    {
        SearchPresenter presenter;
        DataSet results;

        protected void Page_Load(object sender, EventArgs e)
        {
            presenter = new SearchPresenter(this);
        }

        public void Search_Click(object sender, EventArgs e)
        {
            presenter.GetSearchResults();
            dtlSearchResults.DataSource = results;
            dtlSearchResults.DataBind();
        }

        public DataSet searchResults
        {
            get { return results; }
            set { results = value; }
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
        }
    }
}
