using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers.InGameManager;

namespace Objects.ManaStone
{
    public class ManaStone : MonoBehaviour
    {
        bool isGround; 

        void Update()
        {
            if (!isGround)
                transform.Translate(Vector3.down * 6f * Time.deltaTime);    
        }

        void switchWithPlayer(Transform player)
        {
            if(!isGround)
                player.position += new Vector3(0, 2f, 0);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                switchWithPlayer(collision.transform);
            }
            else if (collision.gameObject.CompareTag("Ground"))
            {
                if (isGround) return;

                isGround = true;
                Vector3 pos = transform.position;
                transform.position = new Vector3(pos.x, Mathf.Round(pos.y), 0);
                InGameManager.Instance.stackManaStone(gameObject);
            }
        }
    }
}