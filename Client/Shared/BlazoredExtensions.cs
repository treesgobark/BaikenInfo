using System.Diagnostics.CodeAnalysis;
using Blazored.LocalStorage;

namespace Client.Shared;

public static class BlazoredExtensions
{
    public static bool TryGetItem<T>(this ISyncLocalStorageService localStorageService, string key, [MaybeNullWhen(false)] out T item)
    {
        if (!localStorageService.ContainKey(key))
        {
            item = default;
            return false;
        }

        var fetchedItem = localStorageService.GetItem<T>(key);

        if (fetchedItem is null)
        {
            item = default;
            return false;
        }

        item = fetchedItem;
        return true;
    }
}