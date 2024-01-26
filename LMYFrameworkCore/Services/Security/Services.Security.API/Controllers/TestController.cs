using Framework.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Security.Core.Interfaces;
using Services.Security.Infrastructure.Entities;

namespace Services.Security.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        IUserService _userService;
        public TestController(ILogger<TestController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("AddUser")]
        public async Task<DTOBase> AddUser(UserDTO userDTO)
        {
            var result = await _userService.AddUser(userDTO);
            return result;
        }
    }
}