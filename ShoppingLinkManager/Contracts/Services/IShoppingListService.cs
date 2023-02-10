using ShoppingLinkManager.Core.Models;

namespace ShoppingLinkManager.Contracts.Services;

public interface IShoppingListService
{
    Task<IEnumerable<ShoppingList>> GetShoppingListsAsync();
    Task SaveShoppingListsAsync(IEnumerable<ShoppingList> shoppingLists);
}
