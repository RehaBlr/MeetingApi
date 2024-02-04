using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApi.Domain.Helper
{
    public static class Utils
    {
        public static string HashPassword(string password)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }

        public static bool CompareHashPassword(string password, string hashedPassword)
        {
            var result = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            return result;
        }
    }
}
