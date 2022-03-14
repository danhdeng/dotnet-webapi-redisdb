using System.Text.Json;
using RedisAPI.Models;
using StackExchange.Redis;

namespace RedisAPI.Data;


public class RedisPlatformRepo : IPlatformRepo
{
    private readonly IConnectionMultiplexer _redis;

    public RedisPlatformRepo(IConnectionMultiplexer redis){
        _redis = redis;
    }
    public void CreatePlatform(Platform platform)
    {
        if(platform == null){
            throw new ArgumentOutOfRangeException(nameof(platform));
        }
        var db=_redis.GetDatabase();

        var seerializedPlatform=JsonSerializer.Serialize(platform);

        db.StringSet(platform.id, seerializedPlatform);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        //var db=_redis.GetDatabase();

        throw new NotImplementedException();

    }

    public Platform? GetPlatformById(string id)
    {
       var db=_redis.GetDatabase();

       var platform=db.StringGet(id);
       if(!string.IsNullOrEmpty(platform)){
           return JsonSerializer.Deserialize<Platform> (platform);
       }
       return null;
    }
}