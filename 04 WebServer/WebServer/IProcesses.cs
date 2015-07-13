using System;

namespace WebServer
{
    public interface IProcesses
    {
        void DoGet(string uri);
        void DoPost();
    }
}