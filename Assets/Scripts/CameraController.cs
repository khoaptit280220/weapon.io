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

    private Vector3 _originOffset;
    public Vector3 offset;

    public Transform targetFollow;

    public float transitionSpeed;

    private float Left = -100;
    private float Right = 100;
    private float Top = 61;
    private float Down = -61;

    private void Awake()
    {
        instance = this;
        this.transform.position = new Vector3(targetFollow.position.x, transform.position.y, transform.position.z);
        if (targetFollow != null)
        {
            offset = Transform.position - targetFollow.transform.position;
        }
            
        _originOffset = offset;
        
        
    }

    private void Start()
    {
        Moon.gameObject.SetActive(false);
        Mask.transform.position = new Vector3(960, 540, 0);
    }

    public void ResetOffset()
    {
        offset = _originOffset;
    }

    private void LateUpdate()
    {
        if (targetFollow == null)
            return;
        if (targetFollow.position.x > Right && Down < targetFollow.position.y && targetFollow.position.y< Top)
        {
            Transform.position = new Vector3(Transform.position.x, targetFollow.position.y + offset.y,
                targetFollow.position.z + offset.z);
        }

        if (targetFollow.position.x > Right)
        {
            Mask.transform.position = Vector3.Lerp(Mask.transform.position,
                new Vector3(960 + 24 * (targetFollow.position.x - Right),
                    Mask.transform.position.y, Mask.transform.position.z), Time.deltaTime * 1000);
        }
        if (targetFollow.position.y > Top && targetFollow.position.x > Left && targetFollow.position.x < Right)
        {
            Transform.position = new Vector3(targetFollow.position.x + offset.x, Transform.position.y,
                targetFollow.position.z + offset.z);
        }

        if (targetFollow.position.y > Top)
        {
            Mask.transform.position = Vector3.Lerp(Mask.transform.position,
                new Vector3(Mask.transform.position.x, 540 + 24 * (targetFollow.position.y - Top),
                    Mask.transform.position.z), Time.deltaTime * 1000);
        }
        if (targetFollow.position.x < Left && targetFollow.position.y > Down && targetFollow.position.y < Top)
        {
            Transform.position = new Vector3(Transform.position.x, targetFollow.position.y + offset.y,
                targetFollow.position.z + offset.z);
        }

        if (targetFollow.position.x < Left)
        {
            Mask.transform.position = Vector3.Lerp(Mask.transform.position,
                new Vector3(960 + 24 * (targetFollow.position.x - Left),
                    Mask.transform.position.y, Mask.transform.position.z), Time.deltaTime * 1000);
        }
        if (targetFollow.position.y < Down && targetFollow.position.x > Left && targetFollow.position.x < Right)
        {
            Transform.position = new Vector3(targetFollow.position.x + offset.x, Transform.position.y,
                targetFollow.position.z + offset.z);
        }

        if (targetFollow.position.y < Down)
        {
            Mask.transform.position = Vector3.Lerp(Mask.transform.position,
                new Vector3(Mask.transform.position.x, 540 + 24 * (targetFollow.position.y - Down),
                    Mask.transform.position.z), Time.deltaTime * 1000);
        }
        if( targetFollow.position.x < Right && targetFollow.position.x > Left && targetFollow.position.y >Down && targetFollow.position.y < Top) 
        {
            Transform.position = Vector3.Lerp(Transform.position,
                targetFollow.position + offset, Time.deltaTime * transitionSpeed);
            //Mask.transform.position = new Vector3(960, 540, 0);
        }
        
    }

    private void Update()
    {
        if(Database.CurrentIdMap == 6)
        {
            if (GameManager.Instance.time == 50 || GameManager.Instance.time == 25)
            {
                Moon.gameObject.SetActive(true);
                DOTween.Sequence().SetDelay(15).OnComplete(() =>
                {
                    Moon.gameObject.SetActive(false);
                });
            }
        }
    }
}