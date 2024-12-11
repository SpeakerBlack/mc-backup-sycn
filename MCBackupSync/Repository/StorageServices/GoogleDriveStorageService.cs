using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Http;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using MCBackupSync.Enum;
using MCBackupSync.Helper;
using MCBackupSync.Model;
using MCBackupSync.Service;

namespace MCBackupSync.Repository.StorageServices
{
    public class GoogleDriveStorageService : IStorageServiceRepository
    {

        private DriveService _driveService;
        private string _folderId;

        public GoogleDriveStorageService()
        {
            _driveService = new DriveService();
            _folderId = AppSettingsService.GetString("StorageServices:GoogleDrive:FolderId");
        }

        public void Connect()
        {
            Console.WriteLine("Connecting to Google Drive...");

            IConfigurableHttpClientInitializer credential;
            GoogleDriveStorageServiceAuthModeEnum serviceAuthModeEnum = (GoogleDriveStorageServiceAuthModeEnum)AppSettingsService.GetInt("StorageServices:GoogleDrive:AuthMode");
            switch (serviceAuthModeEnum)
            {
                case GoogleDriveStorageServiceAuthModeEnum.Auth2:
                    using (var stream = new FileStream(AppSettingsService.GetString("StorageServices:GoogleDrive:AuthFile"), FileMode.Open, FileAccess.Read))
                    {
                        string credPath = "token.json";
                        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                            GoogleClientSecrets.FromStream(stream).Secrets,
                            new[] { DriveService.Scope.Drive },
                            "user",
                            CancellationToken.None,
                            new FileDataStore(credPath, true)).Result;
                    }
                    break;
                case GoogleDriveStorageServiceAuthModeEnum.ServiceClient:
                    using (var stream = new FileStream(AppSettingsService.GetString("StorageServices:GoogleDrive:AuthFile"), FileMode.Open, FileAccess.Read))
                    {
                        credential = GoogleCredential.FromStream(stream).CreateScoped(new[] { DriveService.Scope.Drive });
                    }
                    break;
                default:
                    throw new NotImplementedException("Google authentication not implemented");
            }

            _driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MCBackupSync"
            });
        }

        public void DeleteFile(string fileId)
        {
            _driveService.Files.Delete(fileId).Execute();

            Console.WriteLine("File deleted successfully.");
        }

        public IEnumerable<FileInfoStorageServiceModel> GetFilesInFolder()
        {
            var request = _driveService.Files.List();
            request.Q = $"'{_folderId}' in parents and trashed = false";
            var result = request.Execute();
            var files = result.Files;
            return files.
                Select(x => new FileInfoStorageServiceModel() { Id = x.Id, Name = x.Name });
        }

        public void UploadFile(string filePath)
        {
            int cursorTop = Console.CursorTop;
            int cursorLeft = Console.CursorLeft;
            long sizeFile = FileHelper.GetSizeFile(filePath);
            var progress = new Action<IUploadProgress>(p =>
            {
                Console.SetCursorPosition(cursorLeft, cursorTop);
                Console.WriteLine($"Uploading file... {p.BytesSent * 100 / sizeFile} %");
                Console.CursorLeft = cursorLeft;
            });

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(filePath),
                Parents = !string.IsNullOrEmpty(_folderId) ? new[] { _folderId } : null
            };

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var request = _driveService.Files.Create(fileMetadata, stream, FileHelper.GetMimeType(filePath));
                request.ProgressChanged += progress;
                request.Upload();
            }

            Console.WriteLine("File uploaded successfully.");
        }
        
    }
}
