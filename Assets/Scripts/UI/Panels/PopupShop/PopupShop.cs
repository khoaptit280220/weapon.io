using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class PopupShop : UIPanel
{
    [SerializeField] private TMP_Text coin;
    
    [ReadOnly] public ShopState currentShopState = ShopState.Skin;
    public Transform content;
    public GameObject btnOnShopSkin;
    public GameObject btnOffShopSkin;
    public GameObject btnOnShopSword;
    public GameObject btnOffShopSword;
    public GameObject btnOnShopTrail;
    public GameObject btnOffShopTrail;
    public GameObject btnOnShopPack;
    public GameObject btnOffShopPack;
    
    [SerializeField] private ShopItem shopItemPrefabs;
    private ItemConfig itemConfig;
    private List<ItemData> listItemDatas = new List<ItemData>();

    public static PopupShop Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.PopupShop;
    }
    public static void Show()
    {
        var newInstance = (PopupShop)GUIManager.Instance.NewPanel(UiPanelType.PopupShop);
        Instance = newInstance;
        newInstance.OnAppear();
        
    }

    public override void OnAppear()
    {
        if (isInited)
            return;

        base.OnAppear();
        Init();
    }

    private void Init()
    {
        UpdateText();
        itemConfig = ConfigManager.Instance.itemConfig;
        SetupState(currentShopState);
    }

    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
    }

    public void UpdateText()
    {
        coin.text = "" + Gm.data.user.money;
    }
    
    public void OnClickBtnSkin(int idSkin)
    {
        //if (ConfigManager.Instance.modelSkinConfig.GetModelSkinById(idSkin).IsUnlock == true)
        {
            Database.CurrentIdModelSkin = idSkin;
        }
    }

    public void ShopBackHome()
    {
        AudioAssistant.Shot(TypeSound.Button);
        Hide();
        GameManager.Instance.PrepareGame();
        MainScreen.Show();
    }

    public void Close()
    {
        Hide();
    }
    private void SetupDefaultBtn()
    {
        btnOffShopPack.SetActive(false);
        btnOffShopSkin.SetActive(false);
        btnOffShopSword.SetActive(false);
        btnOffShopTrail.SetActive(false);
        btnOnShopPack.SetActive(false);
        btnOnShopSkin.SetActive(false);
        btnOnShopSword.SetActive(false);
        btnOnShopTrail.SetActive(false);
    }

    private void SetupBtn(ShopState _shopState)
    {
        SetupDefaultBtn();
        switch (_shopState)
        {
            case ShopState.Skin:
                btnOnShopSkin.SetActive(true);
                btnOffShopSword.SetActive(true);
                btnOffShopTrail.SetActive(true);
                btnOffShopPack.SetActive(true);
                break;
            case ShopState.Sword:
                btnOffShopSkin.SetActive(true);
                btnOnShopSword.SetActive(true);
                btnOffShopTrail.SetActive(true);
                btnOffShopPack.SetActive(true);
                break;
            case ShopState.Trail:
                btnOffShopSkin.SetActive(true);
                btnOffShopSword.SetActive(true);
                btnOnShopTrail.SetActive(true);
                btnOffShopPack.SetActive(true);
                break;
            case ShopState.Pack:
                btnOffShopSkin.SetActive(true);
                btnOffShopSword.SetActive(true);
                btnOffShopTrail.SetActive(true);
                btnOnShopPack.SetActive(true);
                break;
        }
    }

    public void SetupState(ShopState _shopState)
    {
        var childs = content.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            DestroyImmediate(content.GetChild(i).gameObject, true);
        }
        //Utility.Clear(content);
        currentShopState = _shopState;
        SetupBtn(_shopState);
        switch (_shopState)
        {
            case ShopState.Skin:
                listItemDatas = itemConfig.ListSkinDatas;
                break;
            case ShopState.Sword:
                listItemDatas = itemConfig.ListSwordDatas;
                break;
            case ShopState.Trail:
                listItemDatas = itemConfig.ListTrailDatas;
                break;
            case ShopState.Pack:
                listItemDatas = itemConfig.ListPackDatas;
                break;
        }

        for (int i = 0; i < listItemDatas.Count; i++)
        {
            ShopItem shopItem = Instantiate(shopItemPrefabs, content);
            shopItem.InitItemData(listItemDatas[i]);
        }
    }

    public void OnClickSkinShop()
    {
        if (currentShopState != ShopState.Skin)
        {
            currentShopState = ShopState.Skin;
            SetupState(currentShopState);
        }
    }

    public void OnClickSwordShop()
    {
        if (currentShopState != ShopState.Sword)
        {
            currentShopState = ShopState.Sword;
            SetupState(currentShopState);
        }
    }
    public void OnClickTrailShop()
    {
        if (currentShopState != ShopState.Trail)
        {
            currentShopState = ShopState.Trail;
            SetupState(currentShopState);
        }
    }
    public void OnClickPackShop()
    {
        if (currentShopState != ShopState.Pack)
        {
            currentShopState = ShopState.Pack;
            SetupState(currentShopState);
        }
    }
    public void ViewSkin(int idSkin)
    {
        //playerSkinController.ViewSkin(idSkin);
    }

    private void Update()
    {
        UpdateText();
    }
}
public enum ShopState
{
    Skin,
    Sword,
    Trail,
    Pack,
}