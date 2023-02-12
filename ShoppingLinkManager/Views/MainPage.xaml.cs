using Microsoft.UI.Xaml.Controls;

using ShoppingLinkManager.ViewModels;

namespace ShoppingLinkManager.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }

    private void AddLinkListButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        AddNewItemFlayout.Hide();
    }

    private void RenameLinkListButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        RenameItemFlayout.Hide();
    }
}
