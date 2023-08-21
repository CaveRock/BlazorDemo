using BlazorDemo.Core.Data.Entities.Identity;
using BlazorDemo.Core.Server.Interfaces.DataAccess;
using BlazorDemo.Core.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Server.Services.Authentication {
    public class AuthenticationService {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserDataAccessService _userDataAccess;
        private readonly AuthenticationValidationService _validator;
        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            IUserDataAccessService userDataAccess,
            AuthenticationValidationService validator
            )
        {
            _userManager = userManager;
            _userDataAccess = userDataAccess;
            _validator = validator;
        }

        public async Task<ServiceActionResult<RegisterUserModel>> RegisterUser(RegisterUserModel user)
        {
            var serviceResult = new ServiceActionResult<RegisterUserModel>(user);



            var validationResult = await _validator.RegisterUser(user);

            if (validationResult.IsValid)
            {

            }

   

            serviceResult.AddValidationResult(validationResult);

            return serviceResult;
        }


    }
}
