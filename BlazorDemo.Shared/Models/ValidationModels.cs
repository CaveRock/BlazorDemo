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

        public ValidationFieldResult(string errorMessage, Expression<Func<T, object>> fieldAccessor)
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

    public class ValidationResult<T>
    {
        //Holds errors that pertain to the entity or action performed that are not related to a specific field
        public List<string> EntityErrors { get; set; }

        //Hold field specific errors
        public List<ValidationFieldResult<T>> FieldErrors { get; set; }

        public bool IsValid
        {
            get
            {
                return FieldErrors.Any() || EntityErrors.Any();
            }
        }

        public ValidationResult()
        {
            FieldErrors = new List<ValidationFieldResult<T>>();
            EntityErrors = new List<string>();
        }
    }
}
