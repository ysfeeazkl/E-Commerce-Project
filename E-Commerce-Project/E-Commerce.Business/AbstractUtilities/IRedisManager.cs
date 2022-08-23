using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.AbstractUtilities
{
    public interface IRedisService
    {
        Task SetStringAsync<T>(string key, T value, DateTime expire);
        Task SetStringAsync<T>(string key, T value);
        Task<T> GetStringAsync<T>(string key);
    }
}
