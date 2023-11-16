using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookmarkPair : MonoBehaviour
{
    [SerializeField] private BookmarkManager bookmarkManager;
    [SerializeField] private Bookmark leftBookmark;
    [SerializeField] private Bookmark rightBookmark;
    [SerializeField] private Color color;
    [SerializeField] private Bandmate category;
    [SerializeField] private int index;
    public Bandmate GetCategory() { return category; }

    private void Start()
    {
        leftBookmark.Initialize(color, category, false);
        rightBookmark.Initialize(color, category, true);
    }

    public void BookmarkSelected()
    {
        bookmarkManager.SelectBookmark(index);
    }
    // called by bookmark manager on selected bookmark after resetting bookmarks
    public void Select()
    {
        rightBookmark.Open();
    }
    // called by bookmark manager when resetting bookmarks
    public void Unselect()
    {
        rightBookmark.Close();
    }

    public void FlipLeft()
    {
        leftBookmark.Show(true);
        rightBookmark.Show(false);
    }

    public void ResetFlip()
    {
        leftBookmark.Show(false);
        rightBookmark.Show(true);
    }
}
