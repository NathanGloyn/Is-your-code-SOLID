using System;
using BoxInformation.Interfaces;
using BoxInformation.Logging;
using BoxInformation.Model;

namespace BoxInformation.Presenter
{
    public class RecordPresenter
    {
        private readonly ILogger logger;

        public IRecordView RecordView 
        {
            get { return box.View; }
            set { box.View = value; }
        }

        private readonly IBoxEntry box;

        public RecordPresenter(IBoxEntry box, ILogger logger)
        {
            if (box == null) throw new Exception("box cannot be null");

            this.box = box;
            this.logger = logger;
        }

        public void GetRecordById(string RecordID)
        {
            logger.Log("Call GetRecordById recordId: " + RecordID);
            box.Get(RecordID);
        }

        public void DeleteRecord()
        {
            logger.Log("Call DeleteRecord for recordId: " + RecordView.Id);
            box.Delete();
        }

        public void UpdateRecord()
        {
            logger.Log("Call UpdateRecord for recordId: " + RecordView.Id);
            box.Update();
        }

        public void AddRecord()
        {
            logger.Log("Call AddRecord for recordId: " + RecordView.ClientName);
            box.Add();
        }

        public void DeleteManifest()
        {
            logger.Log("Call DeleteManifest for recordId: " + RecordView.Id);
            box.DeleteManifest();
        }

        public void DeleteAgreement()
        {
            logger.Log("Call DeleteAgreement for recordId: " + RecordView.Id);
            box.DeleteAgreement();
        }
    }
}
