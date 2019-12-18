using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaStone : MonoBehaviour
{
    bool isGround;

    void FixedUpdate()
    {
        if (!isGround)
            transform.Translate(Vector3.down * 6f * Time.deltaTime);
    }

    void SwitchWithPlayer(Transform player)
    {
        if (!isGround)
            player.position += new Vector3(0, 2f, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            SwitchWithPlayer(collision.transform);
        }
        else if (collision.transform.CompareTag("Ground"))
        {
            if (isGround) return;

            isGround = true;
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x, Mathf.Round(pos.y), 0);
            InGameManager.Instance.StackManaStone(gameObject);
        }
    }
}