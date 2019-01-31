using UnityEngine;
using Managers.InGameManager;

namespace Objects.Enemy
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField]
        float moveSpeed;

        void FixedUpdate()
        {
            transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Player"))
                InGameManager.Instance.Pool.ReturnObject(gameObject, ObjectType.EnemyBullet);
        }

        void OnTriggerExit(Collider other)
        {
            if (other.transform.CompareTag("Boundary"))
            {
                InGameManager.Instance.Pool.ReturnObject(gameObject, ObjectType.EnemyBullet);
            }
        }
    }
}