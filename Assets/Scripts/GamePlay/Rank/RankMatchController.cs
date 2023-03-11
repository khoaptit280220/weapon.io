using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankMatchController : MonoBehaviour
{
    public List<RankMatch> listRanks = new List<RankMatch>();

    private void SetupRankDefault()
    {
        listRanks.ForEach(item => item.gameObject.SetActive(false));
    }

    private void Start()
    {
        SetupRankDefault();
    }

    private void Update()
    {
        if (gameObject.GetComponentInParent<PlayerController>() != null && SpawnEnemy.cells.Count != 0)
        {
            if (this.gameObject.GetComponentInParent<PlayerController>().entityInfo == SpawnEnemy.cells[0])
            {
                SetupRankDefault();
                listRanks[0].gameObject.SetActive(true);
                if (GameManager.Instance.rank > 1)
                {
                    GameManager.Instance.rank = 1;
                }
            }
            else if (this.gameObject.GetComponentInParent<PlayerController>().entityInfo == SpawnEnemy.cells[1])
            {
                SetupRankDefault();
                listRanks[1].gameObject.SetActive(true);
                if (GameManager.Instance.rank > 2)
                {
                    GameManager.Instance.rank = 2;
                }
            }
            else if (this.gameObject.GetComponentInParent<PlayerController>().entityInfo == SpawnEnemy.cells[2])
            {
                SetupRankDefault();
                listRanks[2].gameObject.SetActive(true);
                if (GameManager.Instance.rank > 3)
                {
                    GameManager.Instance.rank = 3;
                }
            }
            else if (this.gameObject.GetComponentInParent<PlayerController>().entityInfo == SpawnEnemy.cells[3])
            {
                if (GameManager.Instance.rank > 4)
                {
                    GameManager.Instance.rank = 4;
                }
            }
            else if (this.gameObject.GetComponentInParent<PlayerController>().entityInfo == SpawnEnemy.cells[4])
            {
                if (GameManager.Instance.rank > 5)
                {
                    GameManager.Instance.rank = 5;
                }
            }
            else
            {
                SetupRankDefault();
                if (GameManager.Instance.rank == 0)
                {
                    GameManager.Instance.rank = 7;
                }
            }
        }
    }
}