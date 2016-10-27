using System;
using System.IO;

namespace WebServer
{
    public class FileHandler
    {
        private string _filePath {get; set;}

        public string FilePath
        {
            get 
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
            }
        }

        public FileHandler(string filePath)
        {
            FilePath = filePath;
        }

        internal bool FileExists(string directory)
        {
            return File.Exists(FilePath + directory);
        }

        internal byte[] ReadFile(string path)
        {
            byte[] body = File.ReadAllBytes(_filePath + path);
            return body;
        }
    }
}