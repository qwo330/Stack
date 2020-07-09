using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCube : MonoBehaviour
{
    bool isGround;

    public void ReturnPool()
    {
        isGround = false;
    }

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

    // 플레이어와 y 포지션 비교해서 상단 접촉시 바닥 처리 & 하단 접촉시 통과 처리로 수정
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
            InGameManager.Instance.StackManaCube(gameObject);
        }
    }
}