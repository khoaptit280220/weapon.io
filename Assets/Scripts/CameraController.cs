#region

using UnityEngine;

#endregion

public class CameraController : HCMonoBehaviour
{
    public static CameraController instance;

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
        if (targetFollow != null)
            offset = Transform.position - targetFollow.transform.position;
        _originOffset = offset;
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
            Transform.position = new Vector3(Transform.position.x, targetFollow.position.y + offset.y,targetFollow.position.z + offset.z);
        }

        else if (targetFollow.position.y > Top && targetFollow.position.x > Left && targetFollow.position.x < Right)
        {
            Transform.position = new Vector3(targetFollow.position.x + offset.x, Transform.position.y, targetFollow.position.z + offset.z);
        }

        else if (targetFollow.position.x < Left && targetFollow.position.y > Down && targetFollow.position.y < Top)
        {
            Transform.position = new Vector3(Transform.position.x, targetFollow.position.y + offset.y, targetFollow.position.z +offset.z);
        }
        else if (targetFollow.position.y < Down && targetFollow.position.x > Left && targetFollow.position.x < Right)
        {
            Transform.position = new Vector3(targetFollow.position.x + offset.x, Transform.position.y, targetFollow.position.z + offset.z);
        }
        else if( targetFollow.position.x < Right && targetFollow.position.x > Left && targetFollow.position.y >Down && targetFollow.position.y < Top)
        {
            Transform.position = Vector3.Lerp(Transform.position,
                targetFollow.position + offset, Time.deltaTime * transitionSpeed); 
        }
    }
}