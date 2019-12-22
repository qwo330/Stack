using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    float ReturnY;

    public void Init(Vector3 position)
    {
        transform.position = position;
        ReturnY = position.y + Defines.Screen_Height;
    }

    void FixedUpdate()
    {
        transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);

        if (transform.position.y > ReturnY)
            ObjectPool.Get.ReturnObject(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(Defines.key_Enemy))

            ObjectPool.Get.ReturnObject(gameObject);
    }
}
