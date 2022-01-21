using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DataExchangeWorkerService.Helpers
{
    public static class FileHelper
    {
        public static FileInfo[] GetAllFiles(string directoryPath)
        {
            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), directoryPath);
            DirectoryInfo directory = new DirectoryInfo(pathBuilt);
            return directory.GetFiles().Where(c => c.Extension == ".xlsx").OrderBy(c => c.CreationTimeUtc).ToArray();
        }

        public static FileInfo GetFileByName(string directoryPath, string fileName)
        {
            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), directoryPath);
            DirectoryInfo directory = new DirectoryInfo(pathBuilt);
            return directory.GetFiles().FirstOrDefault(c => c.Name.Contains(fileName));
        }

        public static void MoveFile(string clientName, string filePath, string sourcePath, string destinationPath)
        {
            sourcePath = Path.Combine(Directory.GetCurrentDirectory(), sourcePath);
            destinationPath = Path.Combine(Directory.GetCurrentDirectory(), destinationPath);

            DirectoryInfo directory = new DirectoryInfo(sourcePath);
            var file = directory.GetFiles().FirstOrDefault(c => c.FullName == filePath);
            if (file is { Exists: true })
            {
                var path = Path.Combine(destinationPath,
                    clientName + DateTime.UtcNow.ToString("yyyy-MM-dd-hh-mm-ss") + file.Extension);
                File.Move(file.FullName, path);
            }
        }

        public static Type GetClientType(string clientName)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types) 
            {
                if (type.Name.Contains(clientName))
                {
                    return type;
                }
            }
        
            return null;
        }
    }
}
