using System;
using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    
    public GameObject level;
    public GameObject CurrentLevel;

    private void Start()
    {
        GameManager.Instance.SetupLevelController(this);
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        
        if (CurrentLevel != null)
        {
            Destroy(CurrentLevel);
        }
        CurrentLevel = Instantiate(level);
        CurrentLevel.gameObject.SetActive(false);
    }
    public void Update()
    {
        // if (GameManager.Instance.GetPlayer.isPlayerDied == false)
        // {
        //     CurrentLevel.gameObject.SetActive(true);
        // }
    }
}
