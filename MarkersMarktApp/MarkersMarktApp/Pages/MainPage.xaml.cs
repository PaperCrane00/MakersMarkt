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
        }

        private void LoadUserProducts()
        {
            int? userId = AuthService.CurrentUserId;

            if (userId == null)
            {
                NavigationService.NavigateTo(typeof(LoginPage));
                return;
            }

            using (var db = new AppDbContext())
            {
                allUserProducts = db.Products
                                    .Where(p => p.UserId == userId)
                                    .ToList();
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            AuthService.Logout();
            ShowMessage("U bent uitgelogd!");
            NavigationService.NavigateTo(typeof(LoginPage));
        }

        private void ShowMessage(string message)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Informatie",
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
    }
}
