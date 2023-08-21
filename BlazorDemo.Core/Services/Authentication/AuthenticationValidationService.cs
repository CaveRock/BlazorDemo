using BlazorDemo.Core.Server.Interfaces.DataAccess;
using BlazorDemo.Core.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Server.Services.Authentication {
    public class AuthenticationValidationService {
        private readonly IUserDataAccessService _userDataAccessService;
        public AuthenticationValidationService(
            IUserDataAccessService userDataAccessService
            )
        {
            _userDataAccessService = userDataAccessService;
        }

        public async Task<ServiceValidationResult<RegisterUserModel>> RegisterUser(RegisterUserModel user)
        {

            var result = new ServiceValidationResult<RegisterUserModel>(user);

            var validationResults = new List<ValidationResult>();

            ValidationContext ctx = new ValidationContext(user);

            //Attribute validation rules
            if (!Validator.TryValidateObject(user.FirstName, ctx, validationResults))
            {
                result.AddPropertyErrors(validationResults);
                //Return early 
                return result;
            }

            //Further non attribute validation - possible validation rules needing IO to complete
            result.AddPropertyError(x => x.FirstName, "Must be John");

            return result;
        }
    }
}
