using System;
using System.Linq;
using FinanceProjector.Enums;
using FinanceProjector.Interfaces;
using FinanceProjector.Model;
using FinanceProjector.Models.Authentication;
using FinanceProjector.Repository;
using Microsoft.SqlServer.Server;
using MongoDB.Driver;
using MongoDB.Driver.Linq;


namespace FinanceProjector.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private MongoRepository<User> _repo;

        public AuthenticationService()
        {
            _repo = new MongoRepository<User>();
        }

        public AuthenticationResponse AuthenticateUser(AuthenticationRequest request)
        {
            var response = new AuthenticationResponse();

            try
            {
                if (!AuthenticationRequest.IsValid(request))
                {
                    response.SetStatus(ResponseStatus.GeneralError, "Request is null or invalid.");
                }
                else
                {
                    var user = GetUser(request.UserName);

                    if (user != null)
                    {
                        if (user.Password == request.Password)
                        {
                            response.User = user;
                        }
                        else
                        {
                            response.SetStatus(ResponseStatus.InvalidPassword);
                        }
                    }
                    else
                    {
                        response.SetStatus(ResponseStatus.InvalidUserName);
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                response.SetStatus(ResponseStatus.Exception, ex.Message);
            }


            return response;
        }

        public SaveUserResponse SaveUser(SaveUserRequest request)
        {
            var response = new SaveUserResponse();
            var user = _repo.FirstOrDefault(u => u.UserName == request.User.UserName);
            
            if (user != null)
            {
                user.FirstName = request.User.FirstName;
                user.LastName = request.User.LastName;
                user.UserName = request.User.UserName;
                user.Password = request.User.Password;
                user.PasswordSecurityQuestion = request.User.PasswordSecurityQuestion;
                user.PasswordSecurityAnswer = request.User.PasswordSecurityAnswer;
            }
            else
            {
                user = request.User;
            }

            _repo.Save(user);

            return response;
        }

        private User GetUser(string userName)
        {
            return _repo.FirstOrDefault(u => u.UserName == userName);
        }

        //private MongoDatabase GetDatabase()
        //{
        //    var connectionString = "mongodb://localhost";
        //    var client = new MongoClient(connectionString);
        //    var server = client.GetServer();
        //    return server.GetDatabase("FinanceProjector");
        //}
    }
}
