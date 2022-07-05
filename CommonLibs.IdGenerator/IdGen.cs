using CommonLibs.IdGenerator.Mongodb;
using System;

namespace CommonLibs.IdGenerator
{
    /// <summary>
    /// 静态调用方法，获取Id
    /// </summary>
    public class IdGen
    {
        /// <summary>
        /// Mongodb算法Id
        /// </summary>
        /// <returns>返回生成的24位长度Id</returns>
        public static string GetMongodbId()
        {
            return MongodbObjectId.GenerateNewId().ToString();
        }

        /// <summary>
        /// Mongodb算法Id
        /// </summary>
        /// <param name="timestamp">时间戳，当前时间</param>
        /// <returns>返回生成的24位长度Id</returns>
        public static string GetMongodbId(DateTime timestamp)
        {
            return MongodbObjectId.GenerateNewId(timestamp).ToString();
        }

        /// <summary>
        /// Mongodb算法Id
        /// </summary>
        /// <param name="timestamp">时间戳，1970年到现在秒数</param>
        /// <returns>返回生成的24位长度Id</returns>
        public static string GetMongodbId(int timestamp)
        {
            return MongodbObjectId.GenerateNewId(timestamp).ToString();
        }

        /// <summary>
        /// 雪花算法Id
        /// </summary>
        /// <param name="workerId">机器编号 0-255</param>
        /// <returns>返回 18-19位 长度的数字</returns>
        public static long GetSnowFlakeId(int workerId)
        {
            return new SnowFlake(workerId).NextId();
        }
    }
}
