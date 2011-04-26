using System.Data;

namespace BoxInformation.Interfaces
{
    public interface ISearchView
    {
        DataSet searchResults { get; set; }
        string ClientNumber { get; }
        string ClientName { get; }
        string ClientPrincipal { get; }
    }
}
