using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Shared.Models {
    public class ServiceActionResult<TActionEntity> {
        public TActionEntity Entity { get; set; }

        public ValidationResult<TActionEntity> ValidationResult { get; private set; }
        public ServiceActionResult(TActionEntity entity)
        {
            ValidationResult = new ValidationResult<TActionEntity>();
            Entity = entity;
        }

        public void AddValidationResult(ValidationResult<TActionEntity> validationResult)
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
