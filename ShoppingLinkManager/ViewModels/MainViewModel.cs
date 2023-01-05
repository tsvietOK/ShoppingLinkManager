using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingLinkManager.Contracts.Services;
using ShoppingLinkManager.Contracts.ViewModels;

namespace ShoppingLinkManager.ViewModels;

public class MainViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService navigationService;

    private bool isAddButtonEnabled = true;
    private bool isRenameButtonEnabled;
    private bool isDeleteButtonEnabled;
    private string selectedItem;
    private int selectedItemIndex;

    public MainViewModel(INavigationService navigationService)
    {
        this.navigationService = navigationService;

        AddLinkListItemCommand = new RelayCommand(AddLinkListItem);
        DeleteLinkListItemCommand = new RelayCommand(DeleteLinkListItem);

        LinkLists = new ObservableCollection<string>();
    }

    public ObservableCollection<string> LinkLists
    {
        get; set;
    }

    public string SelectedItem
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

    public RelayCommand AddLinkListItemCommand
    {
        get; set;
    }

    public RelayCommand DeleteLinkListItemCommand
    {
        get; set;
    }

    public void OnNavigatedFrom()
    {
        
    }

    public void OnNavigatedTo(object parameter)
    {
        
    }

    private void AddLinkListItem()
    {
        var count = LinkLists.Count;
        count++;
        LinkLists.Add(count.ToString());
    }

    private void DeleteLinkListItem()
    {
        LinkLists.RemoveAt(SelectedItemIndex);
    }

}
