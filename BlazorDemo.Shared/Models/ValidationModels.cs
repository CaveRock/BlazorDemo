using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Shared.Models {
   public class ValidationFieldResult<T>
    {
        private Expression<Func<T, object>> _fieldAccessor;
        public string ErrorMessage { get; private set; }

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
            var parameter = Expression.Parameter(typeof(TModel), "x");
            var convertedBody = Expression.Convert(originalExpression.Body, typeof(TResult));
            var lambda = Expression.Lambda<Func<TResult>>(convertedBody, originalExpression.Parameters);
            return lambda;
        }

    }

    public class ValidationResult<T>
    {
        public List<ValidationFieldResult<T>> Errors { get; set; }
        public bool IsValid
        {
            get
            {
                return Errors.Any();
            }
        }
        public ValidationResult()
        {
            Errors = new List<ValidationFieldResult<T>>();
        }
    }
}
