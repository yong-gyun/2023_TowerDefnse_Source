using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ItemSlot_Subitem : UI_Base
{
    enum Images
    {
        Icon,
        CooltimeImage
    }

    enum Texts
    {
        CooltimeText
    }

    public Define.Item Type { get { return _type; } }
    [SerializeField] Define.Item _type;
    float _cooltime = 5f;
    float _currentCooltime;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));
        gameObject.BindEvent(OnClickItemButton, Define.UIEvent.Click);
        GetText((int)Texts.CooltimeText).text = "";
        GetImage((int)Images.CooltimeImage).gameObject.SetActive(false);
        GetImage((int)Images.Icon).gameObject.SetActive(false);
        SetItem(Define.Item.Unknow);
        return true;
    }

    void OnClickItemButton(PointerEventData evtData)
    {
        if (_currentCooltime > 0f)
            return;

        if (_type == Define.Item.Unknow)
            return;

        UI_Game sceneUI = Managers.UI.SceneUI as UI_Game;
        sceneUI.AddUsedItem(_type);
        SetItem(Define.Item.Unknow);
    }

    public void SetItem(Define.Item type)
    {
        _type = type;

        if (type == Define.Item.Unknow)
        {
            GetImage((int)Images.Icon).gameObject.SetActive(false);
            return;
        }

        GetImage((int)Images.Icon).sprite = Managers.Resource.Load<Sprite>($"Sprites/Icon/Item/{type}");
        GetImage((int)Images.Icon).gameObject.SetActive(true);
    }

    public void OnCooltime()
    {
        StartCoroutine(CoCooltime());
    }

    IEnumerator CoCooltime()
    {
        GetImage((int)Images.CooltimeImage).gameObject.SetActive(true);
        _currentCooltime = _cooltime;

        while(_currentCooltime > 0f)
        {
            GetImage((int)Images.CooltimeImage).fillAmount = _currentCooltime / _cooltime;
            GetText((int)Texts.CooltimeText).text = string.Format("{0:0}", _currentCooltime);
            _currentCooltime -= Time.deltaTime;
            yield return null;
        }

        _currentCooltime = 0f;
        GetText((int)Texts.CooltimeText).text = "";
        GetImage((int)Images.Icon).gameObject.SetActive(false);
    }
}