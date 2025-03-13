using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Net.Mime.MediaTypeNames;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MarkersMarktApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow(bool isCreated)
        {
            this.InitializeComponent();

            //if(!isCreated)
            //{
            //    using (var db = new AppDbContext())
            //    {
            //        db.Database.EnsureDeleted();
            //        db.Database.EnsureCreated();
            //    }
            //}

            //if (isCreated == true)
            //{
            //    accountCreated.Visibility = Visibility.Visible;
            //}
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //var mail = emailTextBox.Text;
            //var password = passPasswordBox.Password;
            //if ( mail.Count() == 0 && password.Count() == 0 )
            //{
            //    fillError.Text = "Fill in every field!";
            //    fillError.Visibility = Visibility.Visible;
            //    emailError.Visibility = Visibility.Collapsed;
            //    passError.Visibility = Visibility.Collapsed;

            //}
            //else if (mail.Count() == 0)
            //{
            //    fillError.Visibility = Visibility.Collapsed;
            //    emailError.Visibility = Visibility.Visible;
            //    passError.Visibility = Visibility.Collapsed;
            //}
            //else if (password.Count() == 0)
            //{
            //    fillError.Visibility = Visibility.Collapsed;
            //    emailError.Visibility = Visibility.Collapsed;
            //    passError.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    fillError.Text = "User not found!";
            //    fillError.Visibility = Visibility.Collapsed;
            //    emailError.Visibility = Visibility.Collapsed;
            //    passError.Visibility = Visibility.Collapsed;

            //    using (var db = new AppDbContext())
            //    {
            //        var user = db.Users.Where(u => u.Email.ToLower().Equals(mail.ToLower()) && u.Password.Equals(password)).FirstOrDefault();
            //        if (user == null)
            //        {
            //            fillError.Visibility = Visibility.Visible;
            //        }
            //        else
            //        {
            //            var itemWindow = new ItemWindow(user.Id);
            //            itemWindow.Activate();
            //            this.Close();
            //        }
            //    }
            //}
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            //var registerWindow = new RegisterWindow();
            //registerWindow.Activate();
            //this.Close();
        }
    }
}
