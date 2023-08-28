using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Shared.Models {

    public class UserId : IFormattable {
        public Guid Value { get; }

        public UserId(Guid value)
        {
            Value = value;
        }

        public static UserId New() => new UserId(Guid.NewGuid());

        public static UserId FromGuidAsString(string id) => new UserId(Guid.Parse(id));

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            return Value.ToString(format, formatProvider);
        }
    }
    public class UserModel {
        public UserId UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }
}
