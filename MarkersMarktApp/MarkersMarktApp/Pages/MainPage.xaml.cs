using MakersMarktApp.Data;
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
    }
}
