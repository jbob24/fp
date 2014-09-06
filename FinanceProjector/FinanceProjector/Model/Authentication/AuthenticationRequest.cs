using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.Models.Authentication
{
    public class AuthenticationRequest : RequestBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public static bool IsValid(AuthenticationRequest request)
        {
            return request != null && !string.IsNullOrWhiteSpace(request.UserName) && !string.IsNullOrWhiteSpace(request.Password);
        }
    }
}
