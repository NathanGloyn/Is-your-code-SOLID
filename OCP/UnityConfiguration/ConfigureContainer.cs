using System.Configuration;
using BoxInformation.Interfaces;
using BoxInformation.Logging;
using BoxInformation.Model;
using BoxInformation.Presenter;
using Microsoft.Practices.Unity;

namespace BoxInformation.UnityConfiguration
{
    public static class ConfigureContainer
    {
        public static void Application(IUnityContainer container)
        {
            container.RegisterType<IDataAccess, SqlDataAccess>
                (new InjectionConstructor
                    (ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
                .RegisterType<ILogger, DebugLogger>();
        }

        public  static void Session(IUnityContainer container)
        {
            container.RegisterType<ISearchPresenter, SearchPresenter>()
                     .RegisterType<ISearchPresenter, SearchPresenterDecorator>
                         (new InjectionConstructor
                             (new ResolvedParameter<SearchPresenter>(), new ResolvedParameter<ILogger>()))
                     .RegisterType<ISearchView, Search>()
                     .RegisterType<IBoxEntry, BoxEntry>()
                     .RegisterType<IRecordPresenter, RecordPresenter>()
                     .RegisterType<IRecordPresenter, RecordPresenterLogger>
                         (new InjectionConstructor
                            (new ResolvedParameter<RecordPresenter>(), new ResolvedParameter<ILogger>()))
                     .RegisterType<IRecordView, ViewRecord>();            
        }

    }
}