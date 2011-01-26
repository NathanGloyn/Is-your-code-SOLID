using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BoxDetails.Interfaces;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BoxDetails.Presenter
{
    /// <summary>
    /// Presenter for the LMS Archive, also contains much code that would ordianrily be in the Model
    /// </summary>
    public class BoxDetailsPresenter
    {
        private IRecordView _recordView;
        private ISearchView _searchView;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoxDetailsPresenter"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public BoxDetailsPresenter(IRecordView view )
        {
            if (view == null) throw new Exception("view cannot be null");
            _recordView = view;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoxDetailsPresenter"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public BoxDetailsPresenter(ISearchView view)
        {
            if (view == null) throw new Exception("view cannot be null");
            _searchView = view;
        }

        /// <summary>
        /// GetSearchResults
        /// Uses the BaseDal helper class to retrieve search results based on the paramater list sent to
        /// u_SearchRecords_s .  The search logic is contained in the SQL of that stored procedure.
        /// Updates the view with the results dataset.
        /// </summary>
        /// <returns>bool</returns>
        public bool GetSearchResults()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            DataSet searchresults = new DataSet();

            string sql = "SELECT * FROM BoxDetails WHERE ClientName LIKE '%";
            if (!string.IsNullOrEmpty(_searchView.ClientName))
            {
                sql += _searchView.ClientName;
            }   
            sql += "%'";
            sql += " AND ClientNumber LIKE '%";
            if (!string.IsNullOrEmpty(_searchView.ClientNumber))
            {
                sql += _searchView.ClientNumber;
            }
            sql += "%'";
            sql += " AND ClientLeader LIKE '%";
            if (!string.IsNullOrEmpty(_searchView.ClientPrincipal))
            {
                sql += _searchView.ClientPrincipal;
            }
            sql += "%'";
            sql += " AND (FileLocation LIKE '%";
            if (!string.IsNullOrEmpty(_searchView.location))
            {
                sql += _searchView.location;
            }
            sql += "%' OR";
            sql += " FileLocation2 LIKE '%";
            if(! string.IsNullOrEmpty(_searchView.location))
            {
                sql += _searchView.location;
            }
            sql += "%'";
            sql += ")";

            command.CommandText = sql;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(searchresults);


            _searchView.searchResults = searchresults;

            return true;
        }

        /// <summary>
        /// GetEntry
        /// Uses DAL helper class to execute u_GetRecordByID_s with the RecordID in order to populate
        /// the view from the returned Dataset.  Checks each Row has a value before assigning it.
        /// Calls the ConvertStringToKVP function to get the list of locations and box numbers.
        /// </summary>
        /// <param name="RecordID">string</param>
        /// <returns>bool</returns>
        public bool GetEntry(string RecordID)
        {

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("u_GetRecordByID_s", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ID", RecordID));

            DataSet entry = new DataSet();

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(entry);


            if (entry.Tables[0].Rows[0]["ClientName"].ToString() != "")
            {
                _recordView.ClientName = entry.Tables[0].Rows[0]["ClientName"].ToString();
            }

            if (entry.Tables[0].Rows[0]["ClientNumber"].ToString() != "")
            {
                _recordView.ClientNumber = entry.Tables[0].Rows[0]["ClientNumber"].ToString(); 
            }

            if (entry.Tables[0].Rows[0]["ClientLeader"].ToString() != "")
            {
                _recordView.ClientLeader = entry.Tables[0].Rows[0]["ClientLeader"].ToString();
            }

            if (entry.Tables[0].Rows[0]["ReviewDate"].ToString() != "")
            {
                _recordView.reviewDate = (DateTime?)entry.Tables[0].Rows[0]["ReviewDate"];
            }

            if (entry.Tables[0].Rows[0]["CompletionDate"].ToString() != "")
            {
                _recordView.completionDate = (DateTime?)entry.Tables[0].Rows[0]["CompletionDate"];
            }

            if (entry.Tables[0].Rows[0]["Comments"].ToString() != "")
            {
                _recordView.comments = entry.Tables[0].Rows[0]["Comments"].ToString();
            }

            if (entry.Tables[0].Rows[0]["FileLocation"].ToString() != "")
            {
                _recordView.fileName = entry.Tables[0].Rows[0]["FileLocation"].ToString();
            }

            if (entry.Tables[0].Rows[0]["FileLocation2"].ToString() != "")
            {
                _recordView.fileName2 = entry.Tables[0].Rows[0]["FileLocation2"].ToString();
            }


            if (entry.Tables[0].Rows[0]["SecureStorage"].ToString() != "")
            {
                _recordView.SecureStorage = (bool)entry.Tables[0].Rows[0]["SecureStorage"];
            }

            if (entry.Tables[0].Rows[0]["BoxDetails"].ToString() != "")
            {
                _recordView.boxDetails = ConvertStringToKVP(entry.Tables[0].Rows[0]["BoxDetails"].ToString());
            }

            return true;

        }

        /// <summary>
        /// GetEntry
        /// Uses DAL helper class to execute u_DeleteRecord_d.  Deletes the currently viewed record.
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteRecord()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("u_DeleteRecord_d", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@ID", _recordView.Id));

            try
            {
                return command.ExecuteNonQuery() != 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// UpdateRecord
        /// Uses DAL helper class to execute u_UpdateRecord_u using the current record in the view
        /// Checks each view property has a value before assigning it.
        /// Calls the ConvertKCPToString function to get the string of locations and numbers from the Key Value Pair.
        /// </summary>
        /// <returns>bool</returns>
        public bool UpdateRecord()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("u_AddRecord_i", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@ID", _recordView.Id));
            command.Parameters.Add(new SqlParameter("@ClientName", _recordView.ClientName));
            command.Parameters.Add(new SqlParameter("@ClientNumber", _recordView.ClientNumber));
            command.Parameters.Add(new SqlParameter("@ClientLeader", _recordView.ClientLeader));
            command.Parameters.Add(new SqlParameter("@ReviewDate", _recordView.reviewDate.GetValueOrDefault(new DateTime(1900, 01, 01))));
            command.Parameters.Add(new SqlParameter("@CompletionDate", _recordView.completionDate.GetValueOrDefault(new DateTime(1900, 01, 01))));
            command.Parameters.Add(new SqlParameter("@Comments", _recordView.comments));

            if (_recordView.file.ContentLength > 0)
            {
                _recordView.file.SaveAs(ConfigurationManager.AppSettings["FileSaveLocation"] + Path.GetFileName(_recordView.file.FileName));
                command.Parameters.Add(new SqlParameter("@FileLocation", Path.GetFileName(_recordView.file.FileName)));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@FileLocation", _recordView.fileName));
            }

            if (_recordView.file2.ContentLength > 0)
            {
                _recordView.file2.SaveAs(ConfigurationManager.AppSettings["FileSaveLocation"] + Path.GetFileName(_recordView.file2.FileName));
                command.Parameters.Add(new SqlParameter("@FileLocation2", Path.GetFileName(_recordView.file2.FileName)));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@FileLocation2", _recordView.fileName2));
            }


            command.Parameters.Add(new SqlParameter("@SecureStorage", _recordView.SecureStorage));
            command.Parameters.Add(new SqlParameter("@BoxDetails", ConvertKVPToString(_recordView.boxDetails)));



            try
            {
                return command.ExecuteNonQuery() != 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// AddRecord
        /// Uses DAL helper class to execute u_AddRecord_i using the current record in the view
        /// Takes strProjectName as a parameter and doesn't need to, this should be removed.
        /// Calls the ConvertKCPToString function to get the string of locations and numbers from the Key Value Pair.
        /// </summary>
        /// <returns>bool</returns>
        /// <param name="strClientName">string</param>
        public bool AddRecord(string strClientName)
        {

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("u_AddRecord_i", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@ClientName", _recordView.ClientName));
            command.Parameters.Add(new SqlParameter("@ClientNumber", _recordView.ClientNumber));
            command.Parameters.Add(new SqlParameter("@ClientLeader", _recordView.ClientLeader));
            command.Parameters.Add(new SqlParameter("@ReviewDate", _recordView.reviewDate.GetValueOrDefault(new DateTime(1900,01,01))));
            command.Parameters.Add(new SqlParameter("@CompletionDate", _recordView.completionDate.GetValueOrDefault(new DateTime(1900,01,01))));
            command.Parameters.Add(new SqlParameter("@Comments", _recordView.comments));
            
            if (_recordView.file.ContentLength > 0 )
            {
                _recordView.file.SaveAs(ConfigurationManager.AppSettings["FileSaveLocation"] + Path.GetFileName(_recordView.file.FileName));
                command.Parameters.Add(new SqlParameter("@FileLocation", Path.GetFileName(_recordView.file.FileName)));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@FileLocation", ""));
            }

            if (_recordView.file2.ContentLength > 0)
            {
                _recordView.file2.SaveAs(ConfigurationManager.AppSettings["FileSaveLocation"] + Path.GetFileName(_recordView.file2.FileName));
                command.Parameters.Add(new SqlParameter("@FileLocation2", Path.GetFileName(_recordView.file2.FileName)));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@FileLocation2", ""));
            }


            command.Parameters.Add(new SqlParameter("@SecureStorage", _recordView.SecureStorage));
            command.Parameters.Add(new SqlParameter("@BoxDetails", ConvertKVPToString(_recordView.boxDetails)));

            try
            {
                return command.ExecuteNonQuery() != 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="fleFile">The fle file.</param>
        /// <returns></returns>
        private string UploadFile(HttpPostedFile fleFile)
        {
            byte[] bufFileBuffer = new byte[fleFile.ContentLength];

            fleFile.InputStream.Read(bufFileBuffer, 0, fleFile.ContentLength);

            FileStream newFile = new FileStream(ConfigurationManager.AppSettings["FileSaveLocation"] + Path.GetFileName(fleFile.FileName), FileMode.Create);

            newFile.Write(bufFileBuffer, 0, bufFileBuffer.Length);

            newFile.Close();

            return Path.GetFileName(fleFile.FileName);
        }

        /// <summary>
        /// Converts the KVP to string.
        /// </summary>
        /// <param name="keyValueList">The key value list.</param>
        /// <returns></returns>
        private string ConvertKVPToString(List<KeyValuePair<string, int>> keyValueList)
        {
            if (keyValueList != null)
            {
                StringBuilder resultString = new StringBuilder();
                foreach (KeyValuePair<string, int> keyValue in keyValueList)
                {
                    resultString.Append("*");
                    resultString.Append(keyValue.Key);
                    resultString.Append(",");
                    resultString.Append(keyValue.Value);
                }

                return resultString.ToString();

            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Converts the string to KVP.
        /// </summary>
        /// <param name="strEntryString">The STR entry string.</param>
        /// <returns></returns>
        private List<KeyValuePair<string, int>> ConvertStringToKVP(string strEntryString)
        {
            List<KeyValuePair<string, int>> lstResults = new List<KeyValuePair<string,int>>();

            string[] strResults;
            int result;
            string[] strEntryStringArray = strEntryString.Split('*');

            foreach (string strValue in strEntryStringArray)
            {
                if (strValue.Length > 0)
                {
                    string key = "";
                    int value = 0;
                    strResults = strValue.Split(',');
                    foreach (string strCurrentPair in strResults)
                    {
                        if (int.TryParse(strCurrentPair, out result))
                        {
                            value = int.Parse(strCurrentPair);
                        }
                        else
                        {
                            key = strCurrentPair;
                        }
                    }
                    KeyValuePair<string, int> kvpResults = new KeyValuePair<string, int>(key, value);
                    lstResults.Add(kvpResults);
                }
            }

            return lstResults;
        }

        /// <summary>
        /// Deletes the file1.
        /// </summary>
        public void DeleteFile1()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("u_DeleteFile1_u", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ID", _recordView.Id));

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// Deletes the file2.
        /// </summary>
        public void DeleteFile2()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("u_DeleteFile2_u", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ID", _recordView.Id));

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
            }

        }

    }
}
