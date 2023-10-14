using UnityEngine;

public class BuildGrid : MonoBehaviour
{
    public bool IsGridOnUnit;
    public bool IsUsing;
    GameObject _grid;
    GameObject _unit;
    GameObject _tower;

    private void Start()
    {
        tag = "BuildGrid";
    }

    private void Update()
    {
        if (IsUsing)
        {
            if (_tower == null)
                IsUsing = false;
        }

        if (IsGridOnUnit)
        {
            if (_unit == null)
                IsGridOnUnit = false;
        }
    }

    public void SetUsing(GameObject go)
    {
        _tower = go;
        IsUsing = true;
    }

    public void SetActive(bool active)
    {
        if (_grid == null)
            _grid = gameObject.FindChild("BuildGrid", true);

        _grid.SetActive(active);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            if (_unit == null)
            {
                _unit = other.gameObject;
                IsGridOnUnit = true;
            }
        }

        if (other.CompareTag("Tower"))
        {
            if (_tower == null)
            {
                _tower = other.gameObject;
                IsUsing = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            if (_unit != null)
            {
                _unit = null;
                IsGridOnUnit = false;
            }
        }

        if (other.CompareTag("Unit"))
        {
            if (_tower != null)
            {
                _tower = null;
                IsUsing = false;
            }
        }
    }
}
