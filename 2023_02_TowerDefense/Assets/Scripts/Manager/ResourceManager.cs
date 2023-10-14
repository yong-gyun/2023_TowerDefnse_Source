using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : UnityEngine.Object
    {
        T origin = Resources.Load<T>(path);

        if (origin == null)
            return null;

        return origin;
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject origin = Load<GameObject>($"Prefabs/{path}");

        if (origin == null)
        {
            Debug.Log($"Load faild {path}");
            return null;
        }

        GameObject go = Object.Instantiate(origin, parent);
        go.name = origin.name;
        return go;
    }

    public GameObject Instantiate(string path, Vector3 pos, Quaternion rot, Transform parent = null)
    {
        GameObject go = Instantiate(path, parent);

        if (go == null)
            return null;

        go.transform.position = pos;
        go.transform.rotation = rot;
        return go;
    }

    public void Destory(GameObject go, float t = 0f)
    {
        if (go == null)
            return;

        Object.Destroy(go, t);
    }
}
