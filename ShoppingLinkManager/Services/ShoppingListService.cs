using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingLinkManager.Contracts.Services;
using ShoppingLinkManager.Core.Contracts.Services;
using ShoppingLinkManager.Core.Models;
using ShoppingLinkManager.Helpers;
using Windows.Storage;

namespace ShoppingLinkManager.Services;
public class ShoppingListService : IShoppingListService
{
    private readonly IFileService fileService;
    private static readonly string ShoppingListsFileName = "ShoppingLists";

    public ShoppingListService(IFileService fileService)
    {
        this.fileService = fileService;
    }

    public async Task<IEnumerable<ShoppingList>> GetShoppingListsAsync()
    {
        var result = await ApplicationData.Current.LocalFolder.ReadAsync<IEnumerable<ShoppingList>>(ShoppingListsFileName);
        if (result == null)
        {
            return Enumerable.Empty<ShoppingList>();
        }

        return result;
    }

    public async Task SaveShoppingListsAsync(IEnumerable<ShoppingList> shoppingLists)
    {
        await ApplicationData.Current.LocalFolder.SaveAsync(ShoppingListsFileName, shoppingLists);
    }
}
