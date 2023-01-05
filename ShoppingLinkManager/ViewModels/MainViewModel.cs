using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ShoppingLinkManager.ViewModels;

public class MainViewModel : ObservableRecipient
{
    public MainViewModel()
    {
        LinkLists = new ObservableCollection<string>();
        LinkLists.Add("1");
        LinkLists.Add("2");
        LinkLists.Add("3");
        LinkLists.Add("4");
    }

    public ObservableCollection<string> LinkLists
    {
        get; set;
    }
}
