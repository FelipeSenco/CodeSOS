using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeSOSProject.ServiceLayer;
using CodeSOSProject.ViewModels;

namespace CodeSOS.ApiControllers
{
    public class AccountController : ApiController
    {
        IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        public bool Get(string email)
        {

            return (this.userService.GetUsersByEmail(email) == null);
            
        }
    }
}
