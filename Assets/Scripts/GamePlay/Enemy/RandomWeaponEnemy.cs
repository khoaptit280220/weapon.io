using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Khoant;
using Sirenix.OdinInspector;
using UnityEngine;

public class RandomWeaponEnemy : MonoBehaviour
{ 
    
    public List<ModelHorn> listWeaponEnemys = new List<ModelHorn>();

    [Button]
    public void LoadList()
    {
        listWeaponEnemys = GetComponentsInChildren<ModelHorn>().ToList();
    }
    private void SetupWeaponEnemyDefault()
    {
        listWeaponEnemys.ForEach(item => item.gameObject.SetActive(false));
    }

    public void SetupModelWeapon(int id)
    {
        SetupWeaponEnemyDefault();
        foreach (var VARIABLE in listWeaponEnemys)
        {
            if (VARIABLE.idHorn == id)
            {
                VARIABLE.gameObject.SetActive(true);
            }
        }
    }
}