using Data;
using System.Collections.Generic;

public class DataManager
{
    public Dictionary<Define.Tower, TowerStatData> TowerStatDatas { get; private set; } = new Dictionary<Define.Tower, TowerStatData>();
    public Dictionary<Define.Unit, UnitStatData> UnitStatDatas { get; private set; } = new Dictionary<Define.Unit, UnitStatData>();

    public void Init()
    {
        LoadTowerStatData();
        LoadUnitStatData();
    }

    void LoadTowerStatData()
    {
        TowerStatDatas.Add(Define.Tower.Default, new TowerStatData()
        {
            maxHp = 50f,
            hp = 50f,
            attack = 2f,
            attackRange = 10f,
            attackSpeed = 0.25f,
            size = 1,
            price = 10
        });

        TowerStatDatas.Add(Define.Tower.Multiply, new TowerStatData()
        {
            maxHp = 100f,
            hp = 100f,
            attack = 5f,
            attackRange = 5f,
            attackSpeed = 1f,
            size = 2,
            price = 30
        });

        TowerStatDatas.Add(Define.Tower.Focus, new TowerStatData()
        {
            maxHp = 80f,
            hp = 80f,
            attack = 10f,
            attackRange = 10f,
            attackSpeed = 1.25f,
            size = 1,
            price = 50
        });

        TowerStatDatas.Add(Define.Tower.Illusion, new TowerStatData()
        {
            maxHp = 30f,
            hp = 30f,
            attack = 0,
            attackRange = 0,
            attackSpeed = 0,
            size = 2,
            price = 100
        });

        TowerStatDatas.Add(Define.Tower.Obstacle, new TowerStatData()
        {
            maxHp = 200f,
            hp = 200f,
            attack = 0,
            attackRange = 0f,
            attackSpeed = 0f,
            size = 2,
            price = 80
        });


        TowerStatDatas.Add(Define.Tower.Protected, new TowerStatData()
        {
            maxHp = 500f,
            hp = 500f,
            attack = 0,
            attackRange = 0f,
            attackSpeed = 0f,
            size = 2,
            price = 0
        });


        TowerStatDatas.Add(Define.Tower.LastProtected, new TowerStatData()
        {
            maxHp = 700f,
            hp = 700f,
            attack = 0,
            attackRange = 0f,
            attackSpeed = 0f,
            size = 2,
            price = 0
        });
    }
    void LoadUnitStatData()
    {
        UnitStatDatas.Add(Define.Unit.Melee, new UnitStatData()
        {
            maxHp = 20f,
            hp = 20f,
            attack = 5,
            attackRange = 2f,
            attackSpeed = 1f,
            moveSpeed = 3f,
            rewardGold = 2
        });

        UnitStatDatas.Add(Define.Unit.RangedAttck, new UnitStatData()
        {
            maxHp = 15f,
            hp = 15f,
            attack = 10,
            attackRange = 5f,
            attackSpeed = 1f,
            moveSpeed = 3f,
            rewardGold = 4
        });

        UnitStatDatas.Add(Define.Unit.QuickMove, new UnitStatData()
        {
            maxHp = 20f,
            hp = 20f,
            attack = 5,
            attackRange = 2f,
            attackSpeed = 1f,
            moveSpeed = 4.5f,
            rewardGold = 5
        });

        UnitStatDatas.Add(Define.Unit.Flyable, new UnitStatData()
        {
            maxHp = 10f,
            hp = 10f,
            attack = 8,
            attackRange = 5f,
            attackSpeed = 1f,
            moveSpeed = 3f,
            rewardGold = 8
        });

        UnitStatDatas.Add(Define.Unit.Gold, new UnitStatData()
        {
            maxHp = 100f,
            hp = 100f,
            attack = 10,
            attackRange = 7f,
            attackSpeed = 7f,
            moveSpeed = 3f,
            rewardGold = 50
        });

        UnitStatDatas.Add(Define.Unit.MiddleBoss, new UnitStatData()
        {
            maxHp = 100f,
            hp = 100f,
            attack = 15f,
            attackRange = 2f,
            attackSpeed = 1f,
            moveSpeed = 3f,
            rewardGold = 10
        });

        UnitStatDatas.Add(Define.Unit.StageBoss, new UnitStatData()
        {
            maxHp = 300f,
            hp = 300f,
            attack = 20f,
            attackRange = 2f,
            attackSpeed = 1f,
            moveSpeed = 3f,
            rewardGold = 100
        });

    }
}