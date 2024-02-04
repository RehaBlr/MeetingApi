using MeetingApi.Domain.Entity;
using MeetingApi.Domain.Helper;
using MeetingApi.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApi.Business.Authentication
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly MeetingDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationService(MeetingDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public User Login(string name, string password)
        {

            var user = _context.Users.FirstOrDefault(x => x.Name == name);
            if (user == null)
                return null;

            var verifyPassword = Utils.CompareHashPassword(password, user.Password);
            if (!verifyPassword)
                return null;

            return user;
        }

        public string GenerateToken(User user)
        {
            var secretKey = _configuration.GetValue<string>("SecretKey");

            byte[] secretKeyByteArray = Encoding.UTF8.GetBytes(secretKey);

            var securityKey = new SymmetricSecurityKey(secretKeyByteArray);
            var credientials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {


                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                }),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = credientials
            };
            var securityToken = tokenHandler.CreateToken(token);
            var tokenString = tokenHandler.WriteToken(securityToken);
            return tokenString;
        }

        public User Register(User user)
        {
            var hashedPassword = Utils.HashPassword(user.Password);
            user.Password = hashedPassword;
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
