using MakersMarktApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MakersMarktApp.Services
{
    public static class AuthService
    {
        public static User CurrentUser { get; private set; }

        public static User Login(string username, string password)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users
                    .Include(u => u.Roles)
                    .Include(u => u.Sales)
                    .Include(u => u.Notifications)
                    .Include(u => u.Product_Reviews)
                    .Include(u => u.Products)
                    .Include(u => u.Seller_Reviews)
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
