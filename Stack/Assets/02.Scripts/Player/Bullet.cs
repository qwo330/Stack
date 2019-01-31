using UnityEngine;
using Managers.InGameManager;

namespace Objects.Player
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        float moveSpeed;

        void FixedUpdate()
        {
            transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Enemy"))
                InGameManager.Instance.Pool.ReturnObject(gameObject, ObjectType.Bullet);
        }

        void OnTriggerExit(Collider other)
        {
            if (other.transform.CompareTag("Boundary"))
            {
                InGameManager.Instance.Pool.ReturnObject(gameObject, ObjectType.Bullet);
            }
        }
    }
}