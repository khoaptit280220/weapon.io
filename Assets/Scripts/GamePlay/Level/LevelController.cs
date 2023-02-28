using System;
using System.Collections;
using System.Collections.Generic;
using CnControls;
using Sirenix.OdinInspector;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject level;
    [ReadOnly] public Level CurrentLevel;

    private void Start()
    {
        GameManager.Instance.SetupLevelController(this);
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        if (CurrentLevel != null)
        {
            Destroy(CurrentLevel.gameObject);
        }

        CurrentLevel = Instantiate(level).GetComponent<Level>();
        CurrentLevel.gameObject.SetActive(false);
        CurrentLevel.SetupMap();
        CurrentLevel.SetupSkin();
        SpawnEnemy.cells.Clear();
    }
}