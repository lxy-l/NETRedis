using ServiceStack.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETRedis
{
    class Program
    {
        static void Main(string[] args)
        {


            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            #region  ServiceStack.Redis
            using (RedisClient redisClient = new RedisClient("127.0.0.1", 6379))
            {
                for (int i = 0; i < 1000000000; i++)
                {
                    redisClient.Set("AAA", $"{i}_Value");
                }
                //string valueStr = "";
                //for (int i = 0; i < 100000; i++)
                //{
                //    valueStr = redisClient.Get<string>("AAA");
                //}
            }
            #endregion
            stopwatch.Stop();
            Console.WriteLine($"ServiceStack.Redis 10,0000 次, 耗时: {stopwatch.ElapsedMilliseconds}毫秒");

            stopwatch.Restart();
            #region  StackExchange.Redis
            {
                IConnectionMultiplexer proxy = ConnectionMultiplexer.Connect("127.0.0.1");
                IDatabase db = proxy.GetDatabase(0);
                for (int i = 0; i < 1000000000; i++)
                {
                    db.StringSet("AAAA", $"{i}_Value");
                }
                //string valueStr = "";
                //for (int i = 0; i < 100000; i++)
                //{
                //    RedisValue value = db.StringGet("AAAA");
                //}
            }
            #endregion
            stopwatch.Stop();
            Console.WriteLine($"StackExchange.Redis 10,0000 次, 耗时: {stopwatch.ElapsedMilliseconds}毫秒");
        }
    }
}
