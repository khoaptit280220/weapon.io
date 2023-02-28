using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public TMP_Text[] rows;
    public TMP_Text[] rowsP;

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy.cells.Sort();

        for (int i = 0; i < rows.Length; i++)
        {
            rows[i].text = "" + SpawnEnemy.cells[i].name;
        }

        for (int i = 0; i < rowsP.Length; i++)
        {
            rowsP[i].text = "" + SpawnEnemy.cells[i].point;
        }
    }
}