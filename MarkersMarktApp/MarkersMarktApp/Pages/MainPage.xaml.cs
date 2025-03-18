using MakersMarktApp.Data;
using MakersMarktApp.Services;
using MarkersMarktApp.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

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
            User user = AuthService.CurrentUser;

            if (user == null)
            {
                NavigationService.NavigateTo(typeof(LoginPage));
                return;
            }

            using (var db = new AppDbContext())
            {
                allUserProducts = db.Products
                                    .Where(p => p.UserId == user.Id)
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

        private void TypeFilterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserItemsListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ViewProfile_Click(object sender, RoutedEventArgs e)
        {

        }

		private void ViewProducts_Click(object sender, RoutedEventArgs e)
		{
            Frame.Navigate(typeof(ProductsPage));
		}
	}
}