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
        if (targetFollow.position.x > 105 && -55 < targetFollow.position.y && targetFollow.position.y< 49)
        {
            Transform.position = new Vector3(Transform.position.x, targetFollow.position.y + offset.y,targetFollow.position.z + offset.z);
        }

        else if (targetFollow.position.y > 49 && targetFollow.position.x > -75 && targetFollow.position.x < 105)
        {
            Transform.position = new Vector3(targetFollow.position.x + offset.x, Transform.position.y, targetFollow.position.z + offset.z);
        }

        else if (targetFollow.position.x < -80 && targetFollow.position.y > -55 && targetFollow.position.y < 49)
        {
            Transform.position = new Vector3(Transform.position.x, targetFollow.position.y + offset.y, targetFollow.position.z +offset.z);
        }
        else if (targetFollow.position.y < -55 && targetFollow.position.x > -75 && targetFollow.position.x < 105)
        {
            Transform.position = new Vector3(targetFollow.position.x + offset.x, Transform.position.y, targetFollow.position.z + offset.z);
        }
        else if( targetFollow.position.x < 105 && targetFollow.position.x > -80 && targetFollow.position.y >-55 && targetFollow.position.y < 49)
        {
            Transform.position = Vector3.Lerp(Transform.position,
                targetFollow.position + offset, Time.deltaTime * transitionSpeed); 
        }
    }
}