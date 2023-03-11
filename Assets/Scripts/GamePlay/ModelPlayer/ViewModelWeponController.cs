using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Khoant;
using Sirenix.OdinInspector;
using UnityEngine;

public class ViewModelWeponController : MonoBehaviour
{
    public TypeViewModel TypeViewModel;
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
                Debug.Log("kiem main ");
                VARIABLE.gameObject.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        EventController.MainWeapon += MainWeaponGame;
        EventController.Lose += AfterWeapon;
        EventController.Win += AfterWeapon;

        // GameManager.Instance.ViewModelWeponController = this;
    }

    private void OnDestroy()
    {
        EventController.MainWeapon -= MainWeaponGame;
        EventController.Lose -= AfterWeapon;
        EventController.Win -= AfterWeapon;
    }

    public void MainWeaponGame()
    {
        SetupModelWeapon();
    }

    public void AfterWeapon()
    {
        SetupModelWeapon();
    }
}

public enum TypeViewModel
{
    ViewModelMain,
    ViewModelAfterGame,
}