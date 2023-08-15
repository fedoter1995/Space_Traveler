using SpaceTraveler.UI;
using System.Collections.Generic;
using UnityEngine;

public class GarageUI : MonoBehaviour
{
    [SerializeField]
    private List<InteractiveTab> _tabs;
    [SerializeField]
    private InteractiveTabBookmark _bookmarkPrefab;

    [SerializeField]
    private Transform _header;

    private InteractiveTabBookmark activeBookmark;

    private List<InteractiveTabBookmark> bookmarks;


    public void Initialize()
    {
        SetupBookmarks();

        if (activeBookmark is null)
            activeBookmark = bookmarks[0];

        OpenTab(activeBookmark);
    }

    public void OpenTab(InteractiveTabBookmark bookmark)
    {
        SetActiveBookmark(activeBookmark, false);
        activeBookmark = bookmark;
        SetActiveBookmark(activeBookmark, true);
    }


    private void SetActiveBookmark(InteractiveTabBookmark bookmark, bool activity)
    {
        bookmark.SetActive(activity);
    }
    private void SetupBookmarks()
    {
        bookmarks = new List<InteractiveTabBookmark>();
        foreach (InteractiveTab tab in _tabs)
        {
            tab.Initialize();
            CreateBookmark(tab);
        }
    }

    private void CreateBookmark(InteractiveTab tab)
    {
        var bookmark = Instantiate(_bookmarkPrefab, _header);

        bookmark.Initialize();
        bookmark.SetGarageTab(tab);
        bookmark.OnClickEvent += OpenTab;
        SetActiveBookmark(bookmark, false);
        bookmarks.Add(bookmark);
    }
}
