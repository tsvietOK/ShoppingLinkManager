using Microsoft.UI.Xaml.Controls;

using ShoppingLinkManager.ViewModels;

namespace ShoppingLinkManager.Views;

public sealed partial class ContentGridPage : Page
{
    public ContentGridViewModel ViewModel
    {
        get;
    }

    public ContentGridPage()
    {
        ViewModel = App.GetService<ContentGridViewModel>();
        InitializeComponent();
    }
}
