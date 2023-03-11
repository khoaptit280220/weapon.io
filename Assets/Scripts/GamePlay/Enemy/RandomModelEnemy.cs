using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Khoant;
using Sirenix.OdinInspector;
using UnityEngine;

public class RandomModelEnemy : MonoBehaviour
{
    public List<SkinController> listSkinEnemys = new List<SkinController>();
    

    private void SetupSkinEnemyDefault()
    {
        listSkinEnemys.ForEach(item => item.gameObject.SetActive(false));
    }
    public void SetupModelSkin(int id)
    {
        SetupSkinEnemyDefault();
       
        foreach (var VARIABLE in listSkinEnemys)
        {
            if (VARIABLE.idSkin == id)
            {
                VARIABLE.gameObject.SetActive(true);
            }
        }
    }
}
