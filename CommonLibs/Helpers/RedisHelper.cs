using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonLibs.Helper
{
#pragma warning disable CS1591 
    public class RedisHelper : IRedisHelper
    {
        private ConnectionMultiplexer _ConnectionMultiplexer;
        private string _connectionString;
        private int _Db = 0;
        private IDatabase _Database;
        private IServer _Server;
        private object objLock = new object();


        private static ConcurrentDictionary<int, IRedisHelper> dbs = new ConcurrentDictionary<int, IRedisHelper>();

        public IRedisHelper this[int db]
        {
            get
            {
                lock (objLock)
                {
                    //var helper = dbs.GetOrAdd(db, new RedisHelper(_connectionString, db));
                    //return helper;
                    if (dbs.TryGetValue(db, out IRedisHelper redis))
                    {
                        return redis;
                    }
                    dbs[db] = (new RedisHelper(_connectionString, db));
                    return dbs[db];
                }
            }
        }

        public RedisHelper(string connectionString, int db = 0)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            if (db < 0)
                db = 0;
            _connectionString = connectionString;
            _Db = db;
            lock (objLock)
            {
                _ConnectionMultiplexer = GetConnectionMultiplexer();

                if (_connectionString.Contains(",defaultDatabase="))
                {
                    _Database = _ConnectionMultiplexer.GetDatabase();
                }
                else
                {
                    _Database = _ConnectionMultiplexer.GetDatabase(db);
                }
                if (_Server == null)
                {
                    _Server = _ConnectionMultiplexer.GetServer(_ConnectionMultiplexer.GetEndPoints()[0]);
                }
                dbs[db] = this;
            }
        }

        private ConnectionMultiplexer GetConnectionMultiplexer()
        {
            if (_ConnectionMultiplexer != null && _ConnectionMultiplexer.IsConnected)
            {
                return _ConnectionMultiplexer;
            }
            _ConnectionMultiplexer = ConnectionMultiplexer.Connect(_connectionString);
            return _ConnectionMultiplexer;
        }

        public bool Add(string key, string value, TimeSpan? expiry = null)
        {
            return _Database.StringSet(key, value, expiry);
        }

        public bool Add<T>(string key, T value)
        {
            return _Database.StringSet(key, value.ToJson(false));
        }

        public bool Add<T>(string key, T value, DateTime expireAt)
        {
            return _Database.StringSet(key, value.ToJson(false), expireAt.Subtract(DateTime.Now));
        }

        public async Task<bool> AddAsync<T>(string key, T value, TimeSpan expireIn)
        {
            return await _Database.StringSetAsync(key, value.ToJson(false), expireIn);
        }

        public async Task<bool> AddAsync<T>(string key, T value)
        {
            return await _Database.StringSetAsync(key, value.ToJson(false));
        }

        public async Task<bool> AddAsync<T>(string key, T value, DateTime expireAt)
        {
            return await _Database.StringSetAsync(key, value.ToJson(false), expireAt.Subtract(DateTime.Now));
        }

        public bool Add<T>(string key, T value, TimeSpan expireIn)
        {
            return _Database.StringSet(key, value.ToJson(false), expireIn);
        }

        public long AddItemtoList(string key, string value)
        {
            return _Database.ListRightPush(key, value);
        }

        public bool AddItemtoSet(string key, string value)
        {
            return _Database.SetAdd(key, value);
        }

        public bool AddItemToSortSet(string key, string value, double score)
        {
            return _Database.SortedSetAdd(key, value, score);
        }

        public long AddRangeToList(string key, List<string> values)
        {
            List<RedisValue> redisValues = new List<RedisValue>(values.Count);
            values.ForEach(value =>
            {
                redisValues.Add(value);
            });
            return _Database.ListRightPush(key, redisValues.ToArray());
        }

        public long AddRangeToSet(string key, List<string> values)
        {
            List<RedisValue> redisValues = new List<RedisValue>(values.Count);
            values.ForEach(value =>
            {
                redisValues.Add(value);
            });
            return _Database.SetAdd(key, redisValues.ToArray());
        }

        public long AddRangeToSortSet(string key, List<string> values, double score)
        {
            List<SortedSetEntry> redisValues = new List<SortedSetEntry>(values.Count);
            values.ForEach(value =>
            {
                redisValues.Add(new SortedSetEntry(value, score));
            });
            return _Database.SortedSetAdd(key, redisValues.ToArray());
        }

        public long AppendToValue(string key, string value)
        {
            return _Database.StringAppend(key, value);
        }

        public string Get(string key)
        {
            var redisValue = _Database.StringGet(key);
            if (redisValue.IsNullOrEmpty)
                return string.Empty;
            return redisValue;
        }


        public async Task<string> GetAsync(string key)
        {
            var redisValue = await _Database.StringGetAsync(key);
            if (redisValue.IsNullOrEmpty)
                return string.Empty;
            return redisValue;
        }

        public T Get<T>(string key)
        {
            T value = default(T);
            var redisValue = _Database.StringGet(key);
            if (redisValue.HasValue)
            {
                string strJson = redisValue.ToString();
                if (!strJson.IsNullOrWhiteSpace())
                    value = strJson.FromJson<T>();
            }
            return value;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            T value = default(T);
            var redisValue = await _Database.StringGetAsync(key);
            if (redisValue.HasValue)
            {
                string strJson = redisValue.ToString();
                if (!strJson.IsNullOrWhiteSpace())
                    value = strJson.FromJson<T>();
            }
            return value;
        }

        public IList<string> GetHashKeys(string hashId)
        {
            List<string> keys = new List<string>();
            var rvs = _Database.HashKeys(hashId);
            if (rvs != null)
            {
                keys.AddRange(rvs.ToStringArray());
            }
            return keys;
        }

        public List<string> GetHashValues(string hashId)
        {
            List<string> retValues = new List<string>();

            RedisValue[] values = _Database.HashValues(hashId);
            if (values != null)
                retValues.AddRange(values.ToStringArray());

            return retValues;
        }

        public IList<string> GetAllItemsFromList(string listId)
        {
            return new List<string>(_Database.ListRange(listId).ToStringArray());
        }

        public IList<string> GetAllItemsFromSet(string setId)
        {
            return new List<string>(_Database.SetMembers(setId).ToStringArray());
        }

        public IList<string> GetAllItemsFromSortSet(string setId)
        {
            return new List<string>(_Database.SortedSetRangeByRank(setId).ToStringArray());
        }

        public IList<string> GetAllItemsFromSortSetDesc(string setId)
        {
            return new List<string>(_Database.SortedSetRangeByRank(setId, 0, -1, Order.Descending, CommandFlags.None).ToStringArray());
        }

        public string GetValueFromHash(string hashId, string key)
        {
            return _Database.HashGet(hashId, key);
        }

        public async Task<string> GetValueFromHashAsync(string hashId, string key)
        {
            return await _Database.HashGetAsync(hashId, key);
        }

        public bool Remove(string key)
        {
            return _Database.KeyDelete(key);
        }

        public long RemoveAll(IEnumerable<string> keys)
        {
            return _Database.KeyDelete(keys.ConvertAllX<string, RedisKey>(p => p).ToArray());
        }

        public bool ContainsKey(string key)
        {
            return _Database.KeyExists(key);
        }

        public bool ExpireAt(string key, DateTime expireAt)
        {
            return _Database.KeyExpire(key, expireAt);
        }

        public bool ExpireIn(string key, DateTime expireIn)
        {
            return _Database.KeyExpire(key, expireIn);
        }

        public bool Set<T>(string key, T value)
        {
            return _Database.StringSet(key, value.ToJson());
        }

        public bool Set<T>(string key, T value, TimeSpan expiresIn)
        {
            return _Database.StringSet(key, value.ToJson(), expiresIn);
        }

        public bool Set<T>(string key, T value, DateTime expiresAt)
        {
            return _Database.StringSet(key, value.ToJson(), expiresAt.Subtract(DateTime.Now));
        }

        public async Task<bool> SetAsync<T>(string key, T value)
        {
            return await _Database.StringSetAsync(key, value.ToJson());
        }

        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan expiresIn)
        {
            return await _Database.StringSetAsync(key, value.ToJson(), expiresIn);
        }

        public async Task<bool> SetAsync<T>(string key, T value, DateTime expiresAt)
        {
            return await _Database.StringSetAsync(key, value.ToJson(), expiresAt.Subtract(DateTime.Now));
        }

        public bool SetEntryIfNotExists(string key, string value)
        {
            bool ret = false;
            ret = _Database.StringSet(key, value, null, When.NotExists);
            return ret;
        }

        public bool SetEntryIfNotExists(string key, string value, TimeSpan expireIn)
        {
            bool ret = false;
            ret = _Database.StringSet(key, value, expireIn, When.NotExists);
            return ret;
        }

        public bool SetEntryInHash(string hashId, string key, string value)
        {
            bool result = true;
            result = _Database.HashSet(hashId, key, value);
            return result;
        }


        public async Task<bool> SetEntryInHashAsync(string hashId, string key, string value)
        {
            bool result = true;
            result = await _Database.HashSetAsync(hashId, key, value);
            return result;
        }

        public bool SetEntryInHashIfNotExists(string hashId, string key, string value)
        {
            bool ret = false;
            if (!_Database.HashExists(hashId, key))
                ret = _Database.HashSet(hashId, key, value);
            return ret;
        }

        public void SetItemInList(string listId, int listIndex, string value)
        {
            _Database.ListSetByIndex(listId, listIndex, value);
        }

        public double IncrementItemInSortedSet(string setId, string value, long incrementBy)
        {
            return _Database.SortedSetIncrement(setId, value, incrementBy);
        }

        public double IncrementItemInSortedSet(string setId, string value, double incrementBy)
        {
            return _Database.SortedSetIncrement(setId, value, incrementBy);
        }

        public List<string> GetAllKeys()
        {
            List<string> retValues = new List<string>();
            RedisKey[] values = _Server.Keys((int)_Db).ToArray();
            if (values != null)
                retValues = values.ConvertAllX<RedisKey, string>(p => (string)p).ToList();
            return retValues;
        }

        public long GetHashCount(string hashId)
        {
            return _Database.HashLength(hashId);
        }

        public long GetListCount(string listId)
        {
            return _Database.ListLength(listId);
        }

        public long GetSetCount(string setId)
        {
            return _Database.SetLength(setId);
        }

        public long GetSortedSetCount(string setId)
        {
            return _Database.SortedSetLength(setId);
        }

        public TimeSpan GetTimeToLive(string key)
        {
            TimeSpan? ts = _Database.KeyTimeToLive(key);
            return ts.HasValue ? ts.Value : new TimeSpan(0);
        }

        public bool HashContainsEntry(string hashId, string key)
        {
            return _Database.HashExists(hashId, key);
        }

        /// <summary>
        /// 从当前DB中搜索匹配指定模式的所有Key。
        /// </summary>
        /// <param name="pattern">搜索匹配模式。如"*foo*"表示搜索Key中包含"foo"的Key.</param>
        /// <returns></returns>
        public List<string> SearchKeys(string pattern)
        {
            return new List<string>(_Server.Keys((int)_Db, pattern).ToArray().ConvertAllX<RedisKey, string>(p => (string)p));
        }

        /// <summary>
        /// 从当前DB中搜索匹配指定模式的Key，按指定的每页数量分页，返回指定页的所有Key。
        /// </summary>
        /// <param name="pattern">搜索匹配模式。如"*foo*"表示搜索Key中包含"foo"的Key.</param>
        /// <param name="pageSize">每页数量。</param>
        /// <param name="pageOffset">要返回第几页。</param>
        /// <returns>符合条件的Key列表。</returns>
        public List<string> SearchKeys(string pattern, int pageSize = 10000, int pageOffset = 0)
        {
            return new List<string>(_Server.Keys((int)_Db, pattern, pageSize, 0, pageOffset).ConvertAllX<RedisKey, string>(p => (string)p));
        }

        public string PopItemWithHighestScoreFromSortedSet(string setId)
        {
            string result = string.Empty;
            if (!setId.IsNullOrWhiteSpace())
            {
                //锁的方式性能较低，用事务模式。
                ITransaction transaction = _Database.CreateTransaction();
                Task<RedisValue[]> getItemTask = transaction.SortedSetRangeByRankAsync(setId, -1, -1);
                Task<long> removeItemTask = transaction.SortedSetRemoveRangeByRankAsync(setId, -1, -1);
                transaction.Execute();
                if (getItemTask.Result != null && getItemTask.Result.Length > 0)
                {
                    result = getItemTask.Result[0];
                }
            }
            return result;
        }

        public string PopItemWithLowestScoreFromSortedSet(string setId)
        {
            string result = string.Empty;
            if (!setId.IsNullOrWhiteSpace())
            {
                //锁的方式性能较低，用事务模式。
                ITransaction transaction = _Database.CreateTransaction();
                Task<RedisValue[]> getItemTask = transaction.SortedSetRangeByRankAsync(setId, 0, 0, Order.Ascending);
                Task<long> removeItemTask = transaction.SortedSetRemoveRangeByRankAsync(setId, 0, 0);
                transaction.Execute();
                if (getItemTask.Result != null && getItemTask.Result.Length > 0)
                {
                    result = getItemTask.Result[0];
                }
            }
            return result;
        }

        public bool RemoveEntryFromHash(string hashId, string key)
        {
            bool result = true;
            result = _Database.HashDelete(hashId, key);
            return result;
        }

        public async Task<bool> RemoveEntryFromHashAsync(string hashId, string key)
        {
            bool result = true;
            result = await _Database.HashDeleteAsync(hashId, key);
            return result;
        }

        public long RemoveItemFromList(string listId, string value)
        {
            return _Database.ListRemove(listId, value);
        }

        public bool RemoveItemFromSet(string setId, string item)
        {
            return _Database.SetRemove(setId, item);
        }

        public bool RemoveItemFromSortedSet(string setId, string value)
        {
            return _Database.SortedSetRemove(setId, value);
        }

        public void RenameKey(string fromName, string toName)
        {
            _Database.KeyRename(fromName, toName);
        }

        public long Increment(string key, uint amount)
        {
            return _Database.StringIncrement(key, amount);
        }
        public long IncrementValue(string key)
        {
            return _Database.StringIncrement(key);
        }
    }
    public interface IRedisHelper
    {
        IRedisHelper this[int db] { get; }
        bool Add<T>(string key, T value);
        bool Add<T>(string key, T value, DateTime expireAt);
        bool Add<T>(string key, T value, TimeSpan expireIn);
        bool Add(string key, string value, TimeSpan? expiry = null);
        Task<bool> AddAsync<T>(string key, T value, TimeSpan expireIn);
        Task<bool> AddAsync<T>(string key, T value);
        Task<bool> AddAsync<T>(string key, T value, DateTime expireAt);
        long AddItemtoList(string key, string value);
        bool AddItemtoSet(string key, string value);
        bool AddItemToSortSet(string key, string value, double score);
        long AddRangeToList(string key, List<string> values);
        long AddRangeToSet(string key, List<string> values);
        long AddRangeToSortSet(string key, List<string> values, double score);
        long AppendToValue(string key, string value);
        bool ContainsKey(string key);
        bool ExpireAt(string key, DateTime expireAt);
        bool ExpireIn(string key, DateTime expireIn);
        string Get(string key);
        T Get<T>(string key);
        Task<T> GetAsync<T>(string key);
        Task<string> GetAsync(string key);
        IList<string> GetAllItemsFromList(string listId);
        IList<string> GetAllItemsFromSet(string setId);
        IList<string> GetAllItemsFromSortSet(string setId);
        IList<string> GetAllItemsFromSortSetDesc(string setId);
        List<string> GetAllKeys();
        long GetHashCount(string hashId);
        IList<string> GetHashKeys(string hashId);
        List<string> GetHashValues(string hashId);
        long GetListCount(string listId);
        long GetSetCount(string setId);
        long GetSortedSetCount(string setId);
        TimeSpan GetTimeToLive(string key);
        string GetValueFromHash(string hashId, string key);
        Task<string> GetValueFromHashAsync(string hashId, string key);
        bool HashContainsEntry(string hashId, string key);
        long Increment(string key, uint amount);
        double IncrementItemInSortedSet(string setId, string value, double incrementBy);
        double IncrementItemInSortedSet(string setId, string value, long incrementBy);
        long IncrementValue(string key);
        string PopItemWithHighestScoreFromSortedSet(string setId);
        string PopItemWithLowestScoreFromSortedSet(string setId);
        bool Remove(string key);
        long RemoveAll(IEnumerable<string> keys);
        bool RemoveEntryFromHash(string hashId, string key);
        Task<bool> RemoveEntryFromHashAsync(string hashId, string key);
        long RemoveItemFromList(string listId, string value);
        bool RemoveItemFromSet(string setId, string item);
        bool RemoveItemFromSortedSet(string setId, string value);
        void RenameKey(string fromName, string toName);
        List<string> SearchKeys(string pattern, int pageSize = 10000, int pageOffset = 0);
        List<string> SearchKeys(string pattern);
        bool Set<T>(string key, T value, DateTime expiresAt);
        bool Set<T>(string key, T value);
        bool Set<T>(string key, T value, TimeSpan expiresIn);
        Task<bool> SetAsync<T>(string key, T value, DateTime expiresAt);
        Task<bool> SetAsync<T>(string key, T value, TimeSpan expiresIn);
        Task<bool> SetAsync<T>(string key, T value);
        bool SetEntryIfNotExists(string key, string value, TimeSpan expireIn);
        bool SetEntryIfNotExists(string key, string value);
        bool SetEntryInHash(string hashId, string key, string value);
        Task<bool> SetEntryInHashAsync(string hashId, string key, string value);
        bool SetEntryInHashIfNotExists(string hashId, string key, string value);
        void SetItemInList(string listId, int listIndex, string value);
    }

#pragma warning restore CS1591
}
