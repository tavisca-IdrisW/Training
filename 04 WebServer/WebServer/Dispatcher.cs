using System;
using System.Text;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;
using System.Collections.Generic;

namespace WebServer
{
    class Dispatcher
    {
        private static Dictionary<string, Type> _handlerMapping = new Dictionary<string, Type>();
        static Dispatcher()
        {
            var type = typeof(IHandler);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass);

            types.ToList().ForEach(handlerType =>
            {
                var instance = Activator.CreateInstance(handlerType) as IHandler;
                if (string.IsNullOrWhiteSpace(instance.SupportedTypes)) throw new ArgumentException("Invalid handler registered");
                instance.SupportedTypes.Split(',').ToList().ForEach(fileType =>
                {
                    _handlerMapping[fileType.ToLower()] = handlerType;
                });
            });
        }

        private Socket _socket = null;
        private static FactoryHandler handlerFactory = new FactoryHandler();
        public void Start(Socket socket)
        {
            _socket = socket;
        }
        private void Dispatch(Socket socket)
        {
            IProcesses request;
            if (string.IsNullOrEmpty(request.File))
            {
                request = new ErrorHandler(socket, 404);
            }
            else
            {
                var handler = this.ResolveHandler(request.File);
                new Worker(request).Process(handler);
            }
        }

        private IHandler ResolveHandler(string extension)
        {
            if (_handlerMapping.ContainsKey(extension) == false) throw new Exception("Handler not found: Extension - " + extension);

            return Activator.CreateInstance(_handlerMapping[extension]) as IHandler;
        }
    }
}