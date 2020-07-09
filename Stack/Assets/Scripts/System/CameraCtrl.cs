using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField]
    Vector3 cameraOffset = new Vector3(4f, 5f, -10f);

    Transform player;
    
    public void Init(Transform player)
    {
        this.player = player;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        transform.position = player.position.y * Vector3.up + cameraOffset;
    }
}