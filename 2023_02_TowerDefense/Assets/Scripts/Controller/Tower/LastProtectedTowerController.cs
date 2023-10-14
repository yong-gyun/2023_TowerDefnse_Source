using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastProtectedTowerController : TowerController
{
    // Start is called before the first frame update
    void Awake()
    {
        OnStart();
    }

    public override bool OnStart()
    {
        if (base.OnStart() == false)
            return false;

        SetStat(Define.Tower.LastProtected);
        return true;
    }

    protected override void OnDead()
    {
        //@TODO Managers.UI.ShowPopupUI<UI_GameClearFaildPopup>();
        base.OnDead();
    }
}
