using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechnicalQandAForum.ViewModels;
using TechnicalQandAForum.ServiceLayer;

namespace TechnicalQandAForum.ApiControllers
{
    public class AccountController : ApiController
    {
        IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public string GetEmail(string email)
        {
            if (_userService.GetUserByEmail(email) != null)
            {
                return "Email exists";
            }
            else
            {
                return "Email not found";
            }
        }

    }
}
