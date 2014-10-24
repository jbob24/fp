using FinanceProjector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.Models.Authentication
{
    public class RegisterUserRequest : RequestBase
    {
        public User User { get; set; }

        public static bool IsValid(RegisterUserRequest request)
        {
            return request != null && request.User != null && !string.IsNullOrEmpty(request.User.FirstName) && !string.IsNullOrEmpty(request.User.LastName) && !string.IsNullOrEmpty(request.User.UserName) && !string.IsNullOrEmpty(request.User.Password);
        }
    }
}
