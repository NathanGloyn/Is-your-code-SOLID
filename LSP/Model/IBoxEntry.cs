using System.Data;
using BoxInformation.Interfaces;

namespace BoxInformation.Model
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