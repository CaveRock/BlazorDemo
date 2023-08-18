using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Shared.Models {
   public class ValidationFieldResult<T>
    {
        private readonly Expression<Func<T, object>> _fieldAccessor;
        public string ErrorMessage { get; private set; }

        //An expression that can be used to determine which field on the entity the error is related to
        public Expression<Func<object>> ErrorField
        {
            get
            {
                return ConvertExpression(_fieldAccessor);
            }
        }

        public ValidationFieldResult(Expression<Func<T, object>> fieldAccessor, string errorMessage)
        {
            ErrorMessage = errorMessage;
            _fieldAccessor = fieldAccessor;
        }

        static Expression<Func<TResult>> ConvertExpression<TModel, TResult>(Expression<Func<TModel, TResult>> originalExpression)
        {
            var convertedBody = Expression.Convert(originalExpression.Body, typeof(TResult));
            var lambda = Expression.Lambda<Func<TResult>>(convertedBody, originalExpression.Parameters);
            return lambda;
        }

    }

    public class ValidationError
    {
        public FieldIdentifier Field { get; private set; }
        public string ErrorMessage { get; private set; }
        public ValidationError(FieldIdentifier field, string errorMessage)
        {
            Field = field;
            ErrorMessage = errorMessage;
        }
    }

    public class ValidationResult<T>
    {
        public T Entity { get; private set; }
        //Holds errors that pertain to the entity or action performed that are not related to a specific field
        public List<string> EntityErrors { get; set; }

        //Hold field specific errors
        //Issues with serializing Expressions so we keep this private
        private List<ValidationFieldResult<T>> _fieldErrors;

        //We can serialize this structure so we have it public
        public List<ValidationError> FieldErrors { 
        get
            {
                var errors =  new List<ValidationError>();
                foreach (var err in _fieldErrors)
                {
                    if (Entity != null && err.ErrorField.Name != null)
                    {
                        errors.Add(new ValidationError(new FieldIdentifier(Entity, err.ErrorField.Name), err.ErrorMessage));
                    }
                }
                return errors;
            }
        }

        public void AddFieldError(Expression<Func<T, object>> fieldAccessor, string errorMessage)
        {
            if (_fieldErrors == null)
            {
                _fieldErrors = new List<ValidationFieldResult<T>>();
            }
            _fieldErrors.Add(new ValidationFieldResult<T>(fieldAccessor, errorMessage));
        }

        public bool IsValid
        {
            get
            {
                return FieldErrors.Any() || EntityErrors.Any();
            }
        }

        public ValidationResult(T entity)
        {
            _fieldErrors = new List<ValidationFieldResult<T>>();
            EntityErrors = new List<string>();
            Entity = entity;
        }
    }
}
