using SecurityCompanyWPF.Models;

namespace SecurityCompanyWPF.Interfaces
{
    public interface IDataStorage
    {
        void LoadData();
        void SaveData();
        void BackupData();
        void RestoreData();
        Data GetCurrentData();
    }
}