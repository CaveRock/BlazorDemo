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
        public T Entity { get; set; }
        //Holds errors that pertain to the entity or action performed that are not related to a specific field
        public List<string> EntityErrors { get; set; }

        //Hold field specific errors
        private List<ValidationFieldResult<T>> _fieldErrors;

        public List<FieldIdentifier> FieldIdentifiers { 
        get
            {
                var identifiers = new List<FieldIdentifier>();
                foreach (var err in _fieldErrors)
                {
                    identifiers.Add(new FieldIdentifier(Entity, err.ErrorField.Name));
                }
                return identifiers;
            }
        }

        public bool IsValid
        {
            get
            {
                return FieldIdentifiers.Any() || EntityErrors.Any();
            }
        }

        public ValidationResult(T entity, List<ValidationFieldResult<T>> fieldErrors)
        {
            _fieldErrors = new List<ValidationFieldResult<T>>();
            fieldErrors.AddRange(fieldErrors);
            EntityErrors = new List<string>();
            Entity = entity;
        }
    }
}
