using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookmarkManager : MonoBehaviour
{
    [SerializeField] private CatalogManager catalogManager;
    private Bookmark selectedBookmark;

    public void SelectBookmark(Bookmark bookmark)
    {
        if (selectedBookmark != null)
        {
            selectedBookmark.Close();
        }
        bookmark.Open();
        selectedBookmark = bookmark;
    }
}
