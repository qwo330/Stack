using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField]
    Vector3 cameraOffset = new Vector3(4f, 5f, -10f);

    [SerializeField]
    float smothTime = 1f;

    Vector3 velocity = Vector3.zero;
    Transform player;
    
    public void Init(Transform player)
    {
        this.player = player;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        Vector3 nextPos = player.position.y * Vector3.up + cameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, nextPos, ref velocity, smothTime);
        //transform.position = Vector3.Lerp(transform.position, nextPos, Time.deltaTime * cameraSpeed);
    }
}