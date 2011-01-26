using System;
using BoxInformation.Interfaces;
using BoxInformation.Model;

namespace BoxInformation.Presenter
{
    public class RecordPresenter
    {
        public IRecordView RecordView 
        {
            get { return box.View; }
            set { box.View = value; }
        }

        private readonly IBoxEntry box;

        public RecordPresenter(IBoxEntry box)
        {
            if (box == null) throw new Exception("box cannot be null");

            this.box = box;
        }

        public void GetRecordById(string RecordID)
        {
            box.Get(RecordID);
        }

        public void DeleteRecord()
        {
            box.Delete();
        }

        public void UpdateRecord()
        {
            box.Update();
        }

        public void AddRecord()
        {
            box.Add();
        }

        public void DeleteManifest()
        {
            box.DeleteManifest();
        }

        public void DeleteAgreement()
        {
            box.DeleteAgreement();
        }
    }
}
