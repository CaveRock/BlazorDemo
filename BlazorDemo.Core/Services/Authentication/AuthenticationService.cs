using BlazorDemo.Core.Data.Entities.Identity;
using BlazorDemo.Core.Server.Interfaces.DataAccess;
using BlazorDemo.Core.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BlazorDemo.Core.Server.Services.Authentication {
    public class AuthenticationService {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserDataAccessService _userDataAccess;
        private readonly AuthenticationValidationService _validator;

        public AuthenticationService(
            ILogger<AuthenticationService> logger,
            UserManager<ApplicationUser> userManager,
            IUserDataAccessService userDataAccess,
            AuthenticationValidationService validator
            )
        {
            _logger = logger;
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
                var newUserId = new UserId(Guid.NewGuid());
                ApplicationUser appUser = new ApplicationUser()
                {
                    Id = newUserId.Value.ToString(),
                    Email = user.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = user.Email
                };

                //Ensure identity and user create action are atomic
                using (var txscope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var identityResult = await _userManager.CreateAsync(appUser, user.Password);

                        if (identityResult.Succeeded)
                        {
                            UserModel newUser = new UserModel();
                            newUser.Email = user.Email;
                            newUser.UserId = newUserId;
                            newUser.FirstName = user.FirstName;
                            newUser.LastName = user.LastName;

                            await _userDataAccess.Create(newUser);
                        }
                        else
                        {
                            AddClientFriendlyErrors(validationResult, identityResult);
                        }
                    }
                    catch (Exception)
                    {
                        txscope.Dispose();
                        throw;
                    }

                    txscope.Complete();
                }
            }

            serviceResult.SetValidationResult(validationResult);

            return serviceResult;
        }

        private static void AddClientFriendlyErrors(ServiceValidationResult<RegisterUserModel> validationResult, IdentityResult identityResult)
        {
            //Error codes taken from:
            //https://github.com/aspnet/AspNetIdentity/blob/main/src/Microsoft.AspNet.Identity.Core/Resources.resx

            foreach (var err in identityResult.Errors)
            {
                switch (err.Code)
                {
                    case "DuplicateEmail":
                        validationResult.AddPropertyError(x => x.Email, "Email already exists");
                        break;
                    case "PasswordRequireDigit":
                    case "PasswordRequireLower":
                    case "PasswordRequireNonLetterOrDigit":
                    case "PasswordRequireUpper":
                    case "PasswordTooShort":
                    case "PasswordMismatch":
                        validationResult.AddPropertyError(x => x.Password, err.Description);
                        break;
                        default:
                        //
                        //string exceptionErrors = string.Join(" | ", identityResult.Errors);
                        //throw new Exception(exceptionErrors);
                        break;
                }
            }
        }
    }


}
