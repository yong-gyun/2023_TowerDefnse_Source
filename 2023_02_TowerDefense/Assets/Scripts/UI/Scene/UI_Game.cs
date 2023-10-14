using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Game : UI_Scene
{
    enum Texts
    {
        WaveText,
        StageText,
        ScoreText,
        TimeText,
        GoldText, 
        ItemText,
        GameSpeedText
    }

    enum GameObjects
    {
        ItemContent, 
        ActionContent,
        UsedItemContent,
        BuyTowerContent,
        LiveTowerContent
    }

    enum Buttons
    {
        ItemBuyButton,
        ChangeGameSpeedButton
    }

    [SerializeField] UI_ItemSlot_Subitem[] _itemSlotSubitem = new UI_ItemSlot_Subitem[3];

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));

        Managers.UI.MakeSubitemUI<UI_BuyTower_Subitem>(GetObject((int) GameObjects.BuyTowerContent).transform).SetInfo(Define.Tower.Default);
        Managers.UI.MakeSubitemUI<UI_BuyTower_Subitem>(GetObject((int)GameObjects.BuyTowerContent).transform).SetInfo(Define.Tower.Multiply);
        Managers.UI.MakeSubitemUI<UI_BuyTower_Subitem>(GetObject((int)GameObjects.BuyTowerContent).transform).SetInfo(Define.Tower.Focus);
        Managers.UI.MakeSubitemUI<UI_BuyTower_Subitem>(GetObject((int) GameObjects.BuyTowerContent).transform).SetInfo(Define.Tower.Illusion);
        Managers.UI.MakeSubitemUI<UI_BuyTower_Subitem>(GetObject((int) GameObjects.BuyTowerContent).transform).SetInfo(Define.Tower.Obstacle);

        GetButton((int)Buttons.ChangeGameSpeedButton).gameObject.BindEvent(OnClickChangeGameSpeedButton, Define.UIEvent.Click);
        GetButton((int)Buttons.ItemBuyButton).gameObject.BindEvent(OnClickItemBuyButton, Define.UIEvent.Click);

        if (Managers.Object.LastProtectedTower != null)
        {
            UI_LiveTower_Subitem subitem = Managers.UI.MakeSubitemUI<UI_LiveTower_Subitem>(GetObject((int)GameObjects.LiveTowerContent).transform);
            subitem.SetTower(Managers.Object.LastProtectedTower);
        }
        
        for (int i = 0; i < Managers.Object.ProtectedTowers.Count; i++)
        {
            UI_LiveTower_Subitem subitem = Managers.UI.MakeSubitemUI<UI_LiveTower_Subitem>(GetObject((int)GameObjects.LiveTowerContent).transform);
            subitem.SetTower(Managers.Object.ProtectedTowers[i], i + 1);
        }

        for (int i = 0; i < 3; i++)
        {
            UI_ItemSlot_Subitem subitem = Managers.UI.MakeSubitemUI<UI_ItemSlot_Subitem>(GetObject((int)GameObjects.ItemContent).transform);
            subitem.gameObject.BindEvent((evtData) => { OnClickItemSlotButton(subitem); }, Define.UIEvent.Click);
            _itemSlotSubitem[i] = subitem;
        }

        Time.timeScale = 1;
        GetText((int)Texts.GameSpeedText).text = "x1";
        return true;
    }

    void Update()
    {
        GetText((int)Texts.StageText).text = $"�������� : {Managers.Game.Stage}";
        GetText((int)Texts.GoldText).text = $"{Managers.Game.Gold}";
        GetText((int)Texts.WaveText).text = 
@$"���� ���̺�
{Managers.Game.CurrentWave} / {Managers.Game.MaxWave}";

        int min = (int) Managers.Game.PlayTime / 60;
        int sec = (int) Managers.Game.PlayTime % 60;

        GetText((int)Texts.TimeText).text = string.Format("{0:00}:{1:00} (x{2:0})", min, sec, Time.timeScale > 1 ? 2 : 1);
        GetText((int)Texts.ScoreText).text = $"{Managers.Game.Score}";

        for (int i = 0; i < _itemSlotSubitem.Length; i++)
        {
            _itemSlotSubitem[i].SetItem(Managers.Game.CurrentHaveItmes[i]);
        }
    }

    void OnClickChangeGameSpeedButton(PointerEventData evtData)
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 2;
            GetText((int)Texts.GameSpeedText).text = $"x2";
        }
        else
        {
            Time.timeScale = 1;
            GetText((int)Texts.GameSpeedText).text = $"x1";
        }
    }

    public void AddUsedItem(Define.Item type)
    {
        Managers.UI.MakeSubitemUI<UI_UsedItem_Subitem>(GetObject((int)GameObjects.UsedItemContent).transform);
    }

    void OnClickItemBuyButton(PointerEventData evtData)
    {
        int idx = Managers.Game.CurrentItemPriceIdx;
        int count = Managers.Game.CurrentHaveItmes.Where(x => x != Define.Item.Unknow).Count();

        if (count == 3)
        {
            Managers.UI.ShowPopupUI<UI_Alert>("UI_MiddleAlert").SetMessage("�������� ���� �� �� �����ϴ�.");
            return;
        }

        if (idx == Managers.Game.ItemPrices.Length)
        {
            Managers.UI.ShowPopupUI<UI_Alert>("UI_MiddleAlert").SetMessage("���� ������ ���� �����ϼ̽��ϴ�.");
            return;
        }

        if (Managers.Game.ItemPrices[idx] > Managers.Game.Gold)
        {
            Managers.UI.ShowPopupUI<UI_Alert>("UI_MiddleAlert").SetMessage("��尡 �����մϴ�.");
            return;
        }

        Managers.Game.CurrentItemPriceIdx++;

        GetText((int)Texts.ItemText).text =
@$"������ ���� {Managers.Game.ItemPrices[idx]}G
(���� ���� Ƚ�� : {Managers.Game.ItemPrices.Length - Managers.Game.CurrentItemPriceIdx})";
        Managers.Game.GetRandItem();
    }

    void OnClickItemSlotButton(UI_ItemSlot_Subitem subitem)
    {
        if (subitem.Type == Define.Item.Unknow)
        {
            Debug.Log("Return");
            return;
        }

        for (int i = 0; i < _itemSlotSubitem.Length; i++)
        {
            if (_itemSlotSubitem[i].Type != Define.Item.Unknow)
                _itemSlotSubitem[i].OnCooltime();
        }

        Debug.Log("Check");
    }
}