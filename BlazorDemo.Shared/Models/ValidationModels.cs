using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Shared.Models {
    public class ValidationError {
        public FieldIdentifier Field { get; private set; }
        public string ErrorMessage { get; private set; }
        public ValidationError(FieldIdentifier field, string errorMessage)
        {
            Field = field;
            ErrorMessage = errorMessage;
        }
    }

    public class ServiceValidationResult<T> {
        public T Entity { get; private set; }
        
        private List<ValidationResult> _propertyValidationResults;

        public List<ValidationResult> PropertyValidationResults
        {
            get
            {
                return _propertyValidationResults;
            }
        }

        public List<ValidationError> FieldIdentifierValidationResults
        {
            get
            {
                List<ValidationError> errors = new List<ValidationError>();
                foreach (var err in PropertyValidationResults)
                {
                    FieldIdentifier id = new FieldIdentifier(Entity, err.MemberNames.First());
                    errors.Add(new ValidationError(id, err.ErrorMessage));
                }
                return errors;
            }
        }

        public void AddEntityError(string errorMessage)
        {
            _propertyValidationResults.Add(new ValidationResult(errorMessage));
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
            _propertyValidationResults.Add(validationResult);
        }

        public void AddPropertyError(Expression<Func<T, object>> fieldAccessor, string errorMessage)
        {
            var expression = ConvertExpression(fieldAccessor);

            List<string> memberNames = new List<string>();
            memberNames.Add(expression.Name);
            var err = new ValidationResult(errorMessage, memberNames);

            _propertyValidationResults.Add(err);    
        }

        public bool IsValid
        {
            get
            {
                return _propertyValidationResults.Any();
            }
        }

        static Expression<Func<TResult>> ConvertExpression<TModel, TResult>(Expression<Func<TModel, TResult>> originalExpression)
        {
            var convertedBody = Expression.Convert(originalExpression.Body, typeof(TResult));
            var lambda = Expression.Lambda<Func<TResult>>(convertedBody, originalExpression.Parameters);
            return lambda;
        }

        public ServiceValidationResult(T entity)
        {
            _propertyValidationResults = new List<ValidationResult>();
            Entity = entity;
        }
    }
}
