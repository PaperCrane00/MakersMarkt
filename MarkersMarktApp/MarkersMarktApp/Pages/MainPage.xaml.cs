using MakersMarktApp.Data;
using MakersMarktApp.Services;
using MarkersMarktApp.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakersMarktApp.Pages
{
    public sealed partial class MainPage : Page
    {
        private List<Product> allUserProducts = new List<Product>();

        public MainPage()
        {
            this.InitializeComponent();
            LoadUserProducts();
            UserItemsListView.ItemsSource = allUserProducts; 
        }

        private void LoadUserProducts()
        {
            int? userId = AuthService.GetCurrentUserId();

            if (userId.HasValue)
            {
                using (var db = new AppDbContext())
                {
                    allUserProducts = db.Products
                                         .Where(ui => ui.UserId == userId.Value)
                                         .ToList();
                }
            }
        }

        private async void UserProductsListView_ProductClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (Product)e.ClickedItem;

            ContentDialog itemDetailsDialog = new ContentDialog()
            {
                Title = clickedItem.Name,
                Content = $"{clickedItem.Description}\n" +
                          $"Price: ${clickedItem.Price}\n",
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };
            await itemDetailsDialog.ShowAsync();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            AuthService.Logout();
            ShowMessage("You have been logged out!");
            Frame.Navigate(typeof(LoginPage));
        }

        private void ShowMessage(string message)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Information",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };
            _ = dialog.ShowAsync();
        }

        private async void UserProductsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (Product)e.ClickedItem;

            ContentDialog itemDetailsDialog = new ContentDialog()
            {
                Title = clickedItem.Name,
                Content = $"Beschrijving: {clickedItem.Description}\n" +
                          $"Prijs: {clickedItem.Price}\n",
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };
            await itemDetailsDialog.ShowAsync();
        }

        private async void ViewProfile_Click(object sender, RoutedEventArgs e)
        {
            int? userId = AuthService.GetCurrentUserId();

            if (userId == null)
            {
                NavigationService.NavigateTo(typeof(LoginPage));
                return;
            }

            using (var db = new AppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == userId);

                if (user == null)
                {
                    ShowMessage("Gebruiker niet gevonden.");
                    return;
                }

                var usernameTextBox = new TextBox { Text = user.Username, PlaceholderText = "Gebruikersnaam" };
                var emailTextBox = new TextBox { Text = user.Email, PlaceholderText = "E-mail" };
                var passwordBox = new PasswordBox { PlaceholderText = "Nieuw Wachtwoord (optioneel)" };
                var confirmPasswordBox = new PasswordBox { PlaceholderText = "Herhaal Wachtwoord" };

                var dialog = new ContentDialog
                {
                    Title = "Mijn Profiel",
                    PrimaryButtonText = "Terug",
                    SecondaryButtonText = "Bewerken",
                    Content = new StackPanel
                    {
                        Children = { usernameTextBox, emailTextBox, passwordBox, confirmPasswordBox }
                    },
                    XamlRoot = this.XamlRoot
                };

                var result = await dialog.ShowAsync();

                if (result == ContentDialogResult.Secondary)
                {
                    string newUsername = usernameTextBox.Text.Trim();
                    string newEmail = emailTextBox.Text.Trim();
                    string newPassword = passwordBox.Password;
                    string confirmPassword = confirmPasswordBox.Password;

                    if (string.IsNullOrEmpty(newUsername) || string.IsNullOrEmpty(newEmail))
                    {
                        ShowMessage("Alle velden moeten ingevuld zijn.");
                        return;
                    }

                    if (!ValidationService.IsValidEmail(newEmail))
                    {
                        ShowMessage("Voer een geldig e-mailadres in.");
                        return;
                    }

                    if (ValidationService.IsUsernameTaken(newUsername, userId))
                    {
                        ShowMessage($"De gebruikersnaam '{newUsername}' is al in gebruik. Kies een andere gebruikersnaam.");
                        return;
                    }

                    if (!string.IsNullOrWhiteSpace(newPassword) || !string.IsNullOrWhiteSpace(confirmPassword))
                    {
                        if (!ValidationService.ArePasswordsMatching(newPassword, confirmPassword))
                        {
                            ShowMessage("De wachtwoorden komen niet overeen.");
                            return;
                        }

                        if (!ValidationService.IsValidPassword(newPassword))
                        {
                            ShowMessage("Het wachtwoord moet minimaal 8 tekens lang zijn en een combinatie van letters en cijfers bevatten.");
                            return;
                        }

                        user.Password = newPassword;
                    }
                    user.Username = newUsername;
                    user.Email = newEmail;
                    user.Password = newPassword;

                    db.SaveChanges();

                    ShowMessage("Profiel succesvol bijgewerkt!");
                }
            }
        }
    }
}
