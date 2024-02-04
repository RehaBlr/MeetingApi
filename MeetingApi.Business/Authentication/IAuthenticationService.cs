using MeetingApi.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApi.Business.Authentication
{
    public interface IAuthenticationService : IBaseService
    {
        string GenerateToken(User user);
        User Login(string username, string password);
        User Register(User user);
    }
}
