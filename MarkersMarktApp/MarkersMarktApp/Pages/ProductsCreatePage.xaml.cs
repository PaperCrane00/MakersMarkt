using MakersMarktApp.Data;
using MakersMarktApp.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
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

namespace MarkersMarktApp.Pages
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class ProductsCreatePage : Page
	{
		public ProductsCreatePage()
		{
			this.InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			// Validate Input
			if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
				string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ||
				string.IsNullOrWhiteSpace(ImageLinkTextBox.Text) ||
				string.IsNullOrWhiteSpace(UserIdTextBox.Text) ||
				string.IsNullOrWhiteSpace(TypeIdTextBox.Text) ||
				string.IsNullOrWhiteSpace(SpecificIdTextBox.Text) ||
				string.IsNullOrWhiteSpace(PriceTextBox.Text))
			{
				ShowMessage("All fields must be filled.");
				return;
			}

			// Convert IDs and Price
			if (!int.TryParse(UserIdTextBox.Text, out int userId) ||
				!int.TryParse(TypeIdTextBox.Text, out int typeId) ||
				!int.TryParse(SpecificIdTextBox.Text, out int specificId) ||
				!int.TryParse(PriceTextBox.Text, out int price))
			{
				ShowMessage("User ID, Type ID, Specific ID, and Price must be numbers.");
				return;
			}

			// Create a new product
			var newProduct = new Product
			{
				Name = NameTextBox.Text,
				Description = DescriptionTextBox.Text,
				ImageLink = ImageLinkTextBox.Text,
				UserId = userId,
				TypeId = typeId,
				SpecificId = specificId,
				Price = price,
				IsVerified = IsVerifiedCheckBox.IsChecked ?? false,
				Moderation = ModerationCheckBox.IsChecked ?? false
			};

			using (var context = new AppDbContext())
			{
				context.Products.Add(newProduct);
				context.SaveChanges();
			}

			ShowMessage("Product created successfully!");
			Frame.Navigate(typeof(ProductsPage));
			
		}
		private async void ShowMessage(string message)
		{
			ContentDialog dialog = new ContentDialog
			{
				Title = "Info",
				Content = message,
				CloseButtonText = "OK",
				XamlRoot = this.XamlRoot
			};
			await dialog.ShowAsync();
		}
		private void ImageLinkTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				PreviewImage.Source = new BitmapImage(new Uri(ImageLinkTextBox.Text));
			}
			catch (Exception ex)
			{
				PreviewImage.Source = null;
				Console.WriteLine(ex.Message);
				return;
			}
		}
	}
}
