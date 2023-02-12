using CommunityToolkit.Mvvm.ComponentModel;

namespace ShoppingLinkManager.Core.Models;

public class ShoppingList : ObservableObject
{
    private string name;

    public ShoppingList(string name)
    {
        Guid = Guid.NewGuid();
        Name = name;
    }

    public Guid Guid
    {
        get; set;
    }

    public string Name
    {
        get => name;
        set => SetProperty(ref name, value);
    }

    public string Description
    {
        get; set;
    }

    public List<string> Items
    {
        get; set;
    }
}
