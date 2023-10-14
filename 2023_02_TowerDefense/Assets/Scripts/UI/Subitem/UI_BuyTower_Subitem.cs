using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BuyTower_Subitem : UI_Base
{
    enum GameObjects
    {
        Description
    }

    enum Texts
    {
        TowerNameText,
        NameText,
        StatText,
        DescriptionText
    }

    Define.Tower _type;
    int _price;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));
        gameObject.BindEvent((evtData) => { OnBuildMode(); }, Define.UIEvent.Click);
        gameObject.BindEvent((evtData) => { GetObject((int)GameObjects.Description).SetActive(true); }, Define.UIEvent.PointerEnter);
        gameObject.BindEvent((evtData) => { GetObject((int)GameObjects.Description).SetActive(false); }, Define.UIEvent.PointerExit);
        GetObject((int)GameObjects.Description).SetActive(false);
        return true;
    }

    public void SetInfo(Define.Tower type)
    {
        Data.TowerStatData stat = Managers.Data.TowerStatDatas[type];
        string nameText = "";
        string descriptionText = "";
        _type = type;
        _price = stat.price;

        switch(type)
        {
            case Define.Tower.Default:
                nameText = "기본 공격 타워";
                descriptionText = "10M 이내의 1개의 타겟을 향해 공격 한다.";
                break;
            case Define.Tower.Multiply:
                nameText = "다중 공격 타워";
                descriptionText = "5M 이내의 5개의 타겟을 향해 공격 한다.";
                break;
            case Define.Tower.Focus:
                nameText = "집중 공격 타워";
                descriptionText = "10M 이내의 1개의 타겟을 중심으로 3M 이내의 모든 적에게 데미지를 준다.";
                break;
            case Define.Tower.Obstacle:
                nameText = "장애물";
                descriptionText = "적의 이동을 방해 할 수 있다.";
                break;
            case Define.Tower.Illusion:
                nameText = "환영 보호 시설";
                descriptionText = "환영을 만들어 적들을 보호시설로 착각하게 한다. (생성 후 30초 후 소멸)";
                break;
        }

        GetText((int)Texts.NameText).text = nameText;
        GetText((int)Texts.TowerNameText).text = nameText;
        GetText((int)Texts.DescriptionText).text = descriptionText;
        GetText((int)Texts.StatText).text =
@$"체력: {stat.maxHp}
공격력: {stat.attack}
공격속도: {stat.attackSpeed}
사거리: {stat.attackRange}
가격: {stat.price}
크기: {stat.size}";
    }

    void OnBuildMode()
    {
        if(Managers.Game.Gold < _price)
        {
            Managers.UI.ShowPopupUI<UI_Alert>("UI_MiddleAlert").SetMessage("골드가 부족합니다.");
            return;
        }

        GameObject go = GameObject.Find("BuildPreviewer");

        if(go != null)
            go.GetComponent<BuildPreviewerController>().OnExit();

        go = new GameObject("BuildPreviewer");
        BuildPreviewerController previewer = go.GetOrAddComponent<BuildPreviewerController>();
        previewer.OnBuildMode(_type);
    }
}