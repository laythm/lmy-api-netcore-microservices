using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Services.Security.Core.Interfaces;
using Services.Security.Infrastructure;

namespace Services.Security.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : BaseController
    {
        IUserService _userService;

        public ValuesController(IUserService userService, IConfiguration configuration) :
           base(configuration)
        {
            _userService = userService;
        }

        // GET api/values
        [HttpGet("AddUser")]
        public ActionResult<IEnumerable<string>> AddUser()
        {
            _userService.AddUser(new Infrastructure.Entities.UserDTO() { Email = "123", Name = "asd", UserName = "123", Password = "asd", UserRoleIDs = new List<string>() { "SystemAdmin" } });

            return new string[] { "value1", "value2" };
        }
    }
}
