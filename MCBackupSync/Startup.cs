using MCBackupSync.Factory;
using MCBackupSync.Repository;

namespace MCBackupSync
{
    public class Startup
    {

        private static StorageServiceFactory _storageServiceFactory;
        private static IStorageServiceRepository _storageServiceRepository;

        public static void Build(string fileName)
        {
            _storageServiceFactory = new StorageServiceFactory();
            _storageServiceRepository = _storageServiceFactory.BuildRepository();

            UploadFile(fileName);
        }

        private static void UploadFile(string fileName)
        {
            _storageServiceRepository.Connect();
            var files = _storageServiceRepository.GetFilesInFolder();
            _storageServiceRepository.UploadFile(fileName);
            foreach (var file in files)
            {
                Console.WriteLine("Deleting: {0}({1})", file.Name, file.Id);
                _storageServiceRepository.DeleteFile(file.Id ?? string.Empty);
            }
        }
    }
}
