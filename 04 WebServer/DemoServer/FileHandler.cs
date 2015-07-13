using System;
using System.IO;
using System.Net.Sockets;


namespace DemoServer
{
    public class FileHandler
    {
        private string _contentPath;

        public FileHandler(string contentPath)
        {
            _contentPath = contentPath;
        }

        internal bool DoesFileExists(string directory)
        {
            return File.Exists(_contentPath + directory);
        }

        internal byte[] ReadFile(string path)
        {
            byte[] content = File.ReadAllBytes(_contentPath + path);
            return content;
        }
    }
}
