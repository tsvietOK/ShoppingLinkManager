using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using ShoppingLinkManager.Contracts.Services;
using ShoppingLinkManager.Helpers;
using ShoppingLinkManager.Views;

namespace ShoppingLinkManager.ViewModels;

public class ShellViewModel : ObservableRecipient
{
    private bool _isBackEnabled;
    private object? _selected;

    public INavigationService NavigationService
    {
        get;
    }

    public INavigationViewService NavigationViewService
    {
        get;
    }

    public bool IsBackEnabled
    {
        get => _isBackEnabled;
        set => SetProperty(ref _isBackEnabled, value);
    }

    public object? Selected
    {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }

    public ObservableCollection<NavigationViewItemBase> NavItems
    {
        get; set;
    }

    public NavigationViewItem MainNavigationViewItem
    {
        get; set;
    }

    public ObservableCollection<ShoppingListItem> ShoppingListItems
    {
        get; set;
    }

    public ShellViewModel(INavigationService navigationService, INavigationViewService navigationViewService)
    {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;

        NavItems = new();
        //var testNav = new NavigationViewItem()
        //{
        //    Content = "1",

        //};
        //NavigationHelper.SetNavigateTo(testNav, typeof(ContentGridViewModel).FullName);

        MainNavigationViewItem = new NavigationViewItem();
        MainNavigationViewItem.Content = "Shell_Main_Name".GetLocalized();
        MainNavigationViewItem.Icon = new SymbolIcon(Symbol.Home);
        NavigationHelper.SetNavigateTo(MainNavigationViewItem, typeof(MainViewModel).FullName);


        ShoppingListItems = new();
        foreach (var item in MenuItems())
        {
            NavItems.Add(item);
        }
        ShoppingListItems.CollectionChanged += Items_CollectionChanged;
    }

    private void Items_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        //NavItems.Clear();
        foreach (var item in e.NewItems)
        {
            var newShoppingItem = (ShoppingListItem)item;
            var navItem = new NavigationViewItem()
            {
                Content = newShoppingItem.Name,
                Icon = new SymbolIcon(Symbol.ViewAll),
                Tag = newShoppingItem.Tag
            };
            NavigationHelper.SetNavigateTo(navItem, typeof(ContentGridViewModel).FullName);
            NavItems.Add(navItem);
        }


        //foreach (var item in MenuItems())
        //{
        //    NavItems.Add(item);
        //}
        //ShoppingListItems.Add(new ShoppingListItem(typeof(ContentGridViewModel), "2"));
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        IsBackEnabled = NavigationService.CanGoBack;

        if (e.SourcePageType == typeof(SettingsPage))
        {
            Selected = NavigationViewService.SettingsItem;
            return;
        }

        var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem != null)
        {
            Selected = selectedItem;
        }
    }

    private IEnumerable<NavigationViewItemBase> MenuItems()
    {
        yield return MainNavigationViewItem;
        yield return new NavigationViewItemHeader { Content = "Shopping lists" };
        foreach (var another in ShoppingListItems)
        {
            var navItem = new NavigationViewItem()
            {
                Content = another.Name,
                Icon = new SymbolIcon(another.Symbol),
            };
            NavigationHelper.SetNavigateTo(navItem, typeof(ContentGridViewModel).FullName);

            yield return navItem;
        }
    }
}

public sealed class ShoppingListItem
{
    public ShoppingListItem(Type pageType, string? name = null, string? tags = null, Symbol symbol = Symbol.ViewAll)
    {
        //Item = viewItem;
        PageType = pageType;
        Name = name;
        Tag = tags;
        Symbol = symbol;
    }

    /// <summary>
    /// The navigation item for the current entry.
    /// </summary>
    //public NavigationViewItem Item
    //{
    //    get;
    //}

    /// <summary>
    /// The associated page type for the current entry.
    /// </summary>
    public Type PageType
    {
        get;
    }

    /// <summary>
    /// Gets the name of the current entry.
    /// </summary>
    public string? Name
    {
        get;
    }

    public Symbol Symbol
    {
        get;
    }

    /// <summary>
    /// Gets the tag for the current entry, if any.
    /// </summary>
    public string? Tag
    {
        get;
    }
}