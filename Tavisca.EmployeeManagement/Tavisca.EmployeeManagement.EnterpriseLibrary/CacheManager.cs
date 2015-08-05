using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace Tavisca.EmployeeManagement.EnterpriseLibrary
{
    public class CacheManager : Interface.ICacheManager
    {

        public void Flush(string managerName)
        {
            var cacheManager = CacheFactory.GetCacheManager(managerName);
            cacheManager.Flush();
        }

        public object Get(string key, string managerName)
        {
            var cacheManager = CacheFactory.GetCacheManager(managerName);
            return cacheManager.GetData(key);
        }

        public void Add(string key, object value, string managerName)
        {
            var cacheManager = CacheFactory.GetCacheManager(managerName);
            cacheManager.Add(key, value);
        }

        public void Remove(string key, string managerName)
        {
            var cacheManager = CacheFactory.GetCacheManager(managerName);
            cacheManager.Remove(key);
        }
    }
}
