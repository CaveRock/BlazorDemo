using System.Reflection.Emit;

namespace BlazorDemo.API.Routes {
    public class ApiEndpoints {
        public const string ApiBase = "/api/";
        public class Authentication
        {
            public const string AuthenticationController = $"{ApiBase}authentication";
            public const string Register = "register";
        }
    }
}