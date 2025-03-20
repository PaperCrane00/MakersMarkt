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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MarkersMarktApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminPage : Page
    {
        public AdminPage()
        {
            this.InitializeComponent();
        }

        private void UserButtonAdmin_Click(object sender, RoutedEventArgs e)
        {
            UserListViewAdmin.Visibility = Visibility.Visible;
            ProductListViewAdmin.Visibility = Visibility.Collapsed;
            UserScrollViewAdmin.Visibility = Visibility.Collapsed;
            ProductScrollViewAdmin.Visibility = Visibility.Collapsed;

            LoadUserListView();
        }

        private void ProductButtonAdmin_Click(object sender, RoutedEventArgs e)
        {
            UserListViewAdmin.Visibility = Visibility.Collapsed;
            ProductListViewAdmin.Visibility = Visibility.Visible;
            UserScrollViewAdmin.Visibility = Visibility.Collapsed;
            ProductScrollViewAdmin.Visibility = Visibility.Collapsed;

            LoadProductListView();
        }

        private void UserListViewAdmin_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedUser = UserListViewAdmin.SelectedItem as User;

            if (selectedUser == null)
            {
                return;
            }

            using (var db = new AppDbContext())
            {
                var user = db.Users.Where(u => u.Id == selectedUser.Id).FirstOrDefault();

                if (user == null)
                {
                    return;
                }

                UserIdUserUpdateAdminTextBlock.Text = user.Id.ToString();
                UserNameUserUpdateAdminTextBlock.Text = user.Username;
                UserEmailUserUpdateAdminTextBlock.Text = user.Email;
                UserCreditUserUpdateAdminTextBox.Text = user.Credit.ToString();

                if (user.Is_Verified ==  true)
                {
                    UserVarifiedUserUpdateAdminCkeckbox.IsChecked = true;
                }
                else
                {
                    UserVarifiedUserUpdateAdminCkeckbox.IsChecked = false;
                }
            }

            LoadUserListView();

            UserListViewAdmin.Visibility = Visibility.Visible;
            ProductListViewAdmin.Visibility = Visibility.Collapsed;
            UserScrollViewAdmin.Visibility = Visibility.Visible;
            ProductScrollViewAdmin.Visibility = Visibility.Collapsed;
        }

        private void ProductListViewAdmin_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedProduct = ProductListViewAdmin.SelectedItem as Product;

            if (selectedProduct == null)
            {
                return;
            }

            using (var db = new AppDbContext())
            {
                var product = db.Products.Where(p => p.Id == selectedProduct.Id).FirstOrDefault();

                if (product == null)
                {
                    return;
                }

                ProductIdProductUpdateAdminTextBlock.Text = product.Id.ToString();
                ProductNameProductUpdateAdminTextBlock.Text = product.Name;
                ProductDescriptionProductUpdateAdminTextBlock.Text = product.Description;
                ProductPriceProductUpdateAdminTextBox.Text = product.Price.ToString();

                if (product.IsVerified == true)
                {
                    ProductVarifiedProductUpdateAdminCkeckbox.IsChecked = true;
                }
                else
                {
                    ProductVarifiedProductUpdateAdminCkeckbox.IsChecked = false;
                }

                if (product.Moderation == true)
                {
                    ProductModerationProductUpdateAdminCkeckbox.IsChecked = true;
                }
                else
                {
                    ProductModerationProductUpdateAdminCkeckbox.IsChecked = false;
                }
            }

            LoadProductListView();

            UserListViewAdmin.Visibility = Visibility.Collapsed;
            ProductListViewAdmin.Visibility = Visibility.Visible;
            UserScrollViewAdmin.Visibility = Visibility.Collapsed;
            ProductScrollViewAdmin.Visibility = Visibility.Visible;
        }

        private void UpdateUserButtonAdmin_Click(object sender, RoutedEventArgs e)
        {
            int credit;
            if (string.IsNullOrEmpty(UserNameUserUpdateAdminTextBlock.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(UserEmailUserUpdateAdminTextBlock.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(UserCreditUserUpdateAdminTextBox.Text))
            {
                return;
            }
            if (!int.TryParse(UserCreditUserUpdateAdminTextBox.Text, out credit))
            {
                return;
            }

            using (var db = new AppDbContext())
            {
                var user = db.Users.Where(u => u.Id.ToString() == UserIdUserUpdateAdminTextBlock.Text).FirstOrDefault();

                if (user == null)
                {
                    return;
                }

                user.Username = UserNameUserUpdateAdminTextBlock.Text;
                user.Email = UserEmailUserUpdateAdminTextBlock.Text;
                user.Credit = credit;
                
                if (UserVarifiedUserUpdateAdminCkeckbox.IsChecked == true)
                {
                    user.Is_Verified = true;
                }
                else
                {
                    user.Is_Verified = false;
                }

                db.SaveChanges();
            }

            LoadUserListView();

            UserListViewAdmin.Visibility = Visibility.Visible;
            ProductListViewAdmin.Visibility = Visibility.Collapsed;
            UserScrollViewAdmin.Visibility = Visibility.Collapsed;
            ProductScrollViewAdmin.Visibility = Visibility.Collapsed;
        }

        private void UpdateProductButtonAdmin_Click(object sender, RoutedEventArgs e)
        {
            int price;
            if (string.IsNullOrEmpty(ProductNameProductUpdateAdminTextBlock.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(ProductDescriptionProductUpdateAdminTextBlock.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(ProductPriceProductUpdateAdminTextBox.Text))
            {
                return;
            }
            if (!int.TryParse(ProductPriceProductUpdateAdminTextBox.Text, out price))
            {
                return;
            }

            using (var db = new AppDbContext())
            {
                var product = db.Products.Where(p => p.Id.ToString() == ProductIdProductUpdateAdminTextBlock.Text).FirstOrDefault();

                if (product == null)
                {
                    return;
                }

                product.Name = ProductNameProductUpdateAdminTextBlock.Text;
                product.Description = ProductDescriptionProductUpdateAdminTextBlock.Text;
                product.Price = price;
                
                if (ProductVarifiedProductUpdateAdminCkeckbox.IsChecked == true)
                {
                    product.IsVerified = true;
                }
                else
                {
                    product.IsVerified = false;
                }

                if (ProductModerationProductUpdateAdminCkeckbox.IsChecked == true)
                {
                    product.Moderation = true;
                }
                else
                {
                    product.Moderation = false;
                }

                db.SaveChanges();
            }

            LoadProductListView();

            UserListViewAdmin.Visibility = Visibility.Collapsed;
            ProductListViewAdmin.Visibility = Visibility.Visible;
            UserScrollViewAdmin.Visibility = Visibility.Collapsed;
            ProductScrollViewAdmin.Visibility = Visibility.Collapsed;
        }

        private void DeleteUserButtonAdmin_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users.Where(u => u.Id.ToString() == UserIdUserUpdateAdminTextBlock.Text).FirstOrDefault();
                
                if (user == null)
                {
                    return;
                }

                db.Users.Remove(user);
                db.SaveChanges();
            }

            LoadUserListView();

            UserListViewAdmin.Visibility = Visibility.Visible;
            ProductListViewAdmin.Visibility = Visibility.Collapsed;
            UserScrollViewAdmin.Visibility = Visibility.Collapsed;
            ProductScrollViewAdmin.Visibility = Visibility.Collapsed;
        }

        private void DeleteProductButtonAdmin_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                var product = db.Products.Where(p => p.Id.ToString() == ProductIdProductUpdateAdminTextBlock.Text).FirstOrDefault();

                if (product == null)
                {
                    return;
                }

                db.Products.Remove(product);
                db.SaveChanges();
            }

            LoadProductListView();

            UserListViewAdmin.Visibility = Visibility.Collapsed;
            ProductListViewAdmin.Visibility = Visibility.Visible;
            UserScrollViewAdmin.Visibility = Visibility.Collapsed;
            ProductScrollViewAdmin.Visibility = Visibility.Collapsed;
        }

        private void LoadUserListView()
        {
            using (var db = new AppDbContext())
            {
                var users = db.Users.ToList();
                UserListViewAdmin.ItemsSource = users;
            }
        }

        private void LoadProductListView()
        {
            using (var db = new AppDbContext())
            {
                var products = db.Products.ToList();
                ProductListViewAdmin.ItemsSource = products;
            }
        }
    }
}
