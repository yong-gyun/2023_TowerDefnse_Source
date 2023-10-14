using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    public List<EnemyController> EnemyPool { get { _enemyPool.RemoveAll(x => x == null); return _enemyPool; } }
    public List<IllusionTowerController> IllusionTowers { get { _illusionTowers.RemoveAll(x => x == null); return _illusionTowers; } }
    public List<ProtectedTowerController> ProtectedTowers { get { _protectedTowers.RemoveAll(x => x == null); return _protectedTowers; } }
    public List<BuildGrid> BuildGrids { get; set; } = new List<BuildGrid>();
    public List<Transform> Bridgs { get; set; } = new List<Transform>();
    public List<TowerController> TowerPool { get; set; } = new List<TowerController>();
    public LastProtectedTowerController LastProtectedTower { get { return _lastProtectedTower; } }
    List<EnemyController> _enemyPool = new List<EnemyController>();
    List<IllusionTowerController> _illusionTowers = new List<IllusionTowerController>();
    List<ProtectedTowerController> _protectedTowers = new List<ProtectedTowerController>();
    LastProtectedTowerController _lastProtectedTower;

    public void Init()
    {

    }

    public void InitMap()
    {
        GameObject map = GameObject.Find("@Map");

        foreach (BuildGrid grid in map.GetComponentsInChildren<BuildGrid>())
        {
            BuildGrids.Add(grid);
            grid.SetActive(false);
        }

        Transform brids = map.FindChild<Transform>("Bridges", true);

        for (int i = 0; i < brids.childCount; i++)
            Bridgs.Add(brids.GetChild(i));

        Transform protectedTowers = map.FindChild<Transform>("ProtectedTowers", true);

        for (int i = 0; i < protectedTowers.childCount; i++)
        {
            Transform transform = protectedTowers.GetChild(i);
            ProtectedTowerController ptc = transform.GetComponent<ProtectedTowerController>();

            if (ptc != null)
                _protectedTowers.Add(ptc);
        }

        LastProtectedTowerController lpt = map.FindChild<LastProtectedTowerController>("LastProtectedTower", true);

        if (lpt != null)
            _lastProtectedTower = lpt;
    }

    public TowerController BuildTower(Define.Tower type)
    {
        GameObject go = Managers.Resource.Instantiate($"Tower/{type}Tower");
        TowerController tc = go.GetOrAddComponent<TowerController>();

        if (type != Define.Tower.Illusion)
            TowerPool.Add(tc);

        return tc;
    }

    public EnemyController SpawnEnemy(Define.Unit type)
    {
        GameObject go = Managers.Resource.Instantiate($"Unit/Enemy/{type}");
        EnemyController ec = go.GetOrAddComponent<EnemyController>();
        ec.SetStat(type);
        _enemyPool.Add(ec);
        return ec;
    }

    public StageBossController SpawnStageBoss()
    {
        GameObject go = Managers.Resource.Instantiate($"Unit/Boss/Stage{Managers.Game.Stage}_Boss");
        StageBossController sbc = go.GetOrAddComponent<StageBossController>();
        sbc.SetStat(Define.Unit.StageBoss);
        return sbc;
    }



    public void Despawn(GameObject go)
    {
        //TODO Destory
        Define.WorldObject type  = GetWorldObjectType(go);
        switch(type)
        {
            case Define.WorldObject.Tower:
                {
                    TowerPool.Remove(go.GetComponent<TowerController>());
                }
                break;
            case Define.WorldObject.Unit:
                {
                    EnemyController ec = go.GetComponent<EnemyController>();
                    
                    if(ec != null)
                    {
                        EnemyPool.Remove(ec);
                        Managers.Game.Gold += ec.RewardGold;
                        Managers.Game.Score += ec.RewardScore;
                    }
                }
                break;
        }

        Managers.Resource.Destory(go);
    }

    Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        return bc.WorldObjectType;
    }
}
