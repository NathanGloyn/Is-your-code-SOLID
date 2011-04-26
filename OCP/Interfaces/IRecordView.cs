using System;
using System.Collections.Generic;
using System.Web;

namespace BoxInformation.Interfaces
{
    public interface IRecordView
    {
        string ClientNumber { get; set; }
        string ClientName { get; set; }
        string ClientPrincipal { get; set; }
        DateTime? reviewDate { get; set; }
        string comments { get; set; }
        HttpPostedFile file { get;}
        HttpPostedFile file2 { get;}
        string fileName { get; set; }
        string fileName2 { get; set; }
        string Id { get;}
        bool SecureStorage { get; set; }
        List<KeyValuePair<string, int>> boxDetails { get; set; }

        void AddRecord_Click(object sender, EventArgs e);
    }


}


