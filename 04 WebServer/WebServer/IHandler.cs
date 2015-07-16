using System;


namespace WebServer
{
    public interface IHandler
    {
        string SupportedTypes { get; }
        Response Process(Request request);
    }
}
