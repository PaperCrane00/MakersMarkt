using DreamScape.Data;
using DreamScape.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DreamScape
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            this.InitializeComponent();
            Fullscreen fullscreen = new Fullscreen();
            fullscreen.SetFullscreen(this);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new MainWindow(false);
            loginWindow.Activate();
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var username = usernameTextBox.Text;
            var mail = emailTextBox.Text;
            var password = passPasswordBox.Password;
            var repeatPass = passRepeatPasswordBox.Password;

            if (username.Count() == 0 && mail.Count() == 0 && password.Count() == 0 && repeatPass.Count() == 0 )
            {
                fillError.Text = "Fill in every field!";
                fillError.Visibility = Visibility.Visible;
                usernameError.Visibility = Visibility.Collapsed;
                emailError.Visibility = Visibility.Collapsed;
                passError.Visibility = Visibility.Collapsed;
                passRepeatError.Visibility = Visibility.Collapsed;

            }
            else
            {
                using (var db = new AppDbContext())
                {
                    fillError.Visibility = Visibility.Collapsed;
                    usernameError.Visibility = Visibility.Collapsed;
                    emailError.Visibility = Visibility.Collapsed;
                    passError.Visibility = Visibility.Collapsed;
                    passRepeatError.Visibility = Visibility.Collapsed;
                    if (username.Count() == 0)
                    {
                        usernameError.Visibility = Visibility.Visible;
                    }
                    if (mail.Count() == 0)
                    {
                        emailError.Text = "Fill in an E-mail!";
                        emailError.Visibility = Visibility.Visible;
                    }
                    if (!IsValidEmail(mail) && mail.Count() != 0)
                    {
                        emailError.Text = "Fill in a correct E-mail!";
                        emailError.Visibility = Visibility.Visible;
                    }
                    if (db.Users.Where(e => mail.ToLower() == e.Email.ToLower()).Count() != 0)
                    {
                        emailError.Text = "E-mail already in use!";
                        emailError.Visibility = Visibility.Visible;
                    }
                    if (password.Count() == 0)
                    {
                        passError.Visibility = Visibility.Visible;
                    }
                    if (repeatPass.Count() == 0)
                    {
                        passRepeatError.Text = "Fill in your password again!";
                        passRepeatError.Visibility = Visibility.Visible;
                    }
                    if (password.Count() != 0 && repeatPass.Count() != 0 && !password.Equals(repeatPass))
                    {
                        passRepeatError.Text = "Passwords are not the same!";
                        passRepeatError.Visibility = Visibility.Visible;
                    }
                    if (username.Count() != 0 && mail.Count() != 0 && password.Count() != 0 && repeatPass.Count() != 0 && db.Users.Where(e => mail.ToLower() == e.Email.ToLower()).Count() == 0 && password.Equals(repeatPass) && IsValidEmail(mail))
                    {
                        fillError.Text = "Everything checks out!";
                        fillError.Visibility = Visibility.Visible;
                        usernameError.Visibility = Visibility.Collapsed;
                        emailError.Visibility = Visibility.Collapsed;
                        passError.Visibility = Visibility.Collapsed;
                        passRepeatError.Visibility = Visibility.Collapsed;
                        User user = new User();
                        user.Username = username;
                        user.Email = mail;
                        user.Password = password;
                        user.Admin = false;
                        db.Users.Add(user);
                        db.SaveChanges();
                        var login = new MainWindow(true);
                        login.Activate();
                        this.Close();
                    }
                }
            }
        }
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
