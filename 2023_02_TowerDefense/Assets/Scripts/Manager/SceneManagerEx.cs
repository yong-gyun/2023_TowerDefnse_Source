using System;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public void Load(Define.Scene type)
    {
        SceneManager.LoadScene(type.ToString());
    }

    public void LoadAsync(Define.Scene type, Action completed = null)
    {

    }
}
