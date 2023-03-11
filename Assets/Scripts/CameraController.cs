#region

using System;
using DG.Tweening;
using UnityEngine;

#endregion

public class CameraController : HCMonoBehaviour
{
    public static CameraController instance;
    public Canvas Moon;
    public GameObject Mask;

    public Transform targetFollow;

    private float Left = -100;
    private float Right = 100;
    private float Top = 155;
    private float Down = -62;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Moon.gameObject.SetActive(false);
        Mask.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

    private void LateUpdate()
    {
        if (targetFollow == null)
            return;

        if (targetFollow.position.x > Right)
        {
            Mask.transform.position = Vector3.Lerp(Mask.transform.position,
                new Vector3(Screen.width / 2 + 24 * (targetFollow.position.x - Right),
                    Mask.transform.position.y, Mask.transform.position.z), Time.deltaTime * 1000);
        }

        if (targetFollow.position.y > Top)
        {
            Mask.transform.position = Vector3.Lerp(Mask.transform.position,
                new Vector3(Mask.transform.position.x, Screen.height / 2 + 24 * (targetFollow.position.y - Top),
                    Mask.transform.position.z), Time.deltaTime * 1000);
        }

        if (targetFollow.position.x < Left)
        {
            Mask.transform.position = Vector3.Lerp(Mask.transform.position,
                new Vector3(Screen.width / 2 + 24 * (targetFollow.position.x - Left),
                    Mask.transform.position.y, Mask.transform.position.z), Time.deltaTime * 1000);
        }

        if (targetFollow.position.y < Down)
        {
            Mask.transform.position = Vector3.Lerp(Mask.transform.position,
                new Vector3(Mask.transform.position.x, Screen.height / 2 + 24 * (targetFollow.position.y - Down),
                    Mask.transform.position.z), Time.deltaTime * 1000);
        }
    }

    private void Update()
    {
        if (Database.CurrentIdMap == 6)
        {
            if (GameManager.Instance.time == 50 || GameManager.Instance.time == 25)
            {
                Moon.gameObject.SetActive(true);
                DOTween.Sequence().SetDelay(15).OnComplete(() => { Moon.gameObject.SetActive(false); });
            }
        }
    }
}