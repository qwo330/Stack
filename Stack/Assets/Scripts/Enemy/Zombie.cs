using System.Collections;
using UnityEngine;

public class Zombie : BaseEnemy
{
    void Start()
    {
        maxHP = 6;
        hp = maxHP;
        moveSpeed = 1f;
    }

    public override IEnumerator CO_Attack()
    {
        yield return null;
    }
}