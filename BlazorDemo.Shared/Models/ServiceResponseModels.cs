using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Shared.Models {
    public class ServiceActionResult<TActionEntity> {
        public TActionEntity Entity { get; set; }

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
