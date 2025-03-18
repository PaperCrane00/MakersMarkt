using MakersMarktApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MakersMarktApp.Services
{
    public static class AuthService
    {
        public static User CurrentUser { get; set; }

        public static User Login(string username, string password)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users
                    .FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    CurrentUser = user;
                    return user;
                }
            }
            return null;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
