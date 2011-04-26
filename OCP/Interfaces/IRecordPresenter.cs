
namespace BoxInformation.Interfaces
{
    public interface IRecordPresenter
    {
        IRecordView RecordView { get; set; }
        void GetRecordById(string recordId);
        void DeleteRecord();
        void UpdateRecord();
        void AddRecord();
        void DeleteManifest();
        void DeleteAgreement();
    }
}