using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Khoant;
using Sirenix.OdinInspector;
using UnityEngine;

public class ViewModelWeponController : MonoBehaviour
{
    [ReadOnly] public ModelHornData currentModelWeaponData;

    public List<ModelHorn> listModelWeapon = new List<ModelHorn>();

    [Button]
    private void LoadList()
    {
        listModelWeapon = GetComponentsInChildren<ModelHorn>().ToList();
    }

    private void SetupModelWeaponDefault()
    {
        listModelWeapon.ForEach(item => item.gameObject.SetActive(false));
    }

    public void SetupModelWeapon()
    {
        currentModelWeaponData = ConfigManager.Instance.modelHornConfig.GetModelHornById(Database.CurrentIdHorn);
        SetupModelWeaponDefault();
        foreach (var VARIABLE in listModelWeapon)
        {
            if (VARIABLE.idHorn == currentModelWeaponData.idModelHorn)
            {
                VARIABLE.gameObject.SetActive(true);
            }
        }
    }

    public void SetupModelWeaponAfter()
    {
        currentModelWeaponData = ConfigManager.Instance.modelHornConfig.GetModelHornById(Database.CurrentIdHorn);
        SetupModelWeaponDefault();
        foreach (var VARIABLE in listModelWeapon)
        {
            if (VARIABLE.idHorn == currentModelWeaponData.idModelHorn)
            {
                VARIABLE.gameObject.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        EventController.MainWeapon += MainWeaponGame;
        EventController.AfterWeapon += AfterWeapon;
    }

    private void OnDestroy()
    {
        EventController.MainWeapon -= MainWeaponGame;
        EventController.AfterWeapon -= AfterWeapon;
    }

    public void MainWeaponGame()
    {
        SetupModelWeapon();
    }

    public void AfterWeapon()
    {
        SetupModelWeaponAfter();
    }
}

public enum TypeViewModel
{
    ViewModelMain,
    ViewModelAfterGame,
}