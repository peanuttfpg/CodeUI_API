﻿//using CodeUI.Data.Entity;
//using CodeUI.Service.DTO.Response;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CodeUI.Service.Helpers.Enum;

namespace CodeUI.Service.Helpers
{
    public class ServiceHelpers
    {
        public static IConfiguration config;
        public static void Initialize(IConfiguration Configuration)
        {
            config = Configuration;
        }

        public async static Task<dynamic> GetSetDataRedis(RedisSetUpType type, string key, object value = null)
        {
            try
            {
                List<dynamic> rs = new List<dynamic>();
                string connectRedisString = config.GetSection("Endpoint:RedisEndpoint").Value + "," + config.GetSection("Endpoint:Password").Value;
                // Tạo kết nối
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectRedisString);

                // Lấy DB
                IDatabase db = redis.GetDatabase(1);

                // Ping thử
                if (db.Ping().TotalSeconds > 5)
                {
                    throw new TimeoutException("Server Redis không hoạt động");
                }
                switch (type)
                {
                    case RedisSetUpType.GET:
                        var redisValue = db.StringGet(key);
                        rs = JsonConvert.DeserializeObject<dynamic>(redisValue);
                        break;

                    case RedisSetUpType.SET:
                        var redisNewValue = JsonConvert.SerializeObject(value);
                        db.StringSet(key, redisNewValue);
                        break;

                    case RedisSetUpType.DELETE:
                        db.KeyDelete(key);
                        break;
                }

                redis.Close();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
