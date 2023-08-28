using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Shared.Models {
    public class ServiceActionResult<TActionEntity> {
        public TActionEntity Entity { get; set; }

        [JsonInclude]
        public ServiceValidationResult<TActionEntity> ValidationResult { get; private set; }
        public bool Succeeded
        {
            get
            {
                return ValidationResult.IsValid;
            }
        }

        public ServiceActionResult(TActionEntity entity)
        {
            Entity = entity;
            ValidationResult = new ServiceValidationResult<TActionEntity>();
        }

        //[JsonConstructorAttribute]
        //public ServiceActionResult(TActionEntity entity, ServiceValidationResult<TActionEntity> validationResult)
        //{
        //    Entity = entity;
        //    ValidationResult = validationResult;
        //}

        public void SetValidationResult(ServiceValidationResult<TActionEntity> validationResult)
        {
            ValidationResult = validationResult;
        }

    }

    public class ServiceGetResult<TGetEntity> {
        public TGetEntity? Entity { get; private set; }

        public ServiceGetResult(TGetEntity? entity)
        {
            Entity = entity;
        }
    }


}
