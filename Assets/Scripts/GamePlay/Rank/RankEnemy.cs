using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankEnemy : MonoBehaviour
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

    void Update()
    {
       // SpawnEnemy.cells.Sort();
        if (gameObject.GetComponentInParent<EnemyController>() != null && SpawnEnemy.cells.Count != 0)
        {
            if (this.gameObject.GetComponentInParent<EnemyController>().entityInfo == SpawnEnemy.cells[0])
            {
                SetupRankDefault();
                listRanks[0].gameObject.SetActive(true);
            }
            else if (this.gameObject.GetComponentInParent<EnemyController>().entityInfo == SpawnEnemy.cells[1])
            {
                SetupRankDefault();
                listRanks[1].gameObject.SetActive(true);
            }
            else if (this.gameObject.GetComponentInParent<EnemyController>().entityInfo == SpawnEnemy.cells[2])
            {
                SetupRankDefault();
                listRanks[2].gameObject.SetActive(true);
            }
            else
            {
                SetupRankDefault();
            }
        }
    }
}