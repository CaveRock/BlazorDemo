using BlazorDemo.Core.Interfaces.DataAccess;
using BlazorDemo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Data {
    public class UserDataAccessService : IUserDataAccessService {

        private readonly BlazorDemoContext _db;
        public UserDataAccessService(BlazorDemoContext db)
        {
            _db = db;
        }

        public UserDataAccessService() { }

        public async Task<UserModel> Get(UserId id)
        {
            
            throw new NotImplementedException();
        }

        public async Task<UserModel> Create(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
