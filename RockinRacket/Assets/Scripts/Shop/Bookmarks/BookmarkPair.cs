using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookmarkPair : MonoBehaviour
{
    [SerializeField] private BookmarkManager bookmarkManager;
    [SerializeField] private Bookmark leftBookmark;
    [SerializeField] private Bookmark rightBookmarkBack;
    [SerializeField] private Bookmark rightBookmarkFront;
    [SerializeField] private Color color;
    [SerializeField] private Bandmate category;
    [SerializeField] private int index;
    public Bandmate GetCategory() { return category; }

    private void Start()
    {
        leftBookmark.Initialize(color, category, false);
        rightBookmarkBack.Initialize(color, category, true);
        rightBookmarkFront.Initialize(color, category, false);
    }

    public void BookmarkSelected()
    {
        bookmarkManager.SelectBookmark(index);
    }
    // called by bookmark manager on selected bookmark after resetting bookmarks
    public void Select()
    {
        rightBookmarkFront.Open();
        rightBookmarkBack.Show(false);
        rightBookmarkFront.Show(true);
    }
    // called by bookmark manager when resetting bookmarks
    public void Unselect()
    {
        rightBookmarkBack.Open();
        rightBookmarkFront.Show(false);
    }

    public void FlipLeft()
    {
        leftBookmark.Show(true);
        rightBookmarkBack.Show(false);
        rightBookmarkFront.Show(false);
    }

    public void ResetFlip()
    {
        leftBookmark.Show(false);
        rightBookmarkBack.Show(true);
    }
}
