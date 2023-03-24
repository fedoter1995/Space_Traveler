using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageUI : MonoBehaviour
{
    [SerializeField]
    private List<GarageTab> _tabs;
    [SerializeField]
    private GarageBookmark _bookmarkPrefab;

    [SerializeField]
    private Transform _header;

    private GarageBookmark activeBookmark;

    private List<GarageBookmark> bookmarks;


    public void Initialize()
    {
        SetupBookmarks();

        if (activeBookmark is null)
            activeBookmark = bookmarks[0];

        OpenTab(activeBookmark);
    }

    public void OpenTab(GarageBookmark bookmark)
    {
        SetActiveBookmark(activeBookmark, false);
        activeBookmark = bookmark;
        SetActiveBookmark(activeBookmark, true);
    }


    private void SetActiveBookmark(GarageBookmark bookmark, bool activity)
    {
        bookmark.SetActive(activity);
    }
    private void SetupBookmarks()
    {
        bookmarks = new List<GarageBookmark>();
        foreach (GarageTab tab in _tabs)
        {
            tab.Initialize();
            CreateBookmark(tab);
        }
    }

    private void CreateBookmark(GarageTab tab)
    {
        var bookmark = Instantiate(_bookmarkPrefab, _header);

        bookmark.Initialize();
        bookmark.SetGarageTab(tab);
        bookmark.OnClickEvent += OpenTab;
        SetActiveBookmark(bookmark, false);
        bookmarks.Add(bookmark);
    }
}
