using BlazorDemo.Core.Data.Entities.Identity;
using BlazorDemo.Core.Interfaces.DataAccess;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Services.Authentication {
    public class RegisterUser {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserDataAccessService _userDataAccess;
        public RegisterUser(
            UserManager<ApplicationUser> userManager,
            IUserDataAccessService userDataAccess
            )
        {
            _userManager = userManager;
            _userDataAccess = userDataAccess;
        }



    }
}
