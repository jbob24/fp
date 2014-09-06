using FinanceProjector.Model;

namespace FinanceProjector.Models.Authentication
{
    public class SaveUserRequest : RequestBase
    {
        public User User { get; set; }
    }
}
