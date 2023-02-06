using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpawnTrail : MonoBehaviour
{
    public Transform parent;
    public GameObject snowPrefab;
    public GameObject dartPrefab;

    private GameObject snow;
    private GameObject dart;
    
    private List<GameObject> listSnow;
    private List<GameObject> listDart;
    
    public void SpawnSnow(GameObject obj)
    {
        DOTween.Sequence().SetDelay(2).OnComplete(() =>
        {
            snow = Instantiate(snowPrefab, obj.transform.position, snowPrefab.transform.rotation, parent);
            listSnow.Add(snow);
        });
        for (int i = 0; i < listSnow.Count; i++)
        {
            DOTween.Sequence().SetDelay(4).OnComplete(() =>
            {
                listSnow[i].SetActive(false);
            });
            
        }
        
    }

    public void SpawnDart(GameObject obj)
    {
        Instantiate(dartPrefab, obj.transform.position, dartPrefab.transform.rotation, parent);
    }
}
