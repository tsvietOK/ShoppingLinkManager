using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingLinkManager.Contracts.Services;
using ShoppingLinkManager.Contracts.ViewModels;
using ShoppingLinkManager.Core.Models;

namespace ShoppingLinkManager.ViewModels;

public partial class MainViewModel : ObservableRecipient, INavigationAware
{
    private readonly IShoppingListService shoppingListService;

    [ObservableProperty] private bool isAddButtonEnabled = true;
    [ObservableProperty] private bool isRenameButtonEnabled;
    [ObservableProperty] private bool isDeleteButtonEnabled;
    [ObservableProperty] private ShoppingList selectedItem;
    [ObservableProperty] private int selectedItemIndex;
    [ObservableProperty] private bool isListNameValid = true;
    private string newListName;

    public MainViewModel(IShoppingListService shoppingListService)
    {
        this.shoppingListService = shoppingListService;

        ShoppingLists = new ObservableCollection<ShoppingList>();
    }

    public ObservableCollection<ShoppingList> ShoppingLists
    {
        get; set;
    }

    public string NewListName
    {
        get => newListName;
        set
        {
            if (SetProperty(ref newListName, value))
            {
                IsListNameValid = ShoppingLists.Any(x => x.Name == value) || string.IsNullOrWhiteSpace(value);
            }
        }
    }

    partial void OnSelectedItemChanged(ShoppingList value)
    {
        if (value == null)
        {
            IsDeleteButtonEnabled = false;
            IsRenameButtonEnabled = false;
        }
        else
        {
            IsDeleteButtonEnabled = true;
            IsRenameButtonEnabled = true;
        }
    }

    public void OnNavigatedFrom()
    {
        ShoppingLists.CollectionChanged -= ShoppingLists_CollectionChanged;
    }

    public async void OnNavigatedTo(object parameter)
    {
        ShoppingLists.Clear();

        var data = await shoppingListService.GetShoppingListsAsync();
        foreach (var item in data)
        {
            ShoppingLists.Add(item);
        }

        ShoppingLists.CollectionChanged += ShoppingLists_CollectionChanged;
    }

    private void ShoppingLists_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        shoppingListService.SaveShoppingListsAsync(ShoppingLists);
    }

    [RelayCommand]
    private void AddLinkListItem()
    {
        //var count = LinkLists.Count;
        //count++;
        ShoppingLists.Add(new ShoppingList(NewListName.Trim()));
    }

    [RelayCommand]
    private void RenameLinkListItem()
    {
        SelectedItem.Name = NewListName;

        shoppingListService.SaveShoppingListsAsync(ShoppingLists);
    }

    [RelayCommand]
    private void DeleteLinkListItem()
    {
        ShoppingLists.RemoveAt(SelectedItemIndex);
    }

}
