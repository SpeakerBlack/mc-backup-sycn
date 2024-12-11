using MCBackupSync.Enum;
using MCBackupSync.Repository;
using MCBackupSync.Repository.StorageServices;
using MCBackupSync.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBackupSync.Factory
{
    public class StorageServiceFactory
    {
        public StorageServiceFactory() { }

        public IStorageServiceRepository BuildRepository()
        {
            StorageServiceEnum storageServiceEnum = (StorageServiceEnum)AppSettingsService.GetInt("StorageService");
            return storageServiceEnum switch
            {
                StorageServiceEnum.GoogleDrive => new GoogleDriveStorageService(),
                StorageServiceEnum.MicrosoftOneDrive => new MicrosoftOneDriveStorageService(),
                _ => throw new NotImplementedException("Storage service not implemented"),
            };
        }
    }
}
