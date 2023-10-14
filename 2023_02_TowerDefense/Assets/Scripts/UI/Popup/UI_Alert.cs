using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Alert : UI_Popup
{
    enum Images
    {
        BG
    }

    enum Texts
    {
        Text
    }

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        return true;
    }

    public void SetMessage(string message)
    {
        GetText((int)Texts.Text).text = message;
        StartCoroutine(CoDestroy());
    }

    IEnumerator CoDestroy()
    {
        float t = 0f;
        float dest = 0.75f;

        Color color = GetImage((int)Images.BG).color;

        while(color.a > 0.2f)
        {
            t += Time.deltaTime / dest;
            color.a = Mathf.Lerp(1f, 0.2f, t);
            GetImage((int)Images.BG).color = color;
            yield return null;
        }

        ClosePopupUI();
    }
}
