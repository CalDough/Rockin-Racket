using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookmarkManager : MonoBehaviour
{
    [SerializeField] private CatalogManager catalogManager;
    [SerializeField] private BookmarkPair[] bookmarkPairs;
    private int selectedIndex = 0;

    public void SelectBookmark(int index)
    {
        BookmarkPair bookmarkPair = bookmarkPairs[selectedIndex];
        bookmarkPair.Unselect();
        selectedIndex = index;
        FlipBookmarks(index);
        catalogManager.DisplayItemsByCategory(bookmarkPair.GetCategory());
    }

    public void FlipBookmarks(int index)
    {
        foreach (BookmarkPair bookmarkPair in bookmarkPairs)
            bookmarkPair.ResetFlip();
        for (int i = 0; i < index; i++)
            bookmarkPairs[i].FlipLeft();
    }
}