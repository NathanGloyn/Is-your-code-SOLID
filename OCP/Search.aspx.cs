using System;
using System.Data;
using System.Web.UI;
using BoxInformation.Interfaces;
using BoxInformation.Presenter;
using Microsoft.Practices.Unity;


namespace BoxInformation
{
    public partial class Search : Page, ISearchView
    {
        [Dependency]
        public SearchPresenter Presenter { get; set; }

        DataSet results;

        protected void Page_Load(object sender, EventArgs e)
        {
            Presenter.SearchView = this;
        }

        public void Search_Click(object sender, EventArgs e)
        {
            Presenter.GetSearchResults();
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
