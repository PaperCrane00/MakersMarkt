using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MakersMarktApp.Data;
using MakersMarktApp;
using Windows.System;
using MakersMarktApp.Pages;
using MakersMarktApp.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MarkersMarktApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var email = emailTextBox.Text;
            var password = passPasswordBox.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                // Toon foutmelding als velden leeg zijn
                fillError.Text = "Fill in every field!";
                fillError.Visibility = Visibility.Visible;
                emailError.Visibility = Visibility.Collapsed;
                passError.Visibility = Visibility.Collapsed;
            }
            else
            {
                var user = AuthService.Login(email, password);

                if (user == null)
                {
                    // Foutmelding als de gebruiker niet bestaat
                    fillError.Text = "User not found!";
                    fillError.Visibility = Visibility.Visible;
                    emailError.Visibility = Visibility.Collapsed;
                    passError.Visibility = Visibility.Collapsed;
                }
                else
                {
                    // Gebruiker gevonden, ga naar MainPage
                    fillError.Visibility = Visibility.Collapsed;
                    Frame.Navigate(typeof(MainPage));
                }
            }
        }


        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterPage));
        }
    }
}
