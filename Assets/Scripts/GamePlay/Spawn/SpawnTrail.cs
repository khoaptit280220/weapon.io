using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpawnTrail : MonoBehaviour
{
    public Transform parent;
    public GameObject snowPrefab;
    public GameObject dartPrefab;
    public GameObject torpedoPrefab;

    public void SpawnSnow(GameObject obj)
    {
        GameObject snow = Instantiate(snowPrefab,
            new Vector3(obj.transform.position.x, obj.transform.position.y, 0),
            snowPrefab.transform.rotation, parent);
        snow.SetActive(false);
        DOTween.Sequence().SetDelay(0.15f).OnComplete(() => { snow.SetActive(true); });
        DOTween.Sequence().SetDelay(4).OnComplete(() => { snow.SetActive(false); });
    }

    public void SpawnDart(GameObject obj)
    {
        GameObject dart = Instantiate(dartPrefab,
            new Vector3(obj.transform.position.x, obj.transform.position.y, 0),
            dartPrefab.transform.rotation, parent);
        dart.SetActive(false);
        DOTween.Sequence().SetDelay(0.15f).OnComplete(() => { dart.SetActive(true); });
        DOTween.Sequence().SetDelay(4).OnComplete(() => { dart.SetActive(false); });
    }

    public void SpawnTorpedo(GameObject obj)
    {
        GameObject torpedo = Instantiate(torpedoPrefab,
            new Vector3(obj.transform.position.x, obj.transform.position.y, 0),
            torpedoPrefab.transform.rotation, parent);
        torpedo.SetActive(false);
        DOTween.Sequence().SetDelay(0.15f).OnComplete(() => { torpedo.SetActive(true); });
        DOTween.Sequence().SetDelay(4).OnComplete(() => { torpedo.SetActive(false); });
    }
}