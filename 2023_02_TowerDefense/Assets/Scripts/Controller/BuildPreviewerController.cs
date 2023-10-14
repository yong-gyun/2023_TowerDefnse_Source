using Data;
using System.Collections.Generic;
using UnityEngine;

public class BuildPreviewerController : MonoBehaviour
{
    [SerializeField] List<BuildGrid> _touchedGrids = new List<BuildGrid>();
    [SerializeField] List<Material> _mats = new List<Material>();
    [SerializeField] Define.Tower _type;
    [SerializeField] GameObject _grid;
    [SerializeField] int _size;
    [SerializeField] int _price;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Managers.Game.Gold < _price)
        {
            OnExit();
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Buildable")))
        {
            _grid = hit.transform.gameObject;
            transform.position = new Vector3(hit.transform.position.x + 0.92f, 2.4f, hit.transform.position.z - 0.92f);
        }

        if (IsBuildable())
        {
            foreach (Material mat in _mats)
            {
                Color color = Color.green;
                color.a = 0.4f;
                mat.color = color;
            }

            if (Input.GetMouseButtonDown(0))
                OnBuild();
        }
        else
        {
            foreach (Material mat in _mats)
            {
                Color color = Color.red;
                color.a = 0.4f;
                mat.color = color;
            }
        }
    }

    public void OnEnter()
    {
        foreach (BuildGrid grid in Managers.Object.BuildGrids)
            grid.SetActive(true);
    }

    public void OnExit()
    {
        foreach (BuildGrid grid in Managers.Object.BuildGrids)
        {
            grid.SetActive(false);
        }

        Managers.Resource.Destory(gameObject);
    }

    public void OnBuildMode(Define.Tower type)
    {
        TowerStatData stat = Managers.Data.TowerStatDatas[type];
        _type = type;
        _size = stat.size;
        _price = stat.price;

        BoxCollider box = gameObject.GetOrAddComponent<BoxCollider>();
        float boxSize = 1.8f;
        GameObject tower = Managers.Resource.Instantiate($"Tower/{type}Tower", transform);
        tower.transform.Translate(Vector3.up, Space.Self);
        box.size = new Vector3(boxSize, 1f, boxSize);

        {
            GameObject go = Managers.Resource.Instantiate("Subitem/PerviewBuild_Subitem", transform);
            _mats.Add(go.GetComponent<Renderer>().material);
        }

        if (stat.size > 1)
        {
            float interval = 2f;
            tower.transform.Translate(new Vector3(interval / 2f, 0.5f, -interval / 2f), Space.Self);
            box.size = new Vector3(boxSize + interval, 2f, boxSize + interval);
            box.center = new Vector3(interval / 2f, 0f, -interval / 2f);

            {
                GameObject go = Managers.Resource.Instantiate("Subitem/PerviewBuild_Subitem", transform);
                go.transform.Translate(new Vector3(interval, 0, 0), Space.Self);
                _mats.Add(go.GetComponent<Renderer>().material);
            }
            {
                GameObject go = Managers.Resource.Instantiate("Subitem/PerviewBuild_Subitem", transform);
                go.transform.Translate(new Vector3(interval, 0, -interval), Space.Self);
                _mats.Add(go.GetComponent<Renderer>().material);
            }
            {
                GameObject go = Managers.Resource.Instantiate("Subitem/PerviewBuild_Subitem", transform);
                go.transform.Translate(new Vector3(0, 0, -interval), Space.Self);
                _mats.Add(go.GetComponent<Renderer>().material);
            }
        }

        OnEnter();
    }

    bool IsBuildable()
    {
        bool result = true;
        result &= _touchedGrids.Count == _size * _size;
        result &= _grid.GetComponent<BuildGrid>().IsUsing == false;
        
        foreach (BuildGrid grid in _touchedGrids)
            result &= grid.IsUsing == false && grid.IsGridOnUnit == false;
        return result;
    }

    void OnBuild()
    {
        TowerController tc = Managers.Object.BuildTower(_type);
        tc.transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);

        if(_size > 1)
        {
            float interval = 2f;
            tc.transform.position = new Vector3(transform.position.x + interval / 2f, 2.5f, transform.position.z - interval / 2f);
        }

        UI_WaitForTime wait = Managers.UI.MakeWorldSpaceUI<UI_WaitForTime>();
        wait.transform.position = tc.transform.position + (Vector3.up * tc.GetComponent<BoxCollider>().size.y) * 1.5f; 
        wait.OnWait(1f, () => { tc.OnStart(); });

        foreach (BuildGrid grid in _touchedGrids)
            grid.SetUsing(tc.gameObject);

        OnExit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BuildGrid"))
        {
            BuildGrid grid = other.GetComponent<BuildGrid>();

            if (_touchedGrids.Contains(grid) == false)
                _touchedGrids.Add(grid);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BuildGrid"))
        {
            BuildGrid grid = other.GetComponent<BuildGrid>();

            if (_touchedGrids.Contains(grid) == true)
                _touchedGrids.Remove(grid);
        }
    }
}
