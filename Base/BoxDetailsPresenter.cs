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
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BoxInformation.Interfaces;

namespace BoxInformation.Presenter
{

    public class BoxDetailsPresenter
    {
        private IView _view;

        public BoxDetailsPresenter(IView view)
        {
            if (view == null) throw new Exception("view cannot be null");
            _view = view;
        }

        public bool GetSearchResults()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            DataSet searchresults = new DataSet();

            string sql = "SELECT * FROM BoxDetails WHERE ClientName LIKE '%";
            if (!string.IsNullOrEmpty(_view.ClientName))
            {
                sql += _view.ClientName;
            }   
            sql += "%'";
            sql += " AND ClientNumber LIKE '%";
            if (!string.IsNullOrEmpty(_view.ClientNumber))
            {
                sql += _view.ClientNumber;
            }
            sql += "%'";
            sql += " AND ClientLeader LIKE '%";
            if (!string.IsNullOrEmpty(_view.ClientPrincipal))
            {
                sql += _view.ClientPrincipal;
            }
            sql += "%'";
            

            command.CommandText = sql;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(searchresults);


            _view.searchResults = searchresults;

            return true;
        }

        public bool GetEntry(string RecordID)
        {

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("GetRecordByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ID", RecordID));

            DataSet entry = new DataSet();

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(entry);


            if (entry.Tables[0].Rows[0]["ClientName"].ToString() != "")
            {
                _view.ClientName = entry.Tables[0].Rows[0]["ClientName"].ToString();
            }

            if (entry.Tables[0].Rows[0]["ClientNumber"].ToString() != "")
            {
                _view.ClientNumber = entry.Tables[0].Rows[0]["ClientNumber"].ToString(); 
            }

            if (entry.Tables[0].Rows[0]["ClientLeader"].ToString() != "")
            {
                _view.ClientPrincipal = entry.Tables[0].Rows[0]["ClientLeader"].ToString();
            }

            if (entry.Tables[0].Rows[0]["ReviewDate"].ToString() != "")
            {
                _view.reviewDate = (DateTime?)entry.Tables[0].Rows[0]["ReviewDate"];
            }

            if (entry.Tables[0].Rows[0]["Comments"].ToString() != "")
            {
                _view.comments = entry.Tables[0].Rows[0]["Comments"].ToString();
            }

            if (entry.Tables[0].Rows[0]["FileLocation"].ToString() != "")
            {
                _view.fileName = entry.Tables[0].Rows[0]["FileLocation"].ToString();
            }

            if (entry.Tables[0].Rows[0]["FileLocation2"].ToString() != "")
            {
                _view.fileName2 = entry.Tables[0].Rows[0]["FileLocation2"].ToString();
            }

            if (entry.Tables[0].Rows[0]["SecureStorage"].ToString() != "")
            {
                _view.SecureStorage = (bool)entry.Tables[0].Rows[0]["SecureStorage"];
            }

            if (entry.Tables[0].Rows[0]["BoxDetails"].ToString() != "")
            {
                _view.boxDetails = ConvertStringToKVP(entry.Tables[0].Rows[0]["BoxDetails"].ToString());
            }

            return true;

        }

        public bool DeleteRecord()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("DeleteRecord", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@ID", _view.Id));

            try
            {
                return command.ExecuteNonQuery() != 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateRecord()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("UpdateRecord", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@ID", _view.Id));
            command.Parameters.Add(new SqlParameter("@ClientName", _view.ClientName));
            command.Parameters.Add(new SqlParameter("@ClientNumber", _view.ClientNumber));
            command.Parameters.Add(new SqlParameter("@ClientLeader", _view.ClientPrincipal));
            command.Parameters.Add(new SqlParameter("@ReviewDate", _view.reviewDate.GetValueOrDefault(new DateTime(1900, 01, 01))));
            command.Parameters.Add(new SqlParameter("@Comments", _view.comments));

            if (_view.file.ContentLength > 0)
            {
                _view.file.SaveAs(ConfigurationManager.AppSettings["FileSaveLocation"] + Path.GetFileName(_view.file.FileName));
                command.Parameters.Add(new SqlParameter("@FileLocation", Path.GetFileName(_view.file.FileName)));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@FileLocation", _view.fileName));
            }

            if (_view.file2.ContentLength > 0)
            {
                _view.file2.SaveAs(ConfigurationManager.AppSettings["FileSaveLocation"] + Path.GetFileName(_view.file2.FileName));
                command.Parameters.Add(new SqlParameter("@FileLocation2", Path.GetFileName(_view.file2.FileName)));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@FileLocation2", _view.fileName2));
            }


            command.Parameters.Add(new SqlParameter("@SecureStorage", _view.SecureStorage));
            command.Parameters.Add(new SqlParameter("@BoxDetails", ConvertKVPToString(_view.boxDetails)));



            try
            {
                return command.ExecuteNonQuery() != 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool AddRecord(string strClientName)
        {

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("AddRecord", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@ClientName", _view.ClientName));
            command.Parameters.Add(new SqlParameter("@ClientNumber", _view.ClientNumber));
            command.Parameters.Add(new SqlParameter("@ClientLeader", _view.ClientPrincipal));
            command.Parameters.Add(new SqlParameter("@ReviewDate", _view.reviewDate.GetValueOrDefault(new DateTime(1900,01,01))));
            command.Parameters.Add(new SqlParameter("@Comments", _view.comments));
            
            if (_view.file.ContentLength > 0 )
            {
                _view.file.SaveAs(ConfigurationManager.AppSettings["FileSaveLocation"] + Path.GetFileName(_view.file.FileName));
                command.Parameters.Add(new SqlParameter("@FileLocation", Path.GetFileName(_view.file.FileName)));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@FileLocation", ""));
            }

            if (_view.file2.ContentLength > 0)
            {
                _view.file2.SaveAs(ConfigurationManager.AppSettings["FileSaveLocation"] + Path.GetFileName(_view.file2.FileName));
                command.Parameters.Add(new SqlParameter("@FileLocation2", Path.GetFileName(_view.file2.FileName)));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@FileLocation2", ""));
            }


            command.Parameters.Add(new SqlParameter("@SecureStorage", _view.SecureStorage));
            command.Parameters.Add(new SqlParameter("@BoxDetails", ConvertKVPToString(_view.boxDetails)));

            try
            {
                return command.ExecuteNonQuery() != 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        
        private string UploadFile(HttpPostedFile fleFile)
        {
            byte[] bufFileBuffer = new byte[fleFile.ContentLength];

            fleFile.InputStream.Read(bufFileBuffer, 0, fleFile.ContentLength);

            FileStream newFile = new FileStream(ConfigurationManager.AppSettings["FileSaveLocation"] + Path.GetFileName(fleFile.FileName), FileMode.Create);

            newFile.Write(bufFileBuffer, 0, bufFileBuffer.Length);

            newFile.Close();

            return Path.GetFileName(fleFile.FileName);
        }

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

        public void DeleteFile1()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("DeleteFile1", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ID", _view.Id));

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
            }
        }

        public void DeleteFile2()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("DeleteFile2", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ID", _view.Id));

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
