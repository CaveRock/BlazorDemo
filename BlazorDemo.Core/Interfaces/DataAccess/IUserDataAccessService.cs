using BlazorDemo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Interfaces.DataAccess {
    public interface IUserDataAccessService {
        Task<UserModel> Create(UserModel user);
        Task<UserModel> Get(UserId id);
    }

    
}
