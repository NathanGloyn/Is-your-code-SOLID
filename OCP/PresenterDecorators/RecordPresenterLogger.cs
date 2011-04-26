using System;
using BoxInformation.Interfaces;

namespace BoxInformation.PresenterDecorators
{
    public class RecordPresenterLogger:IRecordPresenter
    {
        private readonly ILogger logger;
        private readonly IRecordPresenter presenter;

        public RecordPresenterLogger(IRecordPresenter presenter, ILogger logger) 
        {
            if (presenter == null) throw new Exception("presenter cannot be null");
            if (logger == null) throw new Exception("logger cannot be null");
            this.logger = logger;
            this.presenter = presenter;
        }

        public IRecordView RecordView
        {
            get { return presenter.RecordView; }
            set { presenter.RecordView = value; }
        }

        public void GetRecordById(string recordId)
        {
            logger.Log("Call GetRecordById recordId: " + recordId);
            presenter.GetRecordById(recordId);
        }

        public void DeleteRecord()
        {
            logger.Log("Call DeleteRecord for recordId: " + RecordView.Id);
            presenter.DeleteRecord();
        }

        public void UpdateRecord()
        {
            logger.Log("Call UpdateRecord for recordId: " + RecordView.Id);
            presenter.UpdateRecord();
        }

        public void AddRecord()
        {
            logger.Log("Call AddRecord for recordId: " + RecordView.ClientName);
            presenter.AddRecord();
        }

        public void DeleteManifest()
        {
            logger.Log("Call DeleteManifest for recordId: " + RecordView.Id);
            presenter.DeleteManifest();
        }

        public void DeleteAgreement()
        {
            logger.Log("Call DeleteAgreement for recordId: " + RecordView.Id);
            presenter.DeleteAgreement();
        }
    }
}