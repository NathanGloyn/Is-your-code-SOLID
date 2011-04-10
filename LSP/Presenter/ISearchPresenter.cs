using BoxInformation.Interfaces;

namespace BoxInformation.Presenter
{
    public interface ISearchPresenter
    {
        ISearchView SearchView { get; set; }
        void GetSearchResults();
    }
}