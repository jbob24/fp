using FinanceProjector.Model;

namespace FinanceProjector.Models.Authentication
{
    public class RegisterUserResponse : ResponseBase
    {
        public User User { get; set; }
    }
}
