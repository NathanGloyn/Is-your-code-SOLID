using System.Data;

namespace BoxInformation.Interfaces
{
    public interface IBoxEntry
    {
        IRecordView View { get; set; }
        void Get(string boxId);
        void Delete();
        void Update();
        void Add();
        void DeleteManifest();
        void DeleteAgreement();
        void PopulateView(DataRow entry);
    }
}