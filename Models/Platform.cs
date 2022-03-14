using System.ComponentModel.DataAnnotations;

namespace RedisAPI.Models;

public class Platform{

    [Required]
    public string id { get; set; }=$"Platform:{Guid.NewGuid().ToString()}";
    
    [Required]
    public string Name { get; set; }=string.Empty;
}