using System.Collections;
using UnityEngine;

public class Zombie : BaseEnemy
{
    public override void SetEnemy()
    {
        maxHP = 5;
        hp = maxHP;
        moveSpeed = 5f;
    }
}