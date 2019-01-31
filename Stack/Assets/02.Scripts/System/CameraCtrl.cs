using UnityEngine;

namespace InGameSystem.Camera
{
    public class CameraCtrl : MonoBehaviour
    {
        GameObject player;

        public void Init()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.Log("플레이어를 찾을 수 없음");
                return;
            }
        }

        void LateUpdate()
        {
            if (player != null)
            {
                followPlayer();
            }
        }

        void followPlayer()
        {
            transform.position = new Vector3(4f, player.transform.position.y + Defines.CAMERAGAP, 10f);
        }
    }
}