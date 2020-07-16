using UnityEngine;

public class DestinationLine : MonoBehaviour
{
    public void SetPosition(int height)
    {
        transform.position = new Vector3(Defines.Screen_Width * 0.5f, height, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Defines.key_Player))
        {
            InGameManager.Instance.GameClearEvent.Invoke();
        }
    }
}
