using UnityEngine;

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

            ObjectPool.Get.ReturnObject(gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Boundary"))
        {
            ObjectPool.Get.ReturnObject(gameObject);
        }
    }
}
