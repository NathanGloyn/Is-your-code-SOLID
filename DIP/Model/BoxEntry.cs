using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using BoxInformation.Interfaces;

namespace BoxInformation.Model
{
    public class BoxEntry : IBoxEntry
    {
        private IDataAccess dataAccess;
        public IRecordView View { get; set; }
        
        public BoxEntry(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public void Get(string boxId)
        {
            DataSet entry = dataAccess.FillDataSet("GetRecordByID", CommandType.StoredProcedure, dataAccess.CreateParameter("@ID", boxId));

            if (entry.Tables.Count < 1 || entry.Tables.Count > 1)
            {
                throw new InvalidOperationException("Multiple data tables returned when only one expected");
            }

            if (entry.Tables[0].Rows.Count != 1)
            {
                throw new InvalidOperationException("Multiple rows of data returned when only one expected");
            }

            PopulateView(entry.Tables[0].Rows[0]);

        }

        public void Delete()
        {
            dataAccess.ExecuteNonQuery("DeleteRecord", CommandType.StoredProcedure, dataAccess.CreateParameter("@ID", View.Id));
        }

        public void Update()
        {
            IDbDataParameter[] parameters = CreateParameters(true);

            dataAccess.ExecuteNonQuery("UpdateRecord", CommandType.StoredProcedure, parameters);
        }

        public void Add()
        {
            IDbDataParameter[] parameters = CreateParameters(false);

            dataAccess.ExecuteNonQuery("AddRecord", CommandType.StoredProcedure, parameters);

        }

        public void DeleteManifest()
        {
            dataAccess.ExecuteNonQuery("DeleteFile1", CommandType.StoredProcedure, dataAccess.CreateParameter("@ID", View.Id));
        }

        public void DeleteAgreement()
        {
            dataAccess.ExecuteNonQuery("DeleteFile2", CommandType.StoredProcedure, dataAccess.CreateParameter("@ID", View.Id));
        }

        public void PopulateView(DataRow entry)
        {
            if (entry["ClientName"].ToString() != "")
            {
                View.ClientName = entry["ClientName"].ToString();
            }

            if (entry["ClientNumber"].ToString() != "")
            {
                View.ClientNumber = entry["ClientNumber"].ToString();
            }

            if (entry["ClientLeader"].ToString() != "")
            {
                View.ClientPrincipal = entry["ClientLeader"].ToString();
            }

            if (entry["ReviewDate"].ToString() != "")
            {
                View.reviewDate = (DateTime?)entry["ReviewDate"];
            }

            if (entry["Comments"].ToString() != "")
            {
                View.comments = entry["Comments"].ToString();
            }

            if (entry["FileLocation"].ToString() != "")
            {
                View.fileName = entry["FileLocation"].ToString();
            }

            if (entry["FileLocation2"].ToString() != "")
            {
                View.fileName2 = entry["FileLocation2"].ToString();
            }

            if (entry["SecureStorage"].ToString() != "")
            {
                View.SecureStorage = (bool)entry["SecureStorage"];
            }

            if (entry["BoxDetails"].ToString() != "")
            {
                View.boxDetails = ConvertStringToKVP(entry["BoxDetails"].ToString());
            }
        }

        private List<KeyValuePair<string, int>> ConvertStringToKVP(string strEntryString)
        {
            List<KeyValuePair<string, int>> lstResults = new List<KeyValuePair<string, int>>();

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

        private IDbDataParameter[] CreateParameters(bool forUpdate)
        {
            int offSet = 0;
            IDbDataParameter[] parameters = new SqlParameter[9];
            
            if (forUpdate)
            {
                parameters = new SqlParameter[10];
                parameters[0] = dataAccess.CreateParameter("@ID", View.Id);
                offSet++;
            }

            parameters[0 + offSet] = dataAccess.CreateParameter("@ClientName", View.ClientName);
            parameters[1 + offSet] = dataAccess.CreateParameter("@ClientNumber", View.ClientNumber);
            parameters[2 + offSet] = dataAccess.CreateParameter("@ClientLeader", View.ClientPrincipal);
            parameters[3 + offSet] = dataAccess.CreateParameter("@ReviewDate", View.reviewDate.GetValueOrDefault(new DateTime(1900, 01, 01)));
            parameters[4 + offSet] = dataAccess.CreateParameter("@Comments", View.comments);
            parameters[5 + offSet] = dataAccess.CreateParameter("@FileLocation", "");
            parameters[6 + offSet] = dataAccess.CreateParameter("@FileLocation2", "");
            parameters[7 + offSet] = dataAccess.CreateParameter("@SecureStorage", View.SecureStorage);
            parameters[8 + offSet] = dataAccess.CreateParameter("@BoxDetails", ConvertKVPToString(View.boxDetails));

            if (View.file.ContentLength > 0)
            {
                View.file.SaveAs(ConfigurationManager.AppSettings["FileSaveLocation"] + Path.GetFileName(View.file.FileName));
                parameters[5 + offSet].Value = Path.GetFileName(View.file.FileName);
            }

            if (View.file2.ContentLength > 0)
            {
                View.file2.SaveAs(ConfigurationManager.AppSettings["FileSaveLocation"] + Path.GetFileName(View.file2.FileName));
                parameters[6 + offSet].Value = Path.GetFileName(View.file2.FileName);
            }

            return parameters;
        }
        
    }
}