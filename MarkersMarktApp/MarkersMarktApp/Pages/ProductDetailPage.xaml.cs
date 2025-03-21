using MakersMarktApp.Data;
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
using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MarkersMarktApp.Pages
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class ProductDetailPage : Page
	{
		private Product _selectedProduct;

		public ProductDetailPage()
		{
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			_selectedProduct = (Product)e.Parameter;

			ProductNameTextBlock.Text = _selectedProduct.Name;
			ProductDescriptionTextBlock.Text = _selectedProduct.Description;
			var bitmapImage = new BitmapImage(new Uri(_selectedProduct.ImageLink));
			ProductImage.Source = bitmapImage;
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.GoBack();
		}
	}
}
