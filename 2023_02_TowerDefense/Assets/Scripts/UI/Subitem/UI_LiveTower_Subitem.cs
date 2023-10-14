using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LiveTower_Subitem : UI_Base
{
    enum Texts
    {
        NameText,
    }

    enum Scrollbars
    {
        HPBar
    }

    TowerController _tc;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindScrollbar(typeof(Scrollbars));

        gameObject.BindEvent((evtData) =>
        {
            Camera.main.transform.position = new Vector3(_tc.transform.position.x, Camera.main.transform.position.y, _tc.transform.position.z);
        }, Define.UIEvent.Click);
        
        return true;
    }

    public void SetTower(TowerController tc, int idx = 1)
    {
        _tc = tc;

        switch (tc.Type)
        {
            case Define.Tower.Protected:
                GetText((int)Texts.NameText).text = $"보호 시설 {idx}";
                break;
            case Define.Tower.LastProtected:
                GetText((int)Texts.NameText).text = $"최종 보호 시설";
                break;
        }
    }

    private void Update()
    {
        if(_tc != null)
        {
            GetScrollbar((int)Scrollbars.HPBar).size = _tc.Hp / _tc.MaxHp;
        }
        else
        {
            Managers.Resource.Destory(gameObject);
        }
    }
}