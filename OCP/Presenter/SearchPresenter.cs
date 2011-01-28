using System;
using System.Configuration;
using System.Data;
using BoxInformation.Interfaces;
using BoxInformation.Logging;

namespace BoxInformation.Presenter
{
    public class SearchPresenter
    {
        private ISearchView view;
        private IDataAccess dataAccess;
        private ILogger logger;

        internal ISearchView SearchView
        {
            get { return view; }
            set { view = value; }
        }

        public SearchPresenter(IDataAccess dataAccess, ILogger logger)
        {
            if(dataAccess == null) throw new ArgumentNullException("dataAccess");
            if (logger == null) throw new ArgumentNullException("logger");

            this.dataAccess = dataAccess;
            this.logger = logger;
        }

        public void GetSearchResults()
        {
            logger.Log("Call GetSearchResults");
            const string sqlFormat = "SELECT * FROM BoxDetails WHERE ClientName LIKE '%{0}%' AND ClientNumber LIKE '%{1}%' AND ClientLeader LIKE '%{2}%'";

            string sql = string.Format(sqlFormat,
                                       GetFieldValue(view.ClientName),
                                       GetFieldValue(view.ClientNumber),
                                       GetFieldValue(view.ClientPrincipal));

            view.searchResults = dataAccess.FillDataSet(sql, CommandType.Text);
        }

        private static string GetFieldValue(string field)
        {
            string value = null;

            if (!string.IsNullOrEmpty(field))
            {
                value = field;
            }

            return value;
        }
    }
}
