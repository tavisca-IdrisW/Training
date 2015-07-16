using System;

namespace WebServer
{
    public interface IProcesses
    {
        public string File { get; private set; }
        void DoGet(string uri);
        void DoPost();
    }
}