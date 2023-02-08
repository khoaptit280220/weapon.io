using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapScreen : UIPanel
{
    [SerializeField] private List<TMP_Text> listTextMap;
    public string textMap;

    private int currentIdMap;
    public static MapScreen Instance { get; private set; }
    
    public override UiPanelType GetId()
    {
        return UiPanelType.MapScreen;
    }

    public static void Show()
    {
        MapScreen newInstance = (MapScreen) GUIManager.Instance.NewPanel(UiPanelType.MapScreen);
        Instance = newInstance;
        newInstance.OnAppear();
    }

    public override void OnAppear()
    {
        base.OnAppear();
        Init();
    }

    private void Init()
    {
        currentIdMap = Database.CurrentIdMap;
    }
    public override void OnDisappear()
    {
        base.OnDisappear();
        Instance = null;
    }

    public void MapBackHome()
    {
        AudioAssistant.Shot(TypeSound.Button);
        Hide();
        MainScreen.Show();
        Database.CurrentIdMap = currentIdMap;
    }

    public void ButtonMap1()
    {
        foreach (var VARIABLE in listTextMap)
        {
            if (VARIABLE.text == "Black Pearl Ruin")
            {
                textMap = "Black Pearl Ruin";
                Debug.Log("id map " + Database.CurrentIdMap);
                Database.CurrentIdMap = 1;
                PopupSelectMap.Show();
            }
        }
    }
    public void ButtonMap2()
    {
        foreach (var VARIABLE in listTextMap)
        {
            if (VARIABLE.text == "Atlantis City")
            {
                textMap = "Atlantis City";
                Debug.Log("id map " + Database.CurrentIdMap);
                Database.CurrentIdMap = 2;
                PopupSelectMap.Show();
            }
        }
    }
    public void ButtonMap3()
    {
        foreach (var VARIABLE in listTextMap)
        {
            if (VARIABLE.text == "Snow Wonderland")
            {
                textMap = "Snow Wonderland";
                Debug.Log("id map " + Database.CurrentIdMap);
                Database.CurrentIdMap = 3;
                PopupSelectMap.Show();
            }
        }
    }
    public void ButtonMap4()
    {
        foreach (var VARIABLE in listTextMap)
        {
            if (VARIABLE.text == "Lost Sector")
            {
                textMap = "Lost Sector";
                Debug.Log("id map " + Database.CurrentIdMap);
                Database.CurrentIdMap = 4;
                PopupSelectMap.Show();
            }
        }
    }
    public void ButtonMap5()
    {
        foreach (var VARIABLE in listTextMap)
        {
            if (VARIABLE.text == "Twilight Shrine")
            {
                textMap = "Twilight Shrine";
                Debug.Log("id map " + Database.CurrentIdMap);
                Database.CurrentIdMap = 5;
                PopupSelectMap.Show();
            }
        }
    }
    public void ButtonMap6()
    {
        foreach (var VARIABLE in listTextMap)
        {
            if (VARIABLE.text == "Midnight Rift")
            {
                textMap = "Midnight Rift";
                Debug.Log("id map " + Database.CurrentIdMap);
                Database.CurrentIdMap = 6;
                PopupSelectMap.Show();
            }
        }
    }
    public void ButtonMap7()
    {
        foreach (var VARIABLE in listTextMap)
        {
            if (VARIABLE.text == "Dream Towers")
            {
                textMap = "Dream Towers";
                Debug.Log("id map " + Database.CurrentIdMap);
                Database.CurrentIdMap = 7;
                PopupSelectMap.Show();
            }
        }
    }
}
