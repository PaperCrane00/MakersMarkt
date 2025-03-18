using MakersMarktApp.Data;
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
using Windows.System;
using MarkersMarktApp.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MakersMarktApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow(bool isCreated)
        {
            this.InitializeComponent();

            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
            NavigationService.Initialize(MainFrame); 
            NavigationService.NavigateTo(typeof(LoginPage));

        }
    }
}
