namespace BoxInformation.Interfaces
{
    public interface ISearchPresenter
    {
        ISearchView SearchView { get; set; }
        void GetSearchResults();
    }
}