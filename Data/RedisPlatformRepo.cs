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

        // db.StringSet(platform.Id, seerializedPlatform);
        // db.SetAdd("platformset", seerializedPlatform);

        //use hashset to store the data

        db.HashSet("platformHashset", new HashEntry[]{new HashEntry(platform.Id, seerializedPlatform)});
    }

    public IEnumerable<Platform?>? GetAllPlatforms()
    {
        var db=_redis.GetDatabase();

        // var completeSets=db.SetMembers("platformset");
        
        // if(completeSets != null){
        //     var objects =Array.ConvertAll(completeSets, val=>JsonSerializer.Deserialize<Platform>(val));
        //     return objects;
        // }
         var completeHashSet=db.HashGetAll("platformHashset");
        
        if(completeHashSet != null){
            var objects =Array.ConvertAll(completeHashSet, val=>JsonSerializer.Deserialize<Platform>(val.Value));
            return objects;
        }
        return null;

    }

    public Platform? GetPlatformById(string id)
    {
       var db=_redis.GetDatabase();

    //    var platform=db.StringGet(id);
    //    if(!string.IsNullOrEmpty(platform)){
    //        return JsonSerializer.Deserialize<Platform> (platform);
    //    }
     var platform=db.HashGet("platformHashset",id);
       if(!string.IsNullOrEmpty(platform)){
           return JsonSerializer.Deserialize<Platform> (platform);
       }
       return null;
    }
}