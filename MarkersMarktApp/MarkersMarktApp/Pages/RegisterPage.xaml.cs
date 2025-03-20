using System;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MakersMarktApp.Data;
using MakersMarktApp.Services;

namespace MarkersMarktApp.Pages
{
    public sealed partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            this.InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var username = usernameTextBox.Text;
            var mail = emailTextBox.Text;
            var password = passPasswordBox.Password;
            var repeatPass = passRepeatPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(mail) || 
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(repeatPass))
            {
                fillError.Text = "Fill in every field!";
                fillError.Visibility = Visibility.Visible;
                return;
            }

            using (var db = new AppDbContext())
            {
                fillError.Visibility = Visibility.Collapsed;
                usernameError.Visibility = Visibility.Collapsed;
                emailError.Visibility = Visibility.Collapsed;
                passError.Visibility = Visibility.Collapsed;
                passRepeatError.Visibility = Visibility.Collapsed;
                
                if (ValidationService.IsUsernameTaken(username))
                {
                    usernameError.Text = "Username already in use!";
                    usernameError.Visibility = Visibility.Visible;
                    return;
                }

                if (!ValidationService.IsValidEmail(mail))
                {
                    emailError.Text = "Fill in a correct E-mail!";
                    emailError.Visibility = Visibility.Visible;
                    return;
                }

                if (ValidationService.IsEmailTaken(mail))
                {
                    emailError.Text = "E-mail already in use!";
                    emailError.Visibility = Visibility.Visible;
                    return;
                }

                if (!ValidationService.IsValidPassword(password))
                {
                    passError.Text = "Password does not meet security requirements!";
                    passError.Visibility = Visibility.Visible;
                    return;
                }

                if (!ValidationService.ArePasswordsMatching(password, repeatPass))
                {
                    passRepeatError.Text = "Passwords are not the same!";
                    passRepeatError.Visibility = Visibility.Visible;
                    return;
                }

                var user = new User
                {
                    Username = username,
                    Email = mail,
                    Password = password,
                    Credit = 0,
                    Is_Verified = false
                };

                db.Users.Add(user);
                db.SaveChanges();
                Frame.Navigate(typeof(LoginPage));
            }
        }
    }
}
