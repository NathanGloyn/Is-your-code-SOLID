using System;
using BoxInformation.Interfaces;
using BoxInformation.Logging;
using BoxInformation.Presenter;

namespace BoxInformation.PresenterDecorators
{
    public class SearchPresenterDecorator:ISearchPresenter
    {
        private readonly ILogger logger;
        private readonly ISearchPresenter presenter;

        public ISearchView SearchView 
        {
            get { return presenter.SearchView; } 
            set { presenter.SearchView = value; }
        }

        public SearchPresenterDecorator(ISearchPresenter presenter, ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException("logger");
            if (presenter == null) throw new ArgumentNullException("presenter");
            this.logger = logger;
            this.presenter = presenter;
        }

        public void GetSearchResults()
        {
            logger.Log("Call GetSearchResults");
            presenter.GetSearchResults();
        }
    }
}