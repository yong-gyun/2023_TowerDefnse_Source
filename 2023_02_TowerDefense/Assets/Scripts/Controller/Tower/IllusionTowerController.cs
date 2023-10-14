using UnityEngine;

public class IllusionTowerController : TowerController
{
    public override bool OnStart()
    {
        if (base.OnStart() == false)
            return false;

        Managers.Object.IllusionTowers.Add(this);
        return true;
    }

    protected override void Update()
    {
        if (Hp <= 0)
            return;

        Hp -= Time.deltaTime;

        if (Hp <= 0f)
        {
            Hp = 0f;
            OnDead();
        }
    }

    public override void OnDamaged(BaseController bc)
    {

    }
}