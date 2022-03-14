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

        db.StringSet(platform.Id, seerializedPlatform);
        db.SetAdd("platformset", seerializedPlatform);
    }

    public IEnumerable<Platform?>? GetAllPlatforms()
    {
        var db=_redis.GetDatabase();

        var completeSets=db.SetMembers("platformset");
        
        if(completeSets != null){
            var objects =Array.ConvertAll(completeSets, val=>JsonSerializer.Deserialize<Platform>(val));
            return objects;
        }
        return null;

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