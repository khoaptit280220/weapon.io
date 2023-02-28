using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ModelHornController : MonoBehaviour
{
    [ReadOnly] public ModelHornData currentModelHornData;

    public List<ModelHorn> listModelHorns = new List<ModelHorn>();

    private void SetupModelHornDefault()
    {
        listModelHorns.ForEach(item => item.gameObject.SetActive(false));
    }

    public void SetupModelHorn()
    {
        currentModelHornData = ConfigManager.Instance.modelHornConfig.GetModelHornById(Database.CurrentIdModelSkin);
        SetupModelHornDefault();
        foreach (var VARIABLE in listModelHorns)
        {
            if (VARIABLE.idHorn == currentModelHornData.idModelHorn)
            {
                VARIABLE.gameObject.SetActive(true);
            }
        }
    }
}