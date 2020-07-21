using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    [HideInInspector]
    public int Power;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Defines.key_Enemy))
        {
            BaseEnemy enemy = other.GetComponent<BaseEnemy>();
            enemy.Hit(Power);
        }
    }
}
