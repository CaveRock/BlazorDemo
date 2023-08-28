using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Shared.Models {
    public class ValidationError {
        public string PropertyName { get; private set; }
        public string ErrorMessage { get; private set; }
        public ValidationError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
    }

    public class ServiceValidationResult<T> {

        [JsonInclude]
        public List<ValidationError> ValidationErrors { get; private set; } 

        public void AddEntityError(string errorMessage)
        {
            ValidationErrors.Add(new ValidationError("", errorMessage));
        }

        public void AddPropertyErrors(List<ValidationResult> validationResults)
        {
            foreach (var result in validationResults)
            {
                AddPropertyError(result);
            }
        }

        public void AddPropertyError(ValidationResult validationResult)
        {
            string memberName = string.Empty;
            if (validationResult.MemberNames.Any())
            {
                memberName = validationResult.MemberNames.First();
            }
            ValidationErrors.Add(new ValidationError(memberName, validationResult.ErrorMessage));
        }

        public void AddPropertyError(Expression<Func<T, object>> fieldAccessor, string errorMessage)
        {
            var memberExpression = fieldAccessor.Body as MemberExpression;
            ValidationErrors.Add(new ValidationError(memberExpression.Member.Name, errorMessage));
        }

        public bool IsValid
        {
            get
            {
                return !ValidationErrors.Any();
            }
        }

        public ServiceValidationResult()
        {
            ValidationErrors = new List<ValidationError>();
        }
    }
}
