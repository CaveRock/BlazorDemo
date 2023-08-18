using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Shared.Models {
    public class ServiceResponseModels {
        public ServiceResponseModels()
        {
            ValidationFieldResult<UserModel> res = new ValidationFieldResult<UserModel>("", x => x.Email);
         
        }
    }


}
