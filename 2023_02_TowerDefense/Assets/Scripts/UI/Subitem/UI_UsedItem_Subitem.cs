using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_UsedItem_Subitem : UI_Base
{
    enum Images
    {
        Icon,
        DurationImage
    }

    enum Texts
    {
        DurationText
    }

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;
        BindImage(typeof(Images));
        BindText(typeof(Texts));
        return true;
    }

    public void SetItem()
    {
        
    }
}
