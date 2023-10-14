using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectedTowerController : TowerController
{
    private void Awake()
    {
        OnStart();
    }

    public override bool OnStart()
    {
        if (base.OnStart() == false)
            return false;

        Type = Define.Tower.Protected;
        SetStat(Define.Tower.Protected);
        return true;
    }

    protected override void Update()
    {

    }
}
