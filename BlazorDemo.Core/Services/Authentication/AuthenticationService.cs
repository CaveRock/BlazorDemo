using BlazorDemo.Core.Data.Entities.Identity;
using BlazorDemo.Core.Interfaces.DataAccess;
using BlazorDemo.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Services.Authentication {
    public class AuthenticationService {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserDataAccessService _userDataAccess;
        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            IUserDataAccessService userDataAccess
            )
        {
            _userManager = userManager;
            _userDataAccess = userDataAccess;
        }

        public async Task<UserModel> RegisterUser(UserModel user, string password, string confirmPassword)
        {
            throw new NotImplementedException();
        }


    }
}
