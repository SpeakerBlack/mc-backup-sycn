using MCBackupSync.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBackupSync.Repository.StorageServices
{
    public class MicrosoftOneDriveStorageService : IStorageServiceRepository
    {
        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void DeleteFile(string fileId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileInfoStorageServiceModel> GetFilesInFolder()
        {
            throw new NotImplementedException();
        }

        public void UploadFile(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
