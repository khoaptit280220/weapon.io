using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpawnTrail : MonoBehaviour
{
    public Transform parent;
    public GameObject snowPrefab;
    public GameObject dartPrefab;

    public void SpawnSnow(GameObject obj)
    {
        GameObject snow = Instantiate(snowPrefab, new Vector3(obj.transform.position.x, obj.transform.position.y, -2),
            snowPrefab.transform.rotation, parent);
        snow.SetActive(false);
        DOTween.Sequence().SetDelay(0.15f).OnComplete(() =>
        {
            snow.SetActive(true);
        }); 
        DOTween.Sequence().SetDelay(4).OnComplete(() =>
        {
            snow.SetActive(false);
        });
    }

    public void SpawnDart(GameObject obj)
    {
        GameObject dart = Instantiate(dartPrefab, new Vector3(obj.transform.position.x, obj.transform.position.y, -2),
            dartPrefab.transform.rotation, parent);
        dart.SetActive(false);
        DOTween.Sequence().SetDelay(0.15f).OnComplete(() =>
        {
            dart.SetActive(true);
        });
        DOTween.Sequence().SetDelay(4).OnComplete(() =>
        {
            dart.SetActive(false);
        });
    }
}
