using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapScreen : UIPanel
{
    [SerializeField] private List<TextMeshProUGUI> listTextMap;
    public List<ButtonMap> ListButtonMaps;
    public string textMap;
    public string desMap;

    private int currentIdMap;
    public static MapScreen Instance { get; private set; }

    public override UiPanelType GetId()
    {
        return UiPanelType.MapScreen;
    }

    public static void Show()
    {
        MapScreen newInstance = (MapScreen)GUIManager.Instance.NewPanel(UiPanelType.MapScreen);
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
        GameManager.Instance.previewMain.SetActive(true);
        MainScreen.Show();
        Database.CurrentIdMap = currentIdMap;
    }

    public void OnClickBtnMap(int idMap)
    {
        // if (ConfigManager.Instance.mapConfig.GetMapDataById(idMap).IsUnlock == true)
        {
            textMap = ListButtonMaps.First(x => x.idMap == idMap).textMap.text;
            desMap = ConfigManager.Instance.mapConfig.GetMapDataById(idMap).descriptionMap;
            Database.CurrentIdMap = idMap;
            PopupSelectMap.Show();
        }
    }
}