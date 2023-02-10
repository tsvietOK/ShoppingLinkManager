using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLinkManager.Core.Models;

public class ShoppingList
{
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
        get; set;
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
