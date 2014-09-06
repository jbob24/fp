using System;
using FinanceProjector.Domain.Services;
using FinanceProjector.Enums;
using FinanceProjector.Model;
using FinanceProjector.Models.Authentication;
using FinanceProjector.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinanceProjector.Test
{
    [TestClass]
    public class AuthenticationServiceTest
    {
        [TestMethod]
        public void Create_New_User_Test()
        {
            var user = new User()
            {
                FirstName = "Joe",
                LastName = "Henss",
                UserName = "joehenss",
                Password = Hasher.Hash("password"),
                PasswordSecurityQuestion = "Mother's maiden name",
                PasswordSecurityAnswer = "Hamilton"
            };

            var authenticationProvider = new AuthenticationService();
            var saveUserRequest = new SaveUserRequest() { User = user };

            var saveUserResponse = authenticationProvider.SaveUser(saveUserRequest);

            Assert.IsNotNull(saveUserResponse);
            Assert.AreEqual(ResponseStatus.Success, saveUserResponse.Status);

            var authenticateUserRequest = new AuthenticationRequest()
            {
                UserName = user.UserName,
                Password = user.Password
            };

            var authenticationResponse = authenticationProvider.AuthenticateUser(authenticateUserRequest);

            Assert.IsNotNull(authenticationResponse);
            Assert.AreEqual(ResponseStatus.Success, authenticationResponse.Status);

        }
    }
}
