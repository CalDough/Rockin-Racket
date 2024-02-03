using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookmarkManager : MonoBehaviour
{
    [SerializeField] private CatalogManager catalogManager;
    [SerializeField] private BookmarkPair[] bookmarkPairs;
    private int selectedIndex = 0;

    private void Start()
    {
        // show initial items
        SelectBookmark(selectedIndex);
    }

    public void SelectBookmark(int index)
    {
        bookmarkPairs[selectedIndex].Unselect();
        selectedIndex = index;
        FlipBookmarks(index);
        // pass info to Catalog Manager
        catalogManager.BookmarkPressed(bookmarkPairs[index].GetCategory());
    }

    public void FlipBookmarks(int index)
    {
        foreach (BookmarkPair bookmarkPair in bookmarkPairs)
            bookmarkPair.ResetFlip();
        for (int i = 0; i < index; i++)
            bookmarkPairs[i].FlipLeft();
        bookmarkPairs[index].Select();
    }
}