using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public UI_Scene SceneUI;
    List<UI_Popup> _popupList = new List<UI_Popup>();
    int _order = 10;

    public void SetCanvas(GameObject go, bool sort = false)
    {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");

        if (go == null)
            return null;

        T popup = go.GetOrAddComponent<T>();
        _popupList.Add(popup);
        return popup;
    }

    public void ClosePopupUI()
    {
        if (_popupList.Count > 0)
            ClosePopupUI(_popupList[0]);
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        bool result = _popupList.Remove(popup);

        if (result)
            Managers.Resource.Destory(popup.gameObject);
    }

    public void CloseAllPopupUI()
    {
        while (_popupList.Count > 0)
            ClosePopupUI();
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

        if (go == null)
            return null;

        T scene = go.GetOrAddComponent<T>();
        SceneUI = scene;
        return scene;
    }

    public T MakeSubitemUI<T>(Transform parent, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Subitem/{name}");

        if (go == null)
            return null;

        T subitem = go.GetOrAddComponent<T>();
        subitem.transform.SetParent(parent);
        return subitem;
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");

        if (go == null)
            return null;

        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        T worldSpace = go.GetOrAddComponent<T>();
        if(parent != null)
            worldSpace.transform.SetParent(parent);
        return worldSpace;
    }

    public void Clear()
    {
        CloseAllPopupUI();
        SceneUI = null;
    }
}