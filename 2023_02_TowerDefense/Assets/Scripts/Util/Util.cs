using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (recursive)
        {
            foreach (T component in go.transform.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        else
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                T component = go.transform.GetChild(i).GetComponent<T>();

                if (component != null)
                {
                    if (string.IsNullOrEmpty(name) || component.name == name)
                        return component;
                }
            }
        }

        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);

        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T GetShortestDistance<T>(GameObject a, List<T> targets) where T : MonoBehaviour
    {
        if (targets.Count == 0)
            return null;

        float maxDinstance = Mathf.Infinity;
        T lockTarget = null;

        foreach (T target in targets)
        {
            float distance = GetDistance(a.transform.position, target.transform.position);
            if (distance < maxDinstance)
            {
                maxDinstance = distance;
                lockTarget = target;
            }
        }

        return lockTarget;
    }

    public static float GetDistance(Vector3 from, Vector3 target)
    {
        float distance = (target - from).magnitude * Define.TILE_SIZE;
        return distance;
    }
}
