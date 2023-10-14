using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Scene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Object.InitMap();
        Managers.Game.Stage = 1;
        Managers.UI.ShowSceneUI<UI_Game>();
        Managers.Object.SpawnEnemy(Define.Unit.Melee).transform.position = Managers.Object.Bridgs[0].position;
        return true;
    }
}