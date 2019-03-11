using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Services.Security.Common.Interfaces;
using Services.Security.Core.Interfaces;
using Services.Security.Infrastructure;

namespace Services.Security.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : BaseController
    {
        IUserService _userService;
        IUnitOfWork _unitOfWork;
        public ValuesController(IUnitOfWork unitOfWork, IUserService userService, IConfiguration configuration) :
           base(configuration)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            _userService.AddUser(new Infrastructure.Entities.UserDTO() { Email = "123", Name = "asd", UserName = "123", Password = "asd", UserRoleIDs = new List<string>() { "SystemAdmin" } });
            _unitOfWork.SaveChanges();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
