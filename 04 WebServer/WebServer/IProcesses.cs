using System;

namespace WebServer
{
    public interface IProcesses
    {

        string File { get; set; }
        void DoGet(string uri);
        void DoPost();
    }
}