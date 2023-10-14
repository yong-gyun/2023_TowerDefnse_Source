using UnityEngine;

public class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType;
    bool _init;

    private void Awake()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (_init)
            return false;

        _init = true;
        return true;
    }
}
