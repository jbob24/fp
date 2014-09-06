using FinanceProjector.Models.Authentication;

namespace FinanceProjector.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationResponse AuthenticateUser(AuthenticationRequest request);
        SaveUserResponse SaveUser(SaveUserRequest request);
    }
}
