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
                nameText = "�⺻ ���� Ÿ��";
                descriptionText = "10M �̳��� 1���� Ÿ���� ���� ���� �Ѵ�.";
                break;
            case Define.Tower.Multiply:
                nameText = "���� ���� Ÿ��";
                descriptionText = "5M �̳��� 5���� Ÿ���� ���� ���� �Ѵ�.";
                break;
            case Define.Tower.Focus:
                nameText = "���� ���� Ÿ��";
                descriptionText = "10M �̳��� 1���� Ÿ���� �߽����� 3M �̳��� ��� ������ �������� �ش�.";
                break;
            case Define.Tower.Obstacle:
                nameText = "��ֹ�";
                descriptionText = "���� �̵��� ���� �� �� �ִ�.";
                break;
            case Define.Tower.Illusion:
                nameText = "ȯ�� ��ȣ �ü�";
                descriptionText = "ȯ���� ����� ������ ��ȣ�ü��� �����ϰ� �Ѵ�. (���� �� 30�� �� �Ҹ�)";
                break;
        }

        GetText((int)Texts.NameText).text = nameText;
        GetText((int)Texts.TowerNameText).text = nameText;
        GetText((int)Texts.DescriptionText).text = descriptionText;
        GetText((int)Texts.StatText).text =
@$"ü��: {stat.maxHp}
���ݷ�: {stat.attack}
���ݼӵ�: {stat.attackSpeed}
��Ÿ�: {stat.attackRange}
����: {stat.price}
ũ��: {stat.size}";
    }

    void OnBuildMode()
    {
        if(Managers.Game.Gold < _price)
        {
            Managers.UI.ShowPopupUI<UI_Alert>("UI_MiddleAlert").SetMessage("��尡 �����մϴ�.");
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