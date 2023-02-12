using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ShoppingLinkManager.Contracts.Services;
using ShoppingLinkManager.Contracts.ViewModels;
using ShoppingLinkManager.Core.Contracts.Services;
using ShoppingLinkManager.Core.Models;
using ShoppingLinkManager.Core.Services;

namespace ShoppingLinkManager.ViewModels;

public class MainViewModel : ObservableRecipient, INavigationAware
{
    private readonly IShoppingListService shoppingListService;

    private bool isAddButtonEnabled = true;
    private bool isRenameButtonEnabled;
    private bool isDeleteButtonEnabled;
    private ShoppingList selectedItem;
    private int selectedItemIndex;
    private InfoBarSeverity infoBarSeverity;
    private bool isListNameValid = true;
    private string newListName;

    public MainViewModel(IShoppingListService shoppingListService)
    {
        this.shoppingListService = shoppingListService;

        AddLinkListItemCommand = new RelayCommand(AddLinkListItem);
        RenameLinkListItemCommand = new RelayCommand(RenameLinkListItem);
        DeleteLinkListItemCommand = new RelayCommand(DeleteLinkListItem);

        ShoppingLists = new ObservableCollection<ShoppingList>();
    }

    public ObservableCollection<ShoppingList> ShoppingLists
    {
        get; set;
    }

    public ShoppingList SelectedItem
    {
        get => selectedItem;
        set
        {
            SetProperty(ref selectedItem, value);
            if (selectedItem == null)
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
    }

    public int SelectedItemIndex
    {
        get => selectedItemIndex;
        set => SetProperty(ref selectedItemIndex, value);
    }

    public bool IsAddButtonEnabled
    {
        get => isAddButtonEnabled;
        set => SetProperty(ref isAddButtonEnabled, value);
    }

    public bool IsRenameButtonEnabled
    {
        get => isRenameButtonEnabled;
        set => SetProperty(ref isRenameButtonEnabled, value);
    }

    public bool IsDeleteButtonEnabled
    {
        get => isDeleteButtonEnabled;
        set => SetProperty(ref isDeleteButtonEnabled, value);
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

    public bool IsListNameValid
    {
        get => isListNameValid;
        set => SetProperty(ref isListNameValid, value);
    }

    public InfoBarSeverity InfoBarSeverity
    {
        get => infoBarSeverity;
        set => SetProperty(ref infoBarSeverity, value);
    }

    public RelayCommand AddLinkListItemCommand
    {
        get; set;
    }

    public RelayCommand RenameLinkListItemCommand
    {
        get; set;
    }

    public RelayCommand DeleteLinkListItemCommand
    {
        get; set;
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

    private void AddLinkListItem()
    {
        //var count = LinkLists.Count;
        //count++;
        ShoppingLists.Add(new ShoppingList(NewListName.Trim()));
        NewListName = string.Empty;
    }

    private void RenameLinkListItem()
    {
        SelectedItem.Name = NewListName;
        NewListName = string.Empty;

        shoppingListService.SaveShoppingListsAsync(ShoppingLists);
    }

    private void DeleteLinkListItem()
    {
        ShoppingLists.RemoveAt(SelectedItemIndex);
    }

}
