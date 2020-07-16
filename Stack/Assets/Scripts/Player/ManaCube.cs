using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCube : MonoBehaviour
{
    bool isGround;
    WaitForFixedUpdate waitFixedUpdate;

    void OnEnable()
    {
        StartCoroutine(DropCube());
    }

    void OnDisable()
    {
        isGround = false;
        StopCoroutine(DropCube());
    }

    public IEnumerator DropCube()
    {
        while(!isGround)
        {
            transform.Translate(Vector3.down * 6f * Time.deltaTime);
            yield return waitFixedUpdate;
        }

        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, Mathf.Round(pos.y), 0);
        InGameManager.Instance.StackManaCube(gameObject);
    }

    void PlaceUnderPlayer(Transform player)
    {
        if (!isGround)
        {
            Vector3 playerPos = player.position;
            Vector3 cubePos = transform.position;

            if (playerPos.y < cubePos.y)
            {
                player.position = cubePos;
                //player.position = new Vector3(playerPos.x, cubePos.y, cubePos.z);
                transform.position.Set(cubePos.x, playerPos.y, playerPos.z);        
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isGround)
        {
            if (collision.transform.CompareTag(Defines.key_Ground))
            {
                isGround = true;
            }
            else if(collision.transform.CompareTag(Defines.key_Player))
            {
                PlaceUnderPlayer(collision.transform);
            }
        }
    }
}