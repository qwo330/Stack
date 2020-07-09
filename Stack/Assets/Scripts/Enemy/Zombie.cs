using System.Collections;
using UnityEngine;

public class Zombie : BaseEnemy
{
    void Start()
    {
        maxHP = 5;
        hp = maxHP;
        moveSpeed = 5f;
    }

    public override IEnumerator CO_Attack()
    {
        yield return null;
    }
}