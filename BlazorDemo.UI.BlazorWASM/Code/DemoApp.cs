using BlazorDemo.Core.Shared.Models;

namespace BlazorDemo.UI.BlazorWASM.Code {
    public class DemoApp {
        public AuthenticationManager Authentication { get; private set; }
        public DemoApp(AuthenticationManager _authenticationManager)
        {
            Authentication = _authenticationManager;
        }


    }
}
