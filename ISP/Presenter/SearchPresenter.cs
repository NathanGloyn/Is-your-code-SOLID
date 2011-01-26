using System;
using System.Configuration;
using System.Data;
using BoxInformation.Interfaces;

namespace BoxInformation.Presenter
{
    public class SearchPresenter
    {
        private readonly ISearchView view;

        public SearchPresenter(ISearchView view)
        {
            if (view == null) throw new Exception("view cannot be null");
            this.view = view;
        }

        public void GetSearchResults()
        {
            const string sqlFormat = "SELECT * FROM BoxDetails WHERE ClientName LIKE '%{0}%' AND ClientNumber LIKE '%{1}%' AND ClientLeader LIKE '%{2}%' AND (FileLocation LIKE '%{3}%' OR FileLocation2 LIKE '%{4}%')";

            string sql = string.Format(sqlFormat,
                                       GetFieldValue(view.ClientName),
                                       GetFieldValue(view.ClientNumber),
                                       GetFieldValue(view.ClientPrincipal),
                                       GetFieldValue(view.location),
                                       GetFieldValue(view.location));

            var dataAccess = new SqlDataAccess(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
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
