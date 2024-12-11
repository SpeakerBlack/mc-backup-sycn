using MCBackupSync.Model;

namespace MCBackupSync.Repository
{
    public interface IStorageServiceRepository
    {
        void Connect();

        void UploadFile(string filePath);

        IEnumerable<FileInfoStorageServiceModel> GetFilesInFolder();

        void DeleteFile(string fileId);
    }
}
