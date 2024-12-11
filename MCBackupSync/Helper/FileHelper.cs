using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MCBackupSync.Helper
{
    public class FileHelper
    {
        public static string GetMimeType(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".txt" => MediaTypeNames.Text.Plain,
                ".jpg" => MediaTypeNames.Image.Jpeg,
                ".png" => MediaTypeNames.Image.Png,
                ".gif" => MediaTypeNames.Image.Gif,
                ".pdf" => MediaTypeNames.Application.Pdf,
                ".zip" => MediaTypeNames.Application.Zip,
                _ => MediaTypeNames.Application.Octet,
            };
        }

        public static long GetSizeFile(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            long size = fileInfo.Length;
            return size;
        }
    }
}
