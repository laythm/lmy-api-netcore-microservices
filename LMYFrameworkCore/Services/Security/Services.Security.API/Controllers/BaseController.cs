using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Security.API.Controllers
{
    public class BaseController
    {
        // protected ContextInfo _contextInfo { get; set; }
        protected IConfiguration _configuration;
        //protected LMYFrameworkCoreContext dbContext;

        public string[] AllowedRoles;
        public bool AllowAnonymous;
        public string[] AllowedAnonymousActions;
        public BaseController()
        {
        }

        public BaseController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
    }
}
