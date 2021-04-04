using Consul;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools.Consul
{
    public class ConsulNetClient
    {
        private ConsulOption _consulOption;
        public ConsulNetClient(ConsulOption consulOption)
        {
            //EnsureUtil.NotNull(consulOption, "consulOption");
            _consulOption = consulOption;
        }

        private ConsulClient _consulClient
        {
            get
            {
                Action<ConsulClientConfiguration> cfgAction = (cfg) =>
                {
                    cfg.Address = new Uri(_consulOption.Host);
                    cfg.Datacenter = _consulOption.DataCenter;
                    cfg.Token = _consulOption.Token;
                    cfg.WaitTime = _consulOption.WaitTime;
                };
                //Action<HttpClient> cfgHttpAction = (client) => { _consulOption.TimeOut?client.Timeout = _consulOption.TimeOut; };
                return new ConsulClient(cfgAction);
            }
        }

        public void Dispose()
        {
            _consulClient?.Dispose();
        }


        public async Task<bool> KVAcquireAsync(KVPair p)
        {
            var result = await _consulClient.KV.Acquire(p);
            return result.Response;
        }

        public async Task<bool> KVAcquireAsync(KVPair p, WriteOptions q)
        {
            var result = await _consulClient.KV.Acquire(p, q);
            return result.Response;
        }

        public async Task<bool> KVCasAsync(KVPair p)
        {
            var result = await _consulClient.KV.CAS(p);
            return result.Response;
        }

        public async Task<bool> KVCasAsync(KVPair p, WriteOptions q)
        {
            var result = await _consulClient.KV.CAS(p, q);
            return result.Response;
        }

        public async Task<bool> KVDeleteAsync(string key)
        {
            var result = await _consulClient.KV.Delete(key);
            return result.Response;
        }

        public async Task<bool> KVDeleteAsync(string key, WriteOptions q)
        {
            var result = await _consulClient.KV.Delete(key, q);
            return result.Response;
        }

        public async Task<bool> KVDeleteCasAsync(KVPair p)
        {
            var result = await _consulClient.KV.DeleteCAS(p);
            return result.Response;
        }

        public async Task<bool> KVDeleteCasAsync(KVPair p, WriteOptions q)
        {
            var result = await _consulClient.KV.DeleteCAS(p, q);
            return result.Response;
        }

        public async Task<bool> KVDeleteTreeAsync(string prefix)
        {
            var result = await _consulClient.KV.DeleteTree(prefix);
            return result.Response;
        }

        public async Task<bool> KVDeleteTreeAsync(string prefix, WriteOptions q)
        {
            var result = await _consulClient.KV.DeleteTree(prefix, q);
            return result.Response;
        }

        public async Task<KVPair> KVGetAsync(string key)
        {
            var result = await _consulClient.KV.Get(key);
            return result.Response;
        }

        public async Task<KVPair> KVGetAsync(string key, QueryOptions q)
        {
            var result = await _consulClient.KV.Get(key, q);
            return result.Response;
        }

        public async Task<T> KVGetAsync<T>(string key) where T : class
        {
            var obj = await KVGetValueAsync(key);
            
            return JsonConvert.DeserializeObject<T>(obj);
        }

        public async Task<T> KVGetAsync<T>(string key, QueryOptions q) where T : class
        {
            var obj = await KVGetValueAsync(key, q);
            return JsonConvert.DeserializeObject<T>(obj);
        }

        public async Task<string> KVGetValueAsync(string key)
        {
            var obj = await KVGetAsync(key);
            return GetString(obj?.Value);
        }

        public async Task<string> KVGetValueAsync(string key, QueryOptions q)
        {
            var obj = await KVGetAsync(key, q);
            return GetString(obj?.Value);
        }

        public async Task<string[]> KVKeysAsync(string prefix)
        {
            var result = await _consulClient.KV.Keys(prefix);
            return result.Response;
        }


        public async Task<string[]> KVKeysAsync(string prefix, string separator)
        {
            var result = await _consulClient.KV.Keys(prefix, separator);
            return result.Response;
        }

        public async Task<string[]> KVKeysAsync(string prefix, string separator, QueryOptions q)
        {
            var result = await _consulClient.KV.Keys(prefix, separator, q);
            return result.Response;
        }

        public async Task<KVPair[]> KVListAsync(string prefix)
        {
            var result = await _consulClient.KV.List(prefix);
            return result.Response;
        }

        public async Task<KVPair[]> KVListAsync(string prefix, QueryOptions q)
        {
            var result = await _consulClient.KV.List(prefix, q);
            return result.Response;
        }

        public async Task<IEnumerable<T>> KVListAsync<T>(string prefix) where T : class
        {
            List<T> list = new List<T>();
            var kvList = await KVListAsync(prefix);
            if (kvList != null)
                foreach (var t in kvList?.ToList())
                {
                    list.Add(JsonConvert.DeserializeObject<T>(GetString(t.Value)));
                }
            return list;
        }

        public Task<bool> KVPutAsync(string key, string value)
        {
            KVPair kv = new KVPair(key)
            {
                Value = GetByte(value)
            };
            return KVPutAsync(kv);
        }

        public Task<bool> KVPutAsync<T>(string key, T value) where T : class
        {
            KVPair kv = new KVPair(key)
            {
                Value = GetByte(JsonConvert.SerializeObject(value))
            };
            return KVPutAsync(kv);
        }

        public async Task<bool> KVPutAsync(KVPair p)
        {
            var result = await _consulClient.KV.Put(p);
            return result.Response;
        }

        public async Task<bool> KVPutAsync(KVPair p, WriteOptions q)
        {
            var result = await _consulClient.KV.Put(p, q);
            return result.Response;
        }

        public async Task<bool> KVReleaseAsync(KVPair p)
        {
            var result = await _consulClient.KV.Release(p);
            return result.Response;
        }

        public async Task<bool> KVReleaseAsync(KVPair p, WriteOptions q)
        {
            var result = await _consulClient.KV.Release(p, q);
            return result.Response;
        }

        private byte[] GetByte(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        private string GetString(byte[] value)
        {
            if (value == null)
                return string.Empty;
            return Encoding.UTF8.GetString(value);
        }
    }
}
