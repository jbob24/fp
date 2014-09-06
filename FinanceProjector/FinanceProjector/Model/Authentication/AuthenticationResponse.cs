using FinanceProjector.Model;

namespace FinanceProjector.Models.Authentication
{
    public class AuthenticationResponse : ResponseBase
    {
        public User User { get; set; }
    }
}
