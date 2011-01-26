using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BoxInformation.Interfaces
{
    public interface ISearchView
    {
        DataSet searchResults { get; set; }
        string ClientNumber { get; }
        string ClientName { get; }
        string ClientPrincipal { get; }
        string location { get; }
        DateTime? reviewDate { get; }
        string comments { get; }

    }
}
