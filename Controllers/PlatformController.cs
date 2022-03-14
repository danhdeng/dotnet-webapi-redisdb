using Microsoft.AspNetCore.Mvc;
using RedisAPI.Data;

namespace RedisAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase{
    private readonly IPlatformRepo _repo;

    public PlayerController(IPlatformRepo repo){
        _repo =repo;
    }
    
}