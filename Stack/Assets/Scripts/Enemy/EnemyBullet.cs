using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    void FixedUpdate()
    {
        if (InGameManager.Instance.CheckPlaying())
        {
            transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);

            if (transform.position.y < 0)
                ObjectPool.Get.ReturnObject(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(Defines.key_Player))
            ObjectPool.Get.ReturnObject(gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag(Defines.key_Ground))
        {
            ObjectPool.Get.ReturnObject(gameObject);
        }
    }
}
