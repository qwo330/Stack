using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public int Power = 5;

    public void PlaySkill(GameObject skillObject = null)
    {
        StartCoroutine(CO_Action(skillObject));
    }

    protected abstract IEnumerator CO_Action(GameObject skillObject);

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Defines.key_Enemy))
        {
            BaseEnemy enemy = other.GetComponent<BaseEnemy>();
            enemy.Damage(Power);
        }
    }

    void OnTriggerStay(Collider other)
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        
    }
}
