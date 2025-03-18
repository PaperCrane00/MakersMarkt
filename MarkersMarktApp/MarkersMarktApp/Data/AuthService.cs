using MakersMarktApp.Data;
using System.Linq;
    

public static class AuthService
{
    public static User CurrentUser { get; set; }

    public static User Login(string email, string password)
    {
        using (var db = new AppDbContext())
        {
            var user = db.Users
                .FirstOrDefault(u => u.Email.ToLower() == email.ToLower() && u.Password == password);

            if (user != null)
            {
                CurrentUser = user;
                return user;
            }
        }
        return null;
    }

    public static int? GetCurrentUserId()
    {
        return CurrentUser?.Id;
    }

    public static void Logout()
    {
        CurrentUser = null;
    }
}
