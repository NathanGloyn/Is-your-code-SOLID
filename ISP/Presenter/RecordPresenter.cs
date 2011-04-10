using System;
using BoxInformation.Interfaces;
using BoxInformation.Model;

namespace BoxInformation.Presenter
{
    public class RecordPresenter
    {
        private readonly BoxEntry box;

        public RecordPresenter(IRecordView view)
        {
            if (view == null) throw new Exception("view cannot be null");

            box = new BoxEntry(view);
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
