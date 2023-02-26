using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageUI : MonoBehaviour
{
    [SerializeField]
    private List<GarageBookmark> _bookmarks;
    [SerializeField]
    private GarageBookmark _activeBookmark;
    public void Initialize()
    {

        if (_bookmarks.Count == 0)
            FindBookmarks();

        InitializeBookmarks();

        SetListener();

        if (_activeBookmark is null)
            _activeBookmark = _bookmarks[0];

        foreach (GarageBookmark bookmark in _bookmarks)
            SetActiveBookmark(bookmark, false);

        OpenTab(_activeBookmark);
    }

    public void OpenTab(GarageBookmark bookmark)
    {
        SetActiveBookmark(_activeBookmark, false);
        _activeBookmark = bookmark;
        SetActiveBookmark(_activeBookmark, true);
    }


    private void SetActiveBookmark(GarageBookmark bookmark, bool activity)
    {
        bookmark.SetActive(activity);
    }
    private void SetListener()
    {
        foreach (GarageBookmark bookmark in _bookmarks)
        {
            bookmark.OnClickEvent += OpenTab;
        }
    }

    private void FindBookmarks()
    {
        _bookmarks = new List<GarageBookmark>(gameObject.GetComponentsInChildren<GarageBookmark>());
        if (_bookmarks.Count == 0)
            throw new System.Exception($"Can not find bookmarks in {this} Childrens");
    }

    private void InitializeBookmarks()
    {
        foreach (Bookmark bookmark in _bookmarks)
        {
            bookmark.Initialize();
        }
    }
}
